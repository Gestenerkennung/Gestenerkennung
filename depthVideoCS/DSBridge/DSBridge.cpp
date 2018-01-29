
#include "DSBridge.h"

using namespace DepthSense;

namespace DSB {
	//depthSense library object
	Context g_context;
	DepthNode g_dnode;
	ColorNode g_cnode;
	AudioNode g_anode;

	//frame counters for the different nodes
	int g_aFrames = 0;
	int g_cFrames = 0;
	int g_dFrames = 0;

	bool g_bDeviceFound = false;

	//projection helper for converting the captured depth-image to a point cloud
	ProjectionHelper* g_pProjHelper;
	StereoCameraParameters g_scp;

	//buffer for the last captured RGB-image, used for mapping RGB to 3D points
	uint8_t lastRGB[640 * 480 * 3];

	int vcFrame = 0;
	int vcCount = 0;
	int vcFramesLeft = 0;
	bool vcInCapture = false;


	ref class Glob {
	public:
		static array<DSPointCloud^>^ vcCloudsBuffer;
	};

	//union for conversion between long RGB and float RGB, used in saving the PCD file (float RGB)
	typedef union
	{
		float float_value;
		uint32_t long_value;
	} RGBValue;

	//handler for new color sample
	void onNewColorSample(ColorNode node, ColorNode::NewSampleReceivedData data)
	{
		//Get width and height of current frame format
		int32_t w, h;
		FrameFormat_toResolution(data.captureConfiguration.frameFormat,&w,&h);

		//printf("new color sample dim %d, %d\n", w, h);

		//Save color sample to buffer for point cloud saving
		memcpy(lastRGB, data.colorMap, 640 * 480 * 3);

		//printf("Color#%u: %d\n",g_cFrames,data.colorMap.size());
		g_cFrames++;	
	}


	//handler for new depth sample
	void onNewDepthSample(DepthNode node, DepthNode::NewSampleReceivedData data)
	{
		//do we still need to capture images?
		if(vcFramesLeft <= 0) {
			vcInCapture = false; //if not, we are finished here
		}

		//are we still capturing?
		if(vcInCapture) {
			int32_t w, h;
			FrameFormat_toResolution(data.captureConfiguration.frameFormat,&w,&h);
		
			int points = 0; //number of points that are written
			Point2D pts[320*240]; //buffer for 2d points
		
			if (!g_pProjHelper) //create projection helper if it doesn't exist
			{
				g_pProjHelper = new ProjectionHelper (data.stereoCameraParameters);
				g_scp = data.stereoCameraParameters;
			}
			else if (g_scp != data.stereoCameraParameters)
			{
				//set correct parameters if they have changed
				g_pProjHelper->setStereoCameraParameters(data.stereoCameraParameters);
				g_scp = data.stereoCameraParameters;
			}

			//compute the 2d source point for each vertex
			g_pProjHelper->get2DCoordinates(data.verticesFloatingPoint, pts, 320*240, CAMERA_PLANE_DEPTH);

			//count points that are mapped in uvMap and fulfill the cropping statement
			for(int i = 0; i < data.vertices.size(); i++) {
				FPVertex point = data.verticesFloatingPoint[i];
				int uvix = (int)pts[i].x;
				int uviy = (int)pts[i].y;

				int uvi = uvix + uviy * 320;

				//Is this pixel a correct entry in the uvMap and doesn't get cropped?
				if(data.uvMap[uvi].u >= 0.0 && 
					data.uvMap[uvi].u < 1.0 && 
					data.uvMap[uvi].v >= 0.0 && 
					data.uvMap[uvi].v < 1.0) {
					points++;
				}
			}

			//create new C# cloud object
			DSPointCloud^ cloud = gcnew DSPointCloud(points);
			Glob::vcCloudsBuffer[vcFrame] = cloud;

			for(int i = 0; i < data.vertices.size(); i++) {
				FPVertex point = data.verticesFloatingPoint[i];

				//get source point of vertex for use in uvMap
				int uvix = (int)pts[i].x;
				int uviy = (int)pts[i].y;

				//compute Index in uvMap
				int uvi = uvix + uviy * 320;

				//compute coordinates in the color map, using the mapping from uvMap
				int x = data.uvMap[uvi].u * 640.0f;
				int y = data.uvMap[uvi].v * 480.0f;

				//Compute indices
				//NOTE: The computation for y must be done in this 2 steps,
				//      first with a floating point multiplication and conversion to int
				//      and AFTER that the int multiplication for the 1d index, otherwise
				//      floating point imprecision and a wrong mapping from xyz to color will occur
				int coord = 3 * (x + y * 640);

				//Is this pixel a correct entry in the uvMap and doesn't get cropped?
				if(data.uvMap[uvi].u >= 0.0 && 
					data.uvMap[uvi].u < 1.0 && 
					data.uvMap[uvi].v >= 0.0 && 
					data.uvMap[uvi].v < 1.0) {

					DSPoint pt;

					pt.x = point.x;
					pt.y = point.y;
					pt.z = point.z;
				
					pt.b = lastRGB[coord+2];
					pt.g = lastRGB[coord+1];
					pt.r = lastRGB[coord+0];

					cloud->addPoint(pt);
				}
			}

			vcFrame++;
			vcFramesLeft--;
		}

		g_dFrames++;
	}

