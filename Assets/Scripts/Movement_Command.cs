using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Command : MonoBehaviour
{
    private bool _forward = false;
    private bool _left = false;
    private bool _right = false;
    private bool _back = false;

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "SelectShape_F":
                Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeMovement(EnumMovementEncoder.Forward));
                _forward = true;
                break;
            case "SelectShape_L":
                Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeMovement(EnumMovementEncoder.TurnLeft));
                _left = true;
                break;
            case "SelectShape_R":
                Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeMovement(EnumMovementEncoder.TurnRight));
                _right = true;
                break;
            case "SelectShape_B":
                Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeMovement(EnumMovementEncoder.Backward));
                _back = true;
                break;
        }
    }
    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "SelectShape_F":
                _forward = false;
                break;
            case "SelectShape_L":
                _left = false;
                break;
            case "SelectShape_R":
                _right = false;
                break;
            case "SelectShape_B":
                _back = false;
                break;
        }

        if (!_forward && !_back && !_right && !_left)
        {
            Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeMovement(EnumMovementEncoder.Stop));
        }
    }
}
