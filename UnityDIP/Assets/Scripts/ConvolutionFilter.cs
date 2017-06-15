using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ConvFilterType
{
    CustomFilter
}

public class ConvolutionFilter : MonoBehaviour {

    
    public ConvFilterType filterType;

    [HideInInspector]
    public float[] kernelweights = new float[9];


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float[] GetCurrentKernelWeights()
    {
        return kernelweights;
    }

    public void ApplyFilter()
    {
        
        Material material = GetComponent<MeshRenderer>().sharedMaterial;
        // GetComponent<MeshRenderer>().sharedMaterial.SetFloatArray("_Kernel", GetCurrentKernelWeights());
        float[] weights = GetCurrentKernelWeights();
        
        Debug.Log("GilLog - ConvolutionFilter::ApplyFilter - weights.Length " + weights.Length);

        material.SetFloatArray("_Kernel", weights);
        material.SetFloat("_KernelLength", weights.Length);
        
        // Shader.SetGlobalFloatArray("_Kernel", GetCurrentKernelWeights());   
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ConvolutionFilter))]
public class ConvolutionFilterEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ConvolutionFilter filter = target as ConvolutionFilter;

        if (filter == null) return;


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
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        if (filter.filterType == ConvFilterType.CustomFilter)
        {
            filter.kernelweights = kernelValues;
        }

        if (GUILayout.Button("Apply filter"))
        {
            filter.ApplyFilter();
        }
    }

}
#endif