	void configureDepthNode() {
		g_dnode.newSampleReceivedEvent().connect(&onNewDepthSample);

		DepthNode::Configuration config = g_dnode.getConfiguration();
		config.frameFormat = FRAME_FORMAT_QVGA;
		config.framerate = 25;
		config.mode = DepthNode::CAMERA_MODE_CLOSE_MODE;
		config.saturation = false;

		g_dnode.setEnableVertices(true);
		g_dnode.setEnableDepthMap(true);
		g_dnode.setEnableVerticesFloatingPoint(true);
		g_dnode.setEnableDepthMapFloatingPoint(true);
		g_dnode.setEnableUvMap(true);

		try 
		{
			g_context.requestControl(g_dnode,0);

			g_dnode.setConfiguration(config);
		}
		catch (ArgumentException& e)
		{
			printf("Argument Exception: %s\n",e.what());
		}
		catch (UnauthorizedAccessException& e)
		{
			printf("Unauthorized Access Exception: %s\n",e.what());
		}
		catch (IOException& e)
		{
			printf("IO Exception: %s\n",e.what());
		}
		catch (InvalidOperationException& e)
		{
			printf("Invalid Operation Exception: %s\n",e.what());
		}
		catch (ConfigurationException& e)
		{
			printf("Configuration Exception: %s\n",e.what());
		}
		catch (StreamingException& e)
		{
			printf("Streaming Exception: %s\n",e.what());
		}
		catch (TimeoutException&)
		{
			printf("TimeoutException\n");
		}

	}

	void configureColorNode() {
		// connect new color sample handler
		g_cnode.newSampleReceivedEvent().connect(&onNewColorSample);

		ColorNode::Configuration config = g_cnode.getConfiguration();
		config.frameFormat = FRAME_FORMAT_VGA;
		config.compression = COMPRESSION_TYPE_MJPEG;
		config.powerLineFrequency = POWER_LINE_FREQUENCY_50HZ;
		config.framerate = 25;

		g_cnode.setEnableColorMap(true);

		try 
		{
			g_context.requestControl(g_cnode,0);

			g_cnode.setConfiguration(config);
		}
		catch (ArgumentException& e)
		{
			printf("Argument Exception: %s\n",e.what());
		}
		catch (UnauthorizedAccessException& e)
		{
			printf("Unauthorized Access Exception: %s\n",e.what());
		}
		catch (IOException& e)
		{
			printf("IO Exception: %s\n",e.what());
		}
		catch (InvalidOperationException& e)
		{
			printf("Invalid Operation Exception: %s\n",e.what());
		}
		catch (ConfigurationException& e)
		{
			printf("Configuration Exception: %s\n",e.what());
		}
		catch (StreamingException& e)
		{
			printf("Streaming Exception: %s\n",e.what());
		}
		catch (TimeoutException&)
		{
			printf("TimeoutException\n");
		}
	}

