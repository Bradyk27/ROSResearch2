# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.16

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /opt/local/bin/cmake

# The command to remove a file.
RM = /opt/local/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /Users/admin/desktop/ConvertPCD

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /Users/admin/desktop/ConvertPCD/build

# Include any dependencies generated for this target.
include CMakeFiles/pcd2ply.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/pcd2ply.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/pcd2ply.dir/flags.make

CMakeFiles/pcd2ply.dir/pcd2ply.cpp.o: CMakeFiles/pcd2ply.dir/flags.make
CMakeFiles/pcd2ply.dir/pcd2ply.cpp.o: ../pcd2ply.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/Users/admin/desktop/ConvertPCD/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/pcd2ply.dir/pcd2ply.cpp.o"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/pcd2ply.dir/pcd2ply.cpp.o -c /Users/admin/desktop/ConvertPCD/pcd2ply.cpp

CMakeFiles/pcd2ply.dir/pcd2ply.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/pcd2ply.dir/pcd2ply.cpp.i"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /Users/admin/desktop/ConvertPCD/pcd2ply.cpp > CMakeFiles/pcd2ply.dir/pcd2ply.cpp.i

CMakeFiles/pcd2ply.dir/pcd2ply.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/pcd2ply.dir/pcd2ply.cpp.s"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /Users/admin/desktop/ConvertPCD/pcd2ply.cpp -o CMakeFiles/pcd2ply.dir/pcd2ply.cpp.s

# Object files for target pcd2ply
pcd2ply_OBJECTS = \
"CMakeFiles/pcd2ply.dir/pcd2ply.cpp.o"

# External object files for target pcd2ply
pcd2ply_EXTERNAL_OBJECTS =

pcd2ply: CMakeFiles/pcd2ply.dir/pcd2ply.cpp.o
pcd2ply: CMakeFiles/pcd2ply.dir/build.make
pcd2ply: /usr/local/lib/libpcl_io.dylib
pcd2ply: /opt/local/lib/libboost_system-mt.dylib
pcd2ply: /opt/local/lib/libboost_filesystem-mt.dylib
pcd2ply: /opt/local/lib/libboost_date_time-mt.dylib
pcd2ply: /opt/local/lib/libboost_iostreams-mt.dylib
pcd2ply: /opt/local/lib/libboost_regex-mt.dylib
pcd2ply: /usr/local/lib/libpcl_octree.dylib
pcd2ply: /usr/local/lib/libpcl_common.dylib
pcd2ply: CMakeFiles/pcd2ply.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/Users/admin/desktop/ConvertPCD/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Linking CXX executable pcd2ply"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/pcd2ply.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/pcd2ply.dir/build: pcd2ply

.PHONY : CMakeFiles/pcd2ply.dir/build

CMakeFiles/pcd2ply.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/pcd2ply.dir/cmake_clean.cmake
.PHONY : CMakeFiles/pcd2ply.dir/clean

CMakeFiles/pcd2ply.dir/depend:
	cd /Users/admin/desktop/ConvertPCD/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /Users/admin/desktop/ConvertPCD /Users/admin/desktop/ConvertPCD /Users/admin/desktop/ConvertPCD/build /Users/admin/desktop/ConvertPCD/build /Users/admin/desktop/ConvertPCD/build/CMakeFiles/pcd2ply.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/pcd2ply.dir/depend

