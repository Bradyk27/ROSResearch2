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
// Publish correctly orientated mesh
    //Orient correctly, color, size, etc.
        //Get in form recognized by custom vertex shader. That can do color. Look at their code
//Voxel cloud CAN be loaded AND generates triangles, but is much slower. For some reason only publishes when Rviz is up (fixing this would def save some processing power). Also, might have to comment out the topology change

using UnityEngine;
using System;
using UnityEditor;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class PointcloudSubscriber : UnitySubscriber<MessageTypes.Sensor.PointCloud2>
    {
        //Mesh variables
        public RgbPoint3[] Points;
        private Mesh mesh;
        public MeshRenderer meshRenderer;
        Vector3[] vertices;
        int[] triangles;

        //Subscription variable
        private bool isMessageReceived = false;

        protected override void Start()
        {   
            Debug.Log("Start\n");
            //Start Unity Subscriber
            base.Start();
            //Create empty new mesh for points
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            //meshRenderer.material = new Material(Shader.Find("Custom/VertexColor"));
        }

        private void Update() //Function to update when new messages are received
        {
            if(isMessageReceived){
                ProcessMessage();
            }
        }
        
        protected override void ReceiveMessage(MessageTypes.Sensor.PointCloud2 message) //Automatically called when new mesage received
        {
            Debug.Log("ReceiveMessage\n");
            //Processes message, transforms into form Unity can understand
            //NOTE: Pointcloud.cs methods could be implemented here (depth & image)
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
            int[] indices = new int[vertices.Length];
            try {
                mesh.Clear();
            } catch (Exception e) {
                Debug.Log(e);
            }
            mesh.vertices = vertices;
            for(int i = 0; i < mesh.vertices.Length; i++){
                indices[i] = i;
            }
            
            //Graphs mesh as points. Need to consider color. Works with /rtabmap/cloud_map & voxel_cloud
            mesh.SetIndices(indices, MeshTopology.Points, 0);
            mesh.RecalculateBounds();
            AssetDatabase.CreateAsset(mesh, "Assets/testMesh.asset");
            AssetDatabase.SaveAssets();
            isMessageReceived = false; //Resets and waits for new message
        }
    }
}