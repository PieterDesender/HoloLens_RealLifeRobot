using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using System.Net;
using System.Net.Sockets;
#endif

#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
#endif

public class Network_Controller_Tcp : MySingleton<Network_Controller_Tcp>
{
    [SerializeField] private string _port = "2001";
    [SerializeField] private string _ip = "192.168.1.1";
    private bool _socketReady = false;

#if UNITY_EDITOR
    private TcpClient _tcpClient = null;
    private NetworkStream _netStream = null;
    private StreamWriter _socketWriter = null;
#endif

#if !UNITY_EDITOR
    private StreamSocket _socket;
    private Stream _streamOut;
#endif

    void Awake()
    {
        Application.runInBackground = true;
        Connect();
    }

#if UNITY_EDITOR
    private void Connect()
#else
    private async void Connect()
#endif
    {
        try
        {
#if UNITY_EDITOR
            _tcpClient = new TcpClient();
            int port = Int32.Parse(_port);
            _tcpClient.Connect(IPAddress.Parse(_ip), port);
            _netStream = _tcpClient.GetStream();
            _socketWriter = new StreamWriter(_netStream);
#else
            _socket = new StreamSocket();
            HostName serverHost = new HostName(_ip);
            await _socket.ConnectAsync(serverHost, _port);
            _streamOut = _socket.OutputStream.AsStreamForWrite();
#endif
            _socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    void Start()
    {
        if (!_socketReady)
            return;

        SendBytes(Network_Encoder_Decoder.Instance().EncodeSpeed(CarInformation.Instance().GetSpeed(), EnumEngine.EngineLeft));
        SendBytes(Network_Encoder_Decoder.Instance().EncodeSpeed(CarInformation.Instance().GetSpeed(), EnumEngine.EngineRight));
        ManagerObjects.Instance().GetObject(EnumObjects.UITxtSpeed).GetComponent<TextMesh>().text = CarInformation.Instance().GetSpeed().ToString();

        SendBytes(Network_Encoder_Decoder.Instance().EncodeServoLock());
    }

    void OnApplicationQuit()
    {
        if (!_socketReady)
            return;
#if UNITY_EDITOR
        if (_tcpClient != null)
        {
            _socketWriter.Close();
            _netStream.Close();
            _tcpClient.Close();
        }
#else
        _streamOut.Dispose();
        _socket.Dispose();
#endif
    }

    public void SendBytes(byte[] bytes)
    {
        if (!_socketReady)
            return;
#if UNITY_EDITOR
        _netStream.Write(bytes, 0, bytes.Length);
#else
        _streamOut.Write(bytes, 0, bytes.Length);
        _streamOut.Flush();
#endif
    }
}
