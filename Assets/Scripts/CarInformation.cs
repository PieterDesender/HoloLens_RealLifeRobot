using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperatingMode
{
    MovementMode,
    CameraMode
}

public class CarInformation : MySingleton<CarInformation> {

    private byte _speed = 50;
    private OperatingMode _currentOperatingMode = OperatingMode.MovementMode;
    private bool _lightOn = true;

    private byte _cradleX = 90;
    private byte _cradleY = 90;

    private byte _speedMax = 100;

    public void ChangeOperatingMode()
    {
        if (_currentOperatingMode == OperatingMode.MovementMode)
            _currentOperatingMode = OperatingMode.CameraMode;
        else
            _currentOperatingMode = OperatingMode.MovementMode;
    }
    public bool IncreaseSpeed(byte increaseEffect = 10)
    {
        if (_speed <= _speedMax - increaseEffect)
        {
            _speed += increaseEffect;
            return true;
        }
        return false;
    }

    public bool DecreaseSpeed(byte decreaseEffect = 10)
    {
        if (_speed >= decreaseEffect)
        {
            _speed -= decreaseEffect;
            return true;
        }
        return false;
    }

    public byte GetSpeed()
    {
        return _speed;
    }

    public OperatingMode GetOperatingMode()
    {
        return _currentOperatingMode;
    }

    public bool ChangeLight()
    {
        _lightOn = !_lightOn;
        return _lightOn;
    }

    public bool GetLightOn()
    {
        return _lightOn;
    }

    public bool DecreaseCradleX(byte amount = 1)
    {
        if(_cradleX > 0)
        {
            _cradleX -= amount;
            return true;
        }
        return false;
    }

    public bool IncreaseCradleX(byte amount = 1)
    {
        if (_cradleX < 180)
        {
            _cradleX += amount;
            return true;
        }
        return false;
    }

    public byte GetCradleX()
    {
        return _cradleX;
    }

    public bool DecreaseCradleY(byte amount = 1)
    {
        if (_cradleY > 90)
        {
            _cradleY -= amount;
            return true;
        }
        return false;
    }

    public bool IncreaseCradleY(byte amount = 1)
    {
        if (_cradleY < 180)
        {
            _cradleY += amount;
            return true;
        }
        return false;
    }

    public byte GetCradleY()
    {
        return _cradleY;
    }

    public void InitCradle()
    {
        _cradleX = 90;
        _cradleY = 90;
    }
}
