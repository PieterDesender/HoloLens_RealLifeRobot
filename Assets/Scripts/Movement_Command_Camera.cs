using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Command_Camera : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "SelectShape_F":
                if(CarInformation.Instance().IncreaseCradleY())
                    Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeServoValue(Servo.ServoY, CarInformation.Instance().GetCradleY()));
                break;
            case "SelectShape_R":
                if (CarInformation.Instance().DecreaseCradleX())
                    Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeServoValue(Servo.ServoX, CarInformation.Instance().GetCradleX()));
                break;
            case "SelectShape_L":
                if (CarInformation.Instance().IncreaseCradleX())
                    Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeServoValue(Servo.ServoX, CarInformation.Instance().GetCradleX()));
                break;
            case "SelectShape_B":
                if (CarInformation.Instance().DecreaseCradleY())
                    Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeServoValue(Servo.ServoY, CarInformation.Instance().GetCradleY()));
                break;
        }
    }
}
