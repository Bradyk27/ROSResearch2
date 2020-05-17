/*
.______    __  ___ .______       __    __       _______. _______ 
|   _  \  |  |/  / |   _  \     |  |  |  |     /       ||   ____|
|  |_)  | |  '  /  |  |_)  |    |  |  |  |    |   (----`|  |__   
|   _  <  |    <   |      /     |  |  |  |     \   \    |   __|  
|  |_)  | |  .  \  |  |\  \----.|  `--'  | .----)   |   |  |____ 
|______/  |__|\__\ | _| `._____| \______/  |_______/    |_______|
                                                                */


using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Sensor;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class ImageSubscriberFull : UnitySubscriber<MessageTypes.Sensor.Image> //Better way to do this, probably be some version of ID
    {
        public Image image;
        private bool isMessageReceived;

        public MeshRenderer meshRenderer;
        private Texture2D texture2D;
        private byte[] imageData;

        public bool pointcloudReady;
        public bool generationReady;

        protected override void Start()
        {
			//Debug.Log("Start\n");
			base.Start();
            generationReady = true;

            // texture2D = new Texture2D(1, 1);
            // meshRenderer.material = new Material(Shader.Find("Standard"));
            
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.Image Image)
        {   
            Debug.Log("ReceiveMessage\n");
            image = Image;
            isMessageReceived = true;

            // imageData = Image.data;
        }

        private void ProcessMessage()
        {
            Debug.Log("ProcessMessage\n");
            isMessageReceived = false;
            pointcloudReady = true;

            // texture2D.LoadRawTextureData(imageData);
            // texture2D.Apply();
            // meshRenderer.material.SetTexture("_MainTex", texture2D);
            
        }

    }
}

