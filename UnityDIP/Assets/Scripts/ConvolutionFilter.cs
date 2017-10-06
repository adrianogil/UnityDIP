using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Convolution Filter Effect
public enum ConvFilterType
{
    CustomFilter,
    Identity,
    Blur,
    BottomSobel,
    Emboss,
    LeftSobel,
    Outline,
    RightSobel,
    Sharpen,
    TopSobel
}

public class ConvolutionFilter : MonoBehaviour {

    
    public ConvFilterType filterType;

    public float kernelFactor = 1f;

    public float distance = 1f;

    [HideInInspector]
    public float[] kernelweights = new float[9];

    private float[] identity = new float[9] {0,0,0, 0,1,0, 0,0,0};
    private float[] blur = new float[9] {0.0625f,0.125f,0.0625f, 0.125f,0.25f,0.125f, 0.0625f,0.125f,0.0625f};
    private float[] bottomSobel = new float[9] {-1f, -2f, -1f, 0f, 0f, 0f, 1f, 2f, 1f};
    private float[] emboss = new float[9] {-2f, -1f, 0f, -1f, 1f, 1f, 0f, 1f, 2f};
    private float[] leftSobel = new float[9] {1f, 0f, -1f, 2f, 0f, -2f, 1f, 0f, -1f};
    private float[] outline = new float[9] {-1f, -1f, -1f, -1f, 8f, -1f, -1f, -1f, -1f};
    private float[] rightSobel = new float[9] {-1f, 0f, 1f, -2f, 0f, 2f, -1f, 0f, 1f};
    private float[] sharpen = new float[9] {0f, -1f, 0f, -1f, 5f, -1f, 0f, -1f, 0f};
    private float[] topSobel = new float[9] {1f, 2f, 1f, 0f, 0f, 0f, -1f, -2f, -1f};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float[] GetCurrentKernelWeights()
    {
        if (filterType == ConvFilterType.Identity)
        {
            return identity;
        } else if (filterType == ConvFilterType.Blur)
        {
            return blur;
        } else if (filterType == ConvFilterType.BottomSobel)
        {
            return bottomSobel;
        } else if (filterType == ConvFilterType.Emboss)
        {
            return emboss;
        } else if (filterType == ConvFilterType.LeftSobel)
        {
            return leftSobel;
        } else if (filterType == ConvFilterType.Outline)
        {
            return outline;
        } else if (filterType == ConvFilterType.RightSobel)
        {
            return rightSobel;
        }  else if (filterType == ConvFilterType.Sharpen)
        {
            return sharpen;
        } else if (filterType == ConvFilterType.TopSobel)
        {
            return topSobel;
        } 

        return kernelweights;
    }

    public void ApplyFilter()
    {
        float[] weights = GetCurrentKernelWeights();

        float[] w = new float[weights.Length];
        for (int i = 0; i < w.Length; i++)
        {
            w[i] = kernelFactor * weights[i];
        }
        
        Debug.Log("GilLog - ConvolutionFilter::ApplyFilter - weights.Length " + weights.Length);

        Shader.SetGlobalFloatArray("_Kernel", w);
        Shader.SetGlobalFloat("_KernelLength", w.Length);
        Shader.SetGlobalFloat("_KernelDistance", distance);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ConvolutionFilter))]
public class ConvolutionFilterEditor : Editor {

    public override void OnInspectorGUI()
    {
        ConvolutionFilter filter = target as ConvolutionFilter;

        if (filter == null) return;

        float lastKernelFactor = filter.kernelFactor;

        base.OnInspectorGUI();

        if (lastKernelFactor != filter.kernelFactor)
        {
            filter.filterType = ConvFilterType.CustomFilter;
        }

        float gridItemWidth = 50f;
        int rows = 3;
        int cols = 3;

        float[] kernel = filter.GetCurrentKernelWeights();

        float[] kernelValues = new float[kernel.Length];
        for (int i = 0; i < kernelValues.Length; i++)
            kernelValues[i] = kernel[i];

        GUILayout.BeginVertical();
        for (int y = 0; y < rows; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < cols; x++)
            {
                kernelValues[x + y*cols] = EditorGUILayout.FloatField(GUIContent.none,
                                               kernelValues[x + y*cols], GUILayout.Width(gridItemWidth));
                if (kernelValues[x + y*cols] != kernel[x + y*cols])
                {
                    filter.filterType = ConvFilterType.CustomFilter;
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        if (filter.filterType == ConvFilterType.CustomFilter)
        {
            filter.kernelweights = kernelValues;
        } 
        else 
        {
            filter.kernelFactor = 1f;
        }

        if (GUILayout.Button("Apply filter"))
        {
            filter.ApplyFilter();
        }
    }

}
#endif
