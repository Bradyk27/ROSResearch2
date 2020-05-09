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
    //Need triangles?
    //Material & shader on Mesh renderer?
    //Orient correctly, color, size, etc.
    //Mesh topology & setindices to render as points instead of triangles? Possible not working with operating system.
        //Look at pointcloud importer & look for ideas. You're getting the collection of points...just need to render them
        //Might have to add indices list which basically copies vertices in new form. What about color though...
//Can any of the other assets I've imported be used here?
//SamplePointCloud? May or may not be updating?

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
            
            //Debug.Log("Vertices:\n");
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
            
            Debug.Log("Mesh Vertices Length: " + mesh.vertices.Length);
            Debug.Log("Mesh Indices Length: " + indices.Length);
            //mesh.triangles = triangles;
            //Debug.Log("Mesh Triangles Length: " + mesh.triangles.Length);
            mesh.SetIndices(indices, MeshTopology.Points, 0);
            mesh.RecalculateBounds();
            isMessageReceived = false; //Resets and waits for new message
        }
    }
}