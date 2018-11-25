using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QRScanner : MonoBehaviour
{

    private WebCamTexture camTexture;
    private Rect screenRect;
    private Button gbutton;
    public Texture scanner;

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
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        GUI.Box(new Rect(Screen.width / 2.75f, Screen.height / 5.0f, 400, 400), scanner, style);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake   
       
        if (GUI.Button(new Rect((Screen.width / 2.5f), Screen.height / 1.25f, 300, 100), "Scan QR"))
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
                camTexture.Stop();
                SceneManager.LoadScene(result.Text);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }
}