	void configureNode(Node node) {
		if ((node.is<DepthNode>())&&(!g_dnode.isSet()))
		{
			g_dnode = node.as<DepthNode>();
			configureDepthNode();
			g_context.registerNode(node);
		}

		if ((node.is<ColorNode>())&&(!g_cnode.isSet()))
		{
			g_cnode = node.as<ColorNode>();
			configureColorNode();
			g_context.registerNode(node);
		}
	}

	void onNodeConnected(Device device, Device::NodeAddedData data)
	{
		configureNode(data.node);
	}

	static void onNodeDisconnected(Device device, Device::NodeRemovedData data) {
		if (data.node.is<AudioNode>() && (data.node.as<AudioNode>() == g_anode))
			g_anode.unset();
		if (data.node.is<ColorNode>() && (data.node.as<ColorNode>() == g_cnode))
			g_cnode.unset();
		if (data.node.is<DepthNode>() && (data.node.as<DepthNode>() == g_dnode))
			g_dnode.unset();
	}

	void onDeviceConnected(Context context, Context::DeviceAddedData data) {
		if (!g_bDeviceFound)
		{
			data.device.nodeAddedEvent().connect(&onNodeConnected);
			data.device.nodeRemovedEvent().connect(&onNodeDisconnected);
			g_bDeviceFound = true;
		}
	}

	void onDeviceDisconnected(Context context, Context::DeviceRemovedData data)
	{
		g_bDeviceFound = false;
	}

	void DSBridge::init() 
	{
		g_context = Context::create("localhost");

		g_context.deviceAddedEvent().connect(&onDeviceConnected);
		g_context.deviceRemovedEvent().connect(&onDeviceDisconnected);

		// Get the list of currently connected devices
		std::vector<Device> da = g_context.getDevices();

		// We are only interested in the first device
		if (da.size() >= 1)
		{
			g_bDeviceFound = true;

			da[0].nodeAddedEvent().connect(&onNodeConnected);
			da[0].nodeRemovedEvent().connect(&onNodeDisconnected);

			std::vector<Node> na = da[0].getNodes();

			printf("Found %u nodes\n",na.size());

			for (int n = 0; n < (int)na.size();n++) {
				configureNode(na[n]);
			}
		}

		g_context.startNodes();
	}

	void DSBridge::destroy() {
		g_context.stopNodes();

		if (g_cnode.isSet()) g_context.unregisterNode(g_cnode);
		if (g_dnode.isSet()) g_context.unregisterNode(g_dnode);
		if (g_anode.isSet()) g_context.unregisterNode(g_anode);

		if (g_pProjHelper)
			delete g_pProjHelper;
	}

	void DSBridge::run() {
		g_context.run();
	}

	void DSBridge::quit() {
		g_context.quit();
	}

	uint8_t* DSBridge::getCachedRGB() {
		return lastRGB;
	}

    int DSBridge::getRGBCount() {
		return g_cFrames;
	}

	int DSBridge::getDepthCount() {
		return g_dFrames;
	}
	
	void DSBridge::resetVideoCapture() {
		vcFrame = 0;
		vcInCapture = false;
	}

	void DSBridge::startVideoCapture(int frames) {
		if(vcCount > 0) {
			for(int s = 0; s < vcCount; s++) {
				delete Glob::vcCloudsBuffer[s];
			}
		}

		Glob::vcCloudsBuffer = gcnew array<DSPointCloud^>(frames);
		
		vcCount = frames;
		vcFramesLeft = frames;
		vcInCapture = true;
	}
	
	int DSBridge::getCaptureProgress() {
		if(!vcInCapture && vcFrame != 0) return -1; //-1 = Finished
		if(!vcInCapture) return -2; //-2 = Awaiting
		return vcFrame; // >= 0 Progress
	}

