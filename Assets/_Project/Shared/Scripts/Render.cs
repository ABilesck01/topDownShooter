using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render : MonoBehaviour
{
    private bool takeScreenshotOnNextFrame;
    private Camera cam;


    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        TakeScreenShot();
    }

    private void OnPostRender()
    {
        if(takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D result = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            result.ReadPixels(rect, 0, 0);

            byte[] bytes = result.EncodeToPNG();
            System.IO.File.WriteAllBytes($"{Application.dataPath}/CameraScreenshot_{Time.time}.png", bytes);
            Debug.Log($"Saved as {Application.dataPath}/CameraScreenshot_{Time.time}.png");
            RenderTexture.ReleaseTemporary(renderTexture);
            cam.targetTexture = null;
        }
    }

    [ContextMenu("Take screenshot")]
    private void TakeScreenShot()
    {
        cam.targetTexture = RenderTexture.GetTemporary(3840, 2160, 16);
        takeScreenshotOnNextFrame = true;
    }

}
