// DSBridge.h

#pragma once

#ifdef _MSC_VER
#include <windows.h>
#endif

#include <stdio.h>
#include <vector>
#include <math.h>
#include <exception>
#include <DepthSense.hxx>
#include <algorithm>

using namespace DepthSense;
namespace DSB {
	public struct DSPoint {
		float x, y, z;
		uint8_t r, g, b;
	};

	public struct DSPointFC {
		float x, y, z, c;
	};

	public ref class DSPointCloud {
	private:
		DSPoint* points;
		int pointCount;
		int index;

	public:
		DSPointCloud(int size);
		~DSPointCloud();

		void addPoint(DSPoint point);
		DSPoint* getPoint(int index);
		int getSize();
	};

	public ref class PCDVideoPlayback {
		static void playBack(int person, int gesture, int frames, bool cropped);
	};

	public ref class DSBridge
	{
	public:
		static void init();
		static void destroy();
		static void run();
		static void quit();

		static uint8_t* getCachedRGB();

		static int getRGBCount();
		static int getDepthCount();

		static void resetVideoCapture();
		static void startVideoCapture(int frames);
		static int getCaptureProgress();
		static array<DSPointCloud ^> ^DSBridge::getVideo();
		static void saveToPCDs(int person, int gesture);
	};
}
