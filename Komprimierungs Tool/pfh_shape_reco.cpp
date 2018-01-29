#include "pfh_shape_reco.h"
#include <pcl\features\normal_3d.h>
#include <pcl\pcl_base.h>
#include <iostream>
#include <fstream>
#include <pcl/io/io.h>
#include <pcl/io/pcd_io.h>
#include <pcl/point_types.h>
#include <pcl/point_cloud.h>
#include <pcl\visualization\cloud_viewer.h>
#include <string>
#include <stdio.h>      /* printf, fgets */
#include <stdlib.h>  
template class PFHShapeReco<pcl::PointXYZ>;
//template class PFHShapeReco<pcl::PointXYZRGB>;
//template class PFHShapeReco<pcl::PointXYZRGBA>;
int main(int argc, char *argv[]) {
	using namespace std;
	pcl::PointCloud<pcl::PointXYZ>::Ptr cloud(new pcl::PointCloud<pcl::PointXYZ>);
	int s4 = atoi(argv[4]);
	int s0 = atoi(argv[2]);
	char s7[] = "\\p";
	char s8[] = "-g";
	char s9[] = "-s";
	char pcd[] = ".pcd";
	char txt[] = ".txt";
	
	for (; s4 > 0; s4--) {
		int s10 = s4;
		s10--;
		char* n_dir = new char[512];
		char* x_dir = new char[512];
		char* s5 =new char [512];
		sprintf_s(n_dir, 512, "%s%s%s%s%s%s%d%s",  argv[1], s7, argv[2],s8, argv[3], s9, s10,pcd);
//		printf("%s%s%d%s%s%s%d%s",  argv[5], s7, s0,s8, argv[3], s9,s4,pcd );
//	Sleep(10000);
		sprintf_s(x_dir, 512, "%s%s%s%s%s%s%d%s",  argv[5], s7, argv[2],s8, argv[3], s9, s10,txt);
		pcl::io::loadPCDFile(n_dir, *cloud);
		if (cloud->size() > 300) {//Mindestgröße der PC zur Berechnung des Deskriptors

			pcl::NormalEstimation<pcl::PointXYZ, pcl::Normal> normal_estimation;
			normal_estimation.setInputCloud(cloud);

			pcl::search::KdTree<pcl::PointXYZ>::Ptr kdtree(new pcl::search::KdTree<pcl::PointXYZ>);
			normal_estimation.setSearchMethod(kdtree);

			pcl::PointCloud<pcl::Normal>::Ptr normals(new pcl::PointCloud< pcl::Normal>);
			normal_estimation.setRadiusSearch(0.03);
			normal_estimation.compute(*normals);

			PFHShapeReco<pcl::PointXYZ> pfh;
			pfh.ComputePFH(0.5, cloud, normals);//Berechnung des Deskriptors
			std::ofstream file;
			
			file.open(x_dir);
			for (int i = 0; i < 625; i++) {
				float f = pfh.GetObjectModel()[i];//Extraktion und Schreiben jedes Wertes in eine Datei

				file << f << ",";
			}

			file << 'g';//g wurde am Ende angefügt und gab die Klasse an (für das FANN Format) 
			file << std::endl;//Zeilenumbruch pro Sample
			
			file.close();
		}
	}
	return 0;
}