	array<DSPointCloud^>^ DSBridge::getVideo() {
		return Glob::vcCloudsBuffer;
	}

	void DSBridge::saveToPCDs(int p, int g) {



			for(int s = 0; s < vcCount; s++) {
			char* filename = new char[512];

			sprintf_s(filename, 512, "datasets/p%d/g%d/uncropped/p%d-g%d-s%d.pcd", p, g, p, g, s);

			FILE* pcdOut;
			fopen_s(&pcdOut, filename, "wb");

			int points = Glob::vcCloudsBuffer[s]->getSize();

			fprintf_s(pcdOut, "VERSION .7\n");
			fprintf_s(pcdOut, "FIELDS x y z rgb\n");
			fprintf_s(pcdOut, "SIZE 4 4 4 4\n");
			fprintf_s(pcdOut, "TYPE F F F F\n");
			fprintf_s(pcdOut, "COUNT 1 1 1 1\n");
			fprintf_s(pcdOut, "WIDTH %d\n", points);
			fprintf_s(pcdOut, "HEIGHT 1\n");
			fprintf_s(pcdOut, "VIEWPOINT 0 0 0 1 0 0 0\n");
			fprintf_s(pcdOut, "POINTS %d\n", points);
			fprintf_s(pcdOut, "DATA binary\n");

			for(int i = 0; i < points; i++) {
				DSPoint* point = Glob::vcCloudsBuffer[s]->getPoint(i);

				RGBValue color;
				color.long_value = ((point->r << 0) + (point->g << 8) + (point->b << 16) + (0 << 24));
				//fprintf_s(pcdOut, "%.15le %.15le %.15le %e\n", -point->x, point->y, point->z, color.float_value);			

				DSPointFC pointfc;

				pointfc.x = point->x;
				pointfc.y = point->y;
				pointfc.z = point->z;
				pointfc.c = color.float_value;

				fwrite(&pointfc, sizeof(DSPointFC), 1, pcdOut);
			}

			fclose(pcdOut);
			delete filename;
		}
	}

	DSPointCloud::DSPointCloud(int size)
		: pointCount(size), index(0) {
		this->points = new DSPoint[size];
	}

	DSPointCloud::~DSPointCloud() {
		delete this->points;
	}

	void DSPointCloud::addPoint(DSPoint point) {
		points[this->index] = point;

		this->index++;
	}

	DSPoint* DSPointCloud::getPoint(int i) {
		return &(this->points[i]);
	}

	int DSPointCloud::getSize() {
		return this->pointCount;
	}

	void PCDVideoPlayback::playBack(int person, int gesture, int frames, bool cropped) {
		/*boost::shared_ptr<pcl::visualization::PCLVisualizer> viewer (new pcl::visualization::PCLVisualizer ("3D Viewer"));
	
		viewer->setBackgroundColor (0, 0, 0);

		char bfr1[60];
		if(!cropped) {
			sprintf(bfr1, "datasets\\p%d-g%d-s%d.pcd", person, gesture, 0);
		}
		else
		{
			sprintf(bfr1, "datasets\\p%d-g%d-s%d_cropped.pcd", person, gesture, 0);
		}
		pcl::PointCloud<pcl::PointXYZ>::Ptr cloud1 (new pcl::PointCloud<pcl::PointXYZ>);
		pcl::io::loadPCDFile (bfr1, *cloud1);
		viewer->addPointCloud<pcl::PointXYZ> (cloud1, "sample cloud");


		for(int i = 0; i < frames; i++) {
			if(!cropped) {
				sprintf(bfr1, "datasets\\p%d-g%d-s%d.pcd", person, gesture, 0);
			}
			else
			{
				sprintf(bfr1, "datasets\\p%d-g%d-s%d_cropped.pcd", person, gesture, 0);
			}
			pcl::io::loadPCDFile (bfr1, *cloud1);
			viewer->updatePointCloud<pcl::PointXYZ> (cloud1, "sample cloud");
			viewer->spinOnce (0.1);
		}*/
	}
}