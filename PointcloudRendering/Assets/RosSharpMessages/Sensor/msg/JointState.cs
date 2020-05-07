/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



using RosSharp.RosBridgeClient.MessageTypes.Std;

namespace RosSharp.RosBridgeClient.MessageTypes.Sensor
{
    public class JointState : Message
    {
        public const string RosMessageName = "sensor_msgs/JointState";

        //  This is a message that holds data to describe the state of a set of torque controlled joints.
        // 
        //  The state of each joint (revolute or prismatic) is defined by:
        //   * the position of the joint (rad or m),
        //   * the velocity of the joint (rad/s or m/s) and
        //   * the effort that is applied in the joint (Nm or N).
        // 
        //  Each joint is uniquely identified by its name
        //  The header specifies the time at which the joint states were recorded. All the joint states
        //  in one message have to be recorded at the same time.
        // 
        //  This message consists of a multiple arrays, one for each part of the joint state.
        //  The goal is to make each of the fields optional. When e.g. your joints have no
        //  effort associated with them, you can leave the effort array empty.
        // 
        //  All arrays in this message should have the same size, or be empty.
        //  This is the only way to uniquely associate the joint name with the correct
        //  states.
        public Header header { get; set; }
        public string[] name { get; set; }
        public double[] position { get; set; }
        public double[] velocity { get; set; }
        public double[] effort { get; set; }

        public JointState()
        {
            this.header = new Header();
            this.name = new string[0];
            this.position = new double[0];
            this.velocity = new double[0];
            this.effort = new double[0];
        }

        public JointState(Header header, string[] name, double[] position, double[] velocity, double[] effort)
        {
            this.header = header;
            this.name = name;
            this.position = position;
            this.velocity = velocity;
            this.effort = effort;
        }
    }
}