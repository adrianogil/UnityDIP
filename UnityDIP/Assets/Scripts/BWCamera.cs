using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWCamera : MonoBehaviour {

	public Material mat;

    public RenderTexture renderTexture;

    private bool alreadyInitialized = false;
    private bool alreadyGotFirstFrame = false;

    void Start()
    {
        // renderTexture = new RenderTexture(200,200);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
        // Graphics.Blit(dest, renderTexture);

        // alreadyGotFirstFrame = true;
    }

    void Update()
    {
        if (!alreadyGotFirstFrame) return;

        if (!alreadyInitialized)
        {

        }
    }
}
