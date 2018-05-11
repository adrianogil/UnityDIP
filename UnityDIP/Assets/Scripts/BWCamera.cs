using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BWCamera : MonoBehaviour {

    public Material mat;
	public Material cornerDetectionMat;

    public bool cornerDetectionActive = true;

    // public RenderTexture renderTexture;

    private bool alreadyInitialized = false;
    private bool alreadyGotFirstFrame = false;

    void Start()
    {
        // renderTexture = new RenderTexture(200,200);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
        if (cornerDetectionActive)
            Graphics.Blit(dest, dest, cornerDetectionMat);
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
