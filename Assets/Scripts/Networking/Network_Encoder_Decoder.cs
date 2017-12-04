using System;
using UnityEngine;

public enum EnumTypeEncoder{
    Movement = 0,
    Speed = 2,
    Servo = 3,
    Light = 4,
    LockServo = 32,
    InitServoAngle = 33
}

public enum EnumMovementEncoder
{
    Stop = 0,
    Forward = 1,
    Backward = 2,
    TurnLeft = 3,
    TurnRight = 4
}

public enum EnumEngine
{
    EngineLeft,
    EngineRight
}

public enum Servo
{
    ServoX,
    ServoY
}

public class Network_Encoder_Decoder : MySingleton<Network_Encoder_Decoder>
{
    //public void Decode(string toDecode)
    //{
    //    var stringArray = toDecode.Split('#');
    //    string typeString = stringArray[0];
    //    typeString = typeString.TrimStart('0');
    //    string dataString = stringArray[1];
    //    int typeInt = 0;

    //    if (Int32.TryParse(typeString, out typeInt))
    //    {
    //        switch ((EnumDecoderEncoder)typeInt)
    //        {
    //            case EnumDecoderEncoder.Speed:
    //                _uiMovementSpeed.GetComponent<TextMesh>().text = dataString;
    //                break;
    //            case EnumDecoderEncoder.Movement:
    //                break;
    //            case EnumDecoderEncoder.Quit:
    //                break;
    //            case EnumDecoderEncoder.Mode:
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Network decoder parse error");
    //    }
    //}

    public byte[] EncodeMovement(EnumMovementEncoder movement)
    {
        byte[] bytes = { 0xff, 0x00, 0x00, 0x00, 0xff };

        switch (movement)
        {
            case EnumMovementEncoder.Stop: break;
            case EnumMovementEncoder.Backward: bytes[2] = 0x01; break;
            case EnumMovementEncoder.Forward: bytes[2] = 0x02; break;
            case EnumMovementEncoder.TurnLeft: bytes[2] = 0x04; break;
            case EnumMovementEncoder.TurnRight: bytes[2] = 0x03; break;
            default:
                break;
        }
        return bytes;
    }

    public byte[] EncodeSpeed(byte speed, EnumEngine engine)
    {
        byte[] bytes = { 0xff, 0x02, 0x02, 0x00, 0xff };

        if (engine == EnumEngine.EngineLeft)
            bytes[2] = 0x01;

        bytes[3] = speed;

        return bytes;
    }

    public byte[] EncodeLight(bool light)
    {
        byte[] bytes = { 0xff, 0x04, 0x01, 0x00, 0xff };

        if (light)
            bytes[2] = 0x00;
        return bytes;
    }

    public byte[] EncodeServoLock()
    {
        byte[] bytes = { 0xff, 0x32, 0x00, 0x00, 0xff };
        return bytes;
    }

    public byte[] EncodeServoInit()
    {
        byte[] bytes = { 0xff, 0x33, 0x00, 0x00, 0xff };
        return bytes;
    }

    public byte[] EncodeServoValue(Servo servo, byte value)
    {
        byte[] bytes = { 0xff, 0x01, 0x07, 0x00, 0xff };

        if (servo == Servo.ServoY)
            bytes[2] = 0x08;

        bytes[3] = value;

        return bytes;
    }

}
