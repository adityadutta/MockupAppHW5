using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QRScanner : MonoBehaviour {

    private WebCamTexture camTexture;
    private Rect screenRect;
    private Button gbutton;
    public Texture scanner;
    private Texture scannerImg;

    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        //GUI.Box(new Rect(0.0f, 0.0f, Screen.width, Screen.height), scanner);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake   
        if (GUI.Button(new Rect((Screen.width / 2.0f), Screen.height / 1.25f, 200, 100), "Scan QR"))
        {
            Debug.Log("Scanning QR");
            ScanQR();
        }

    }                                       

    private void ScanQR()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(),
              camTexture.width, camTexture.height);
            if (result != null)
            {
                Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                ScreenCapture.CaptureScreenshot("ss1");
                camTexture.Stop();
                SceneManager.LoadScene(result.Text);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }
}
