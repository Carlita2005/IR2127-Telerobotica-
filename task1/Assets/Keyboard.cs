using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry; 

public class KeyboardRobotController : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "/cmd_vel";

    public float linearSpeed = 1.0f;  // velocidad hacia adelante
    public float angularSpeed = 1.0f; // velocidad de giro

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<TwistMsg>(topicName);
    }

    void Update()
    {
        float linear = 0f;
        float angular = 0f;

        // Control con teclado
        if (Input.GetKey(KeyCode.W)) linear = linearSpeed;
        if (Input.GetKey(KeyCode.S)) linear = -linearSpeed;
        if (Input.GetKey(KeyCode.A)) angular = angularSpeed;
        if (Input.GetKey(KeyCode.D)) angular = -angularSpeed;

        TwistMsg twist = new TwistMsg();
        twist.linear.x = linear;
        twist.linear.y = 0f;
        twist.linear.z = 0f;
        twist.angular.x = 0f;
        twist.angular.y = 0f;
        twist.angular.z = angular;

        ros.Publish(topicName, twist);
    }
}
