/*
.______    __  ___ .______       __    __       _______. _______ 
|   _  \  |  |/  / |   _  \     |  |  |  |     /       ||   ____|
|  |_)  | |  '  /  |  |_)  |    |  |  |  |    |   (----`|  |__   
|   _  <  |    <   |      /     |  |  |  |     \   \    |   __|  
|  |_)  | |  .  \  |  |\  \----.|  `--'  | .----)   |   |  |____ 
|______/  |__|\__\ | _| `._____| \______/  |_______/    |_______|
                                                                */

//Adapted from https://answers.ros.org/question/339483/ros-sharp-unity3d-import-pointcloud2/ & https://github.com/siemens/ros-sharp/blob/master/Libraries/RosBridgeClient/PointCloud.cs
//Need to subscribe to TWO streams at once. Might need to do two classes that both output to a global variable
//Compressed image *may* not work with the pointcloud generator. We shall see. Can't seem to subscribe to straight IMAGE streams though, which is annoying.

using UnityEngine;
using System;
using UnityEditor;
using RosSharp.RosBridgeClient.MessageTypes.Sensor;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class ImageSubscriberDepth : UnitySubscriber<MessageTypes.Sensor.Image> //There's a better way to do this
    {
        public Image depthImage;
        private bool isMessageReceived;

        public void StartTransfer()
        {
            Debug.Log("DepthStart\n");
			base.Start();
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.Image Image)
        {   
            Debug.Log("ReceiveMessageDepth\n");
            depthImage = Image;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {   
            Debug.Log("ProcessMessageDepth\n");
            isMessageReceived = false;
        }
    }

    public class ImageSubscriberColor : UnitySubscriber<MessageTypes.Sensor.Image> //There's a better way to do this
    {
        public Image colorImage;
        private bool isMessageReceived;

        public void StartTransfer()
        {   
            Debug.Log("ColorStart");
			base.Start();
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.Image Image)
        {   
            Debug.Log("ReceiveMessageColor\n");
            colorImage = Image;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Debug.Log("ProcessMessageColor\n");
            isMessageReceived = false;
        }
    }


    public class PointcloudSubscriberFast : MonoBehaviour
    { 
        //Mesh variables
        public RgbPoint3[] Points;
        private Mesh mesh;
        public MeshRenderer meshRenderer;
        Vector3[] vertices;
        Color[] colors;
        public ImageSubscriberColor colorImage;
        public ImageSubscriberDepth depthImage;

        void Start()
        {
            Debug.Log("Start\n");
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            meshRenderer.material = new Material(Shader.Find("Custom/VertexColor"));
            colorImage = new ImageSubscriberColor();
            colorImage.StartTransfer(); //Probably need to manually force classes to receive messages & update and stuff. Actually there's surely a better way to do this...
            //READ CONSOLE WARNING ABOUT NEW COMMAND
            //FIGURE OUT HOW TO PASS VARIABLES BETWEEN SCRIPTS            
            depthImage = new ImageSubscriberDepth();

        }

        void Update() //Need to consider how when and how things are updated
        {
            GeneratePointcloud();
        }

        private void GeneratePointcloud()
        {
            // uint width = depthImage.depthImage.width;
            // uint height = depthImage.depthImage.height;
            // float invFocal = 1.0f; //Remove focal variable

            // Points = new RgbPoint3[width * height];

            // for (uint v = 0; v < height; v++)
            // {
            //     for (uint u = 0; u < width; u++)
            //     {
            //         float depth = 0;// depthImage[u, v]; //It appears this is just being filled with NaN types. Why?
            //         if (depth == 0)
            //         {
            //             Points[u + v * width].x = float.NaN;
            //             Points[u + v * width].y = float.NaN;
            //             Points[u + v * width].z = float.NaN;
            //             Points[u + v * width].rgb = new int[] { 0, 0, 0 };
            //         }
            //         else
            //         {
            //             Points[u + v * width].z = depth * invFocal;
            //             Points[u + v * width].x = u * depth * invFocal;
            //             Points[u + v * width].y = v * depth * invFocal;
            //             Points[u + v * width].rgb = new int[] { 0, 0, 0 };// rgbImage[u, v];
            //         }
            //     }
            // }

            // vertices = new Vector3[Points.Length];
            // colors = new Color[Points.Length];
            // for (var i = 0; i < Points.Length; i++)
            // {
            //     vertices[i].x = Points[i].x;
            //     vertices[i].y = Points[i].z;
            //     vertices[i].z = Points[i].y;
            //     colors[i].r = (float)((float)Points[i].rgb[0] / 255.0);
            //     colors[i].g = (float)((float)Points[i].rgb[1] / 255.0);
            //     colors[i].b = (float)((float)Points[i].rgb[2] / 255.0);
            //     colors[i].a = 1.0F; 
            //     //Debug.Log("Colors: " + colors[i].ToString());
            //     //Debug.Log("Vertex Colors: " + Points[i].rgb[0] + " " + Points[i].rgb[1] + " " + Points[i].rgb[2] + "\n");
            // }

            // mesh.Clear(); //Removed try / catch loop
            // mesh.vertices = vertices;
            // mesh.colors = colors;

            // //Graphs mesh as points. Works with /rtabmap/cloud_map & /voxel_cloud
            // int[] indices = new int[vertices.Length];
            // for(int i = 0; i < mesh.vertices.Length; i++){
            //     indices[i] = i;
            // }
            // mesh.SetIndices(indices, MeshTopology.Points, 0);
            // mesh.RecalculateBounds();
            // //AssetDatabase.CreateAsset(mesh, "Assets/testMeshColorRTABMap4.asset");
            // //AssetDatabase.SaveAssets();

        }

    }   
}