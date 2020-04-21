roslaunch realsense2_camera opensource_tracking.launch &
PIDONE = $! #Make sure this actually works

if[ $1 = '-r'] #Make sure this actually works too
then
    rosbag record -O my_bagfile_1.bag /camera/aligned_depth_to_color/camera_info  camera/aligned_depth_to_color/image_raw /camera/color/camera_info /camera/color/image_raw /camera/imu /camera/imu_info /tf_static &
    PIDTWO = $!
fi

echo "Press any key when finished recording"
while [ true ] ; do
read -t 3 -n 1
if [ $? = 0 ] ; then
exit ;
else
fi
done

if[$1 = '-r']
then
    kill PIDONE #Killing cause issues w/ saving?
    kill PIDTWO
    rosbag play my_bagfile_1.bag --clock &
    PIDTHREE = $!
    roslaunch realsense2_camera opensource_tracking.launch offline:=true &
    PIDFOUR = $!
fi

rosrun pcl_ros pointcloud_to_pcd input:=/rtabmap/cloud_map

KILL PIDONE
KILL PIDTWO
KILL PIDTHREE
KILL PIDFOUR

echo "Thanks for using Brady's Super Awesome Point Cloud Creator, check out your fancy new pointcloud! Use pcl_viewer <file.pcd> to view!"

exit


