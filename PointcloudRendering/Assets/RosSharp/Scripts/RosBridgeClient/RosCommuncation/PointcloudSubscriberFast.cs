/*
.______    __  ___ .______       __    __       _______. _______ 
|   _  \  |  |/  / |   _  \     |  |  |  |     /       ||   ____|
|  |_)  | |  '  /  |  |_)  |    |  |  |  |    |   (----`|  |__   
|   _  <  |    <   |      /     |  |  |  |     \   \    |   __|  
|  |_)  | |  .  \  |  |\  \----.|  `--'  | .----)   |   |  |____ 
|______/  |__|\__\ | _| `._____| \______/  |_______/    |_______|
                                                                */

//Adapted from https://answers.ros.org/question/339483/ros-sharp-unity3d-import-pointcloud2/ & https://github.com/siemens/ros-sharp/blob/master/Libraries/RosBridgeClient/PointCloud.cs
// Points cannot be assigned to for whatever stupid reason
    // Might just consider using a vertex3
// Then, I need to see if the calculation is actually working

using UnityEngine;
using System;
using UnityEditor;
using RosSharp.RosBridgeClient.MessageTypes.Sensor;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]

    public class PointcloudSubscriberFast : MonoBehaviour
    { 
        //Mesh variables
        public RgbPoint3[] Points;
        private Mesh mesh;
        public MeshRenderer meshRenderer;
        Vector3[] vertices;
        Color[] colors;
        public Image colorImage;
        public Image depthImage;
        public ImageSubscriberFull colorImageSubscriber;
        public ImageSubscriberFull depthImageSubscriber;


        void Start()
        {
            //Debug.Log("Start\n");
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            meshRenderer.material = new Material(Shader.Find("Custom/VertexColor"));
        }

        void Update() //Need to consider how when and how things are updated
        {
            if(colorImageSubscriber.pointcloudReady && depthImageSubscriber.pointcloudReady && colorImageSubscriber.generationReady && depthImageSubscriber.generationReady)
            {
                GeneratePointcloud();
            }
        }

        private void GeneratePointcloud()
        {   
            colorImageSubscriber.generationReady = false;
            depthImageSubscriber.generationReady = false;
            Debug.Log("Started Generation");
            try{
                float focal = 1.0f;
                colorImage = colorImageSubscriber.image;
                depthImage = depthImageSubscriber.image;
                uint width = depthImage.width;
                uint height = depthImage.height;
                float invFocal = 1.0f / focal; //Remove focal variable

                //Debug.Log(string.Join("\n", depthImage.data)); (I am, in fact, receiving something here)

                RgbPoint3[] Points = new RgbPoint3[width * height];
                Points[0].x = 0.0F; //This is the problem. No idea why on EARTH Points can't be assigned to. Possible name error? Points being used in the other subscriber too?
                Points[0].y = 0.0F;
                Points[0].z = 0.0F;
                Debug.Log("Points Length: " + Points.Length);
                for (uint v = 0; v < height; v++)
                {
                    for (uint u = 0; u < width; u++) //U not incrementing...image actually being received? Stream crashing and the script is freezing?
                    {
                        //Debug.Log("W: " + width);
                        //Debug.Log("H: " + height);
                        //Debug.Log("U: " + u);
                        //Debug.Log("V: " + v);
                        //Debug.Log("W*H: " + (width*height));
                        //Debug.Log("U*V: " + ((u+1)*(v+1)));
                        //double percent_complete = ((double)(u*v) / (double)(width*height)) * 100.0f;
                        //Debug.Log("Percent Complete: " + percent_complete.ToString("0.000000000000###"));

                        float depth = depthImage.data[u*v]; //Gotta figure out what this is and how to set it.
                        //Debug.Log("Depth: " + depth);
                        if (depth == 0)
                        {
                            //Points[u + v * width].x = float.NaN;
                            //Points[u + v * width].y = float.NaN;
                            //Points[u + v * width].z = float.NaN;
                            //Points[u + v * width].rgb = new int[] { 0, 0, 0 };
                        }
                        else
                        {
                            // Points[u + v * width].z = depth * invFocal;
                            // Points[u + v * width].x = u * depth * invFocal;
                            // Points[u + v * width].y = v * depth * invFocal;
                            // Points[u + v * width].rgb = new int[] { 0, 0, 0 };// rgbImage[u, v]; // Gotta figure out what this is and how to set it as well
                        }
                    }
                }


                Debug.Log("Pointcloud being generated!");
                vertices = new Vector3[Points.Length];
                colors = new Color[Points.Length];
                for (var i = 0; i < Points.Length; i++)
                {
                    vertices[i].x = Points[i].x;
                    vertices[i].y = Points[i].z;
                    vertices[i].z = Points[i].y;
                    colors[i].r = (float)((float)Points[i].rgb[0] / 255.0);
                    colors[i].g = (float)((float)Points[i].rgb[1] / 255.0);
                    colors[i].b = (float)((float)Points[i].rgb[2] / 255.0);
                    colors[i].a = 1.0F; 
                    
                    Debug.Log("Vertexes: " + Points[i].x + " " + Points[i].y + " " + Points[i].z + "\n");
                    Debug.Log("Vertex Colors: " + Points[i].rgb[0] + " " + Points[i].rgb[1] + " " + Points[i].rgb[2] + "\n");
                }

                mesh.Clear();
                mesh.vertices = vertices;
                mesh.colors = colors;

                //Graphs mesh as points
                int[] indices = new int[vertices.Length];
                for(int i = 0; i < mesh.vertices.Length; i++){
                    indices[i] = i;
                }
                mesh.SetIndices(indices, MeshTopology.Points, 0);
                mesh.RecalculateBounds();
                //AssetDatabase.CreateAsset(mesh, "Assets/testMeshColorRTABMap4.asset");
                //AssetDatabase.SaveAssets();
            }
            catch(Exception) {
                return;
            }

            colorImageSubscriber.generationReady = true;
            depthImageSubscriber.generationReady = true;
            colorImageSubscriber.pointcloudReady = false;
            depthImageSubscriber.pointcloudReady = false;

        }

    }   
}