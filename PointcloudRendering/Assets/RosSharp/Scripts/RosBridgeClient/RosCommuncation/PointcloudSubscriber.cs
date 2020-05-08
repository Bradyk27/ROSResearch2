/*
.______    __  ___ .______       __    __       _______. _______ 
|   _  \  |  |/  / |   _  \     |  |  |  |     /       ||   ____|
|  |_)  | |  '  /  |  |_)  |    |  |  |  |    |   (----`|  |__   
|   _  <  |    <   |      /     |  |  |  |     \   \    |   __|  
|  |_)  | |  .  \  |  |\  \----.|  `--'  | .----)   |   |  |____ 
|______/  |__|\__\ | _| `._____| \______/  |_______/    |_______|
                                                                */

//Adapted from https://answers.ros.org/question/339483/ros-sharp-unity3d-import-pointcloud2/ & https://github.com/siemens/ros-sharp/blob/master/Libraries/RosBridgeClient/PointCloud.cs
// To Dos:
// Publish correclty orientated mesh
    //Review some guides & other subscribers to figure out how to best do this
    //How does Unity actually DO this? Like...what object do I apply this script to?
    //Reference Odometry & LaserScan guides
    //How does this all fit with rosconnector and subscriptions and such?
// RGBpoint3 form?
// Debug.Log() publishing place?
//Can any of the other assets I've imported be used here?

using UnityEngine;
using System;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class PointcloudSubscriber : UnitySubscriber<MessageTypes.Sensor.PointCloud2>
    {
        //Mesh variables
        public RgbPoint3[] Points;
        private Mesh mesh;
        Vector3[] vertices;

        //Subscription variable
        private bool isMessageReceived;

        protected override void Start()
        {   
            Debug.Log("Start\n");
            //Start Unity Subscriber
            base.Start();
            //Create empty new mesh for points
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        private void Update() //Function to update when new messages are received
        {
            Debug.Log("Update\n");
            if(isMessageReceived){
                ProcessMessage();
            }
        }
        
        protected override void ReceiveMessage(MessageTypes.Sensor.PointCloud2 message) //Automatically called when new mesage received
        {
            Debug.Log("ReceiveMessage\n");
            //Processes message, transforms into form Unity can understand
            //NOTE: Pointcloud.cs methods could be implemented here
            long I = message.data.Length / message.point_step;
            RgbPoint3[] Points = new RgbPoint3[I];
            byte[] byteSlice = new byte[message.point_step];
            for (long i = 0; i < I; i++)
            {
                Array.Copy(message.data, i * message.point_step, byteSlice, 0, message.point_step);
                Points[i] = new RgbPoint3(byteSlice, message.fields);
            }
            vertices = new Vector3[I];

            for (var i = 0; i < I; i++)
            {
                vertices[i].x = Points[i].x;
                vertices[i].y = Points[i].z;
                vertices[i].z = Points[i].y;
            }
            isMessageReceived = true;
        }
    
        private void ProcessMessage() //Clears mesh and loads new vertices
        {
            Debug.Log("ProcessMessage\n");
            try {
                mesh.Clear();
            } catch (Exception e) {
                Debug.Log(e);
            }
            mesh.vertices = vertices;
            isMessageReceived = false; //Resets and waits for new message
        }
    }
}