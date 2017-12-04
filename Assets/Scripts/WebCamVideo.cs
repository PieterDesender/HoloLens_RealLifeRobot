using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;

public class WebCamVideo : MonoBehaviour {

    private MeshRenderer frame;

    [SerializeField]
    public string sourceURL = "http://192.168.1.1:8080/?action=stream";
    private Texture2D _texture;
    private Stream _stream;
    private bool _streamReady = false;

    OperatingMode _currentOperatingModePrevFrame = OperatingMode.MovementMode;

    private GameObject _uiCamTexPlaneBig = null;
    private GameObject _uiCamTexPlaneSmall = null;
    void Start () {
        _uiCamTexPlaneBig = ManagerObjects.Instance().GetObject(EnumObjects.UICamPlaneBig);
        _uiCamTexPlaneSmall = ManagerObjects.Instance().GetObject(EnumObjects.UICamPlaneSmall);

        _currentOperatingModePrevFrame = CarInformation.Instance().GetOperatingMode();

        Connect();

        if (CarInformation.Instance().GetOperatingMode() == OperatingMode.CameraMode)
            frame = _uiCamTexPlaneBig.GetComponent<MeshRenderer>();
        else
            frame = _uiCamTexPlaneSmall.GetComponent<MeshRenderer>();
    }

#if UNITY_EDITOR
    private void Connect()
#else
    private async void Connect()
#endif
    {
        _texture = new Texture2D(2, 2);
        // create HTTP request
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sourceURL);
        //Optional (if authorization is Digest)
        req.Credentials = new NetworkCredential("root", "admin");
        // get response
#if UNITY_EDITOR
        WebResponse resp = req.GetResponse();
#else
         WebResponse resp = await req.GetResponseAsync();
#endif
        _stream = resp.GetResponseStream();
        _streamReady = true;
    }

    private void Update()
    {
        int length = 99999;
        Byte[] JpegData = new Byte[99999];

        if (_streamReady)
        {
            int bytesToRead = FindLength(_stream);
            if (bytesToRead == -1)
                return;

            int leftToRead = bytesToRead;

            while (leftToRead > 0)
                leftToRead -= _stream.Read(JpegData, bytesToRead - leftToRead, leftToRead);

            MemoryStream ms = new MemoryStream(JpegData, 0, bytesToRead, false, true);

#if UNITY_EDITOR
            byte[] buffer = ms.GetBuffer();
#else
            ArraySegment<byte> seg;
            ms.TryGetBuffer(out seg);
            byte[] buffer = seg.Array;
#endif

            _texture.LoadImage(buffer);
            frame.material.mainTexture = _texture;
            _stream.ReadByte(); // CR after bytes
            _stream.ReadByte(); // LF after bytes

            Array.Clear(JpegData, 0, length);
        }

        CheckModeChange();

    }

    int FindLength(Stream stream)
    {
        int b;
        string line = "";
        int result = -1;
        bool atEOL = false;

        while ((b = stream.ReadByte()) != -1)
        {
            if (b == 10) continue; // ignore LF char
            if (b == 13)
            { // CR
                if (atEOL)
                {  // two blank lines means end of header
                    stream.ReadByte(); // eat last LF
                    return result;
                }
                if (line.StartsWith("Content-Length:"))
                {
                    line = line.Substring("Content-Length: ".Length).Trim();
                    result = Convert.ToInt32(line);
                }
                else
                {
                    line = "";
                }
                atEOL = true;
            }
            else
            {
                atEOL = false;
                line += (char)b;
            }
        }
        return -1;
    }

    private void CheckModeChange()
    {
        OperatingMode currentOperatingMode = CarInformation.Instance().GetOperatingMode();
        if (currentOperatingMode != _currentOperatingModePrevFrame)
        {
            if (CarInformation.Instance().GetOperatingMode() == OperatingMode.CameraMode)
                frame = _uiCamTexPlaneBig.GetComponent<MeshRenderer>();
            else
                frame = _uiCamTexPlaneSmall.GetComponent<MeshRenderer>();
        }
        _currentOperatingModePrevFrame = currentOperatingMode;
    }
}
