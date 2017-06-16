using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ColorHistogram : MonoBehaviour
{

    [HideInInspector]
    public Texture2D[] histogram = new Texture2D[4];

    [HideInInspector] public bool[] alreadyCalculated = new bool[] {false, false, false, false};

    public void CalculateHistogramTexture(int channel)
    {
        Texture2D materialTex = (Texture2D)GetComponent<MeshRenderer>().sharedMaterial.mainTexture;

        int[] channelHist = new int[256];

        for (int x = 0; x < materialTex.width; x++)
        {
            for (int y = 0; y < materialTex.height; y++)
            {
                if (channel == 0)
                    channelHist[(int)(255f*materialTex.GetPixel(x, y).r)]++;
                else if (channel == 1)
                    channelHist[(int)(255f*materialTex.GetPixel(x, y).g)]++;
                else if (channel == 2)
                    channelHist[(int)(255f*materialTex.GetPixel(x, y).b)]++;
                else if (channel == 3)
                    channelHist[(int)(255f*materialTex.GetPixel(x, y).a)]++;
            }
        }


        int maxValue = 0;

        for (int i = 0; i < channelHist.Length; i++)
        {
            if (channelHist[i] > maxValue)
                maxValue = channelHist[i];
        }

        for (int i = 0; i < channelHist.Length; i++)
        {
            channelHist[i] = (int)(150 * (channelHist[i] * 1.0f / (1.1f * maxValue)));
        }


        int sizeX = 300;
        int sizeY = 150;

        Texture2D tex = new Texture2D(sizeX, sizeY);

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (x > (sizeX-256)/2f && x < (sizeX-256)/2f + 256 && y <= channelHist[(int)(x-(sizeX-256)/2f)])
                {
                    tex.SetPixel(x, y, Color.black);
                }
                else {
                    tex.SetPixel(x, y, Color.white);
                }
            }
        }
        tex.Apply();

        histogram[channel] = tex;

        alreadyCalculated[channel] = true;
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(ColorHistogram))]
public class ColorHistogramEditor : Editor {

    private int tabSelected = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColorHistogram histogram = target as ColorHistogram;

        tabSelected = GUILayout.Toolbar (tabSelected, new string[] {"Red Channel", "Green Channel", 
                                                                     "Blue Channel", "Alpha Channel"});
        if (histogram == null) return;

        Debug.Log("GilLog - ColorHistogramEditor::OnInspectorGUI - tabSelected " + tabSelected + " ");

        if (tabSelected >= 0 && tabSelected < histogram.alreadyCalculated.Length && histogram.alreadyCalculated[tabSelected])
            GUILayout.Label(histogram.histogram[tabSelected], GUILayout.Width(300f));

        if (GUILayout.Button("Calculate Histogram"))
        {
            histogram.alreadyCalculated = new bool[] {false, false, false, false};
            histogram.histogram = new Texture2D[4];

            for (int i = 0; i < 4; i++)
            {
                histogram.CalculateHistogramTexture(i);    
            }
        }

        // AssetPreview.GetAssetPreview(histogram.GetHistogramTexture());

        // GUI.Box(new Rect(0,0, 100, 100), "Colors");

        // EditorGUI.DrawPreviewTexture(new Rect(0,0,100, 100), histogram.GetHistogramTexture());

    }

}
#endif