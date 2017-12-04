using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceResponces : MonoBehaviour
{
    private GameObject _uiHolographicMove = null;
    private GameObject _uiHolograpgicCamera = null;

    private GameObject _controlMove = null;
    private GameObject _controlCamera = null;

    private TextMesh _uiHolographicSpeesTxt = null;

	void Start ()
	{
	    _uiHolograpgicCamera = ManagerObjects.Instance().GetObject(EnumObjects.UIHolographicCamera);
	    _uiHolographicMove = ManagerObjects.Instance().GetObject(EnumObjects.UIHolographicMove);
	    _controlMove = ManagerObjects.Instance().GetObject(EnumObjects.controlMove);
	    _controlCamera = ManagerObjects.Instance().GetObject(EnumObjects.controlCamera);
        _uiHolographicSpeesTxt = ManagerObjects.Instance().GetObject(EnumObjects.UITxtSpeed).GetComponent<TextMesh>();
	}

    public void VoiceResponceMove()
    {
        if (CarInformation.Instance().GetOperatingMode() == OperatingMode.MovementMode)
            return;

        VoiceResponceResetCamera();

        CarInformation.Instance().ChangeOperatingMode();

        _uiHolographicMove.SetActive(true);
        _controlMove.SetActive(true);
        _uiHolograpgicCamera.SetActive(false);
        _controlCamera.SetActive(false);
    }

    public void VoiceResponceCamera()
    {
        if (CarInformation.Instance().GetOperatingMode() == OperatingMode.CameraMode)
            return;

        CarInformation.Instance().ChangeOperatingMode();

        _uiHolographicMove.SetActive(false);
        _controlMove.SetActive(false);
        _uiHolograpgicCamera.SetActive(true);
        _controlCamera.SetActive(true);
    }

    public void VoiceResponceQuit()
    {
        Application.Quit();
    }

    public void VoiceResponceFaster()
    {
        if (!CarInformation.Instance().IncreaseSpeed())
            return;

        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeSpeed(CarInformation.Instance().GetSpeed(), EnumEngine.EngineLeft));
        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeSpeed(CarInformation.Instance().GetSpeed(), EnumEngine.EngineRight));

        _uiHolographicSpeesTxt.text = CarInformation.Instance().GetSpeed().ToString();
    }

    public void VoiceResponceSlower()
    {
        if (!CarInformation.Instance().DecreaseSpeed())
            return;

        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeSpeed(CarInformation.Instance().GetSpeed(), EnumEngine.EngineLeft));
        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeSpeed(CarInformation.Instance().GetSpeed(), EnumEngine.EngineRight));

        _uiHolographicSpeesTxt.text = CarInformation.Instance().GetSpeed().ToString();
    }

    public void VoiceResponceChangeLight()
    {
        bool light = CarInformation.Instance().ChangeLight();
        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeLight(light));
    }

    public void VoiceResponceResetCamera()
    {
        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeServoInit());
        CarInformation.Instance().InitCradle();
    }

    public void VoiceResponceLockCamera()
    {
        Network_Controller_Tcp.Instance().SendBytes(Network_Encoder_Decoder.Instance().EncodeServoLock());
        CarInformation.Instance().InitCradle();
    }
}
