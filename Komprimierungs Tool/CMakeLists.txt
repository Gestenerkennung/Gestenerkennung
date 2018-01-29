cmake_minimum_required(VERSION 2.8)
project(pc2)
set(PCL_DIR "C:\\Program Files\\PCL 1.8.1\\cmake\\PCLConfig.cmake")
find_package(PCL 1.8.1 REQUIRED)
add_executable(pc2 pfh_shape_reco.cpp)


  # Additional Include Directories
  # [C/C++]>[General]>[Additional Include Directories]
  include_directories( ${PCL_INCLUDE_DIRS} )
 
  # Preprocessor Definitions
  # [C/C++]>[Preprocessor]>[Preprocessor Definitions]
  add_definitions( ${PCL_DEFINITIONS} )
  #add_definitions( -DPCL_NO_PRECOMPILE )
 
  # Additional Library Directories
  # [Linker]>[General]>[Additional Library Directories]
  link_directories( ${PCL_LIBRARY_DIRS} )
 
  # Additional Dependencies
  # [Linker]>[Input]>[Additional Dependencies]
  target_link_libraries( pc2 ${PCL_LIBRARIES} ${PCL_COMMON_LIBRARIES} ${PCL_IO_LIBRARIES}${VTK_LIBRARIES})


