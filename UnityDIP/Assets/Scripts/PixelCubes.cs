using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PixelCubes : MonoBehaviour
{
    public GameObject prefab;

    public Texture2D pixelsSource;

    public void GeneratePixelCubes()
    {
        if (pixelsSource == null) return;

        DestroyPixelCubes();

        GameObject go;
        Color c;

        for (int x = 0; x < pixelsSource.width; x++)
        {
            for (int y = 0; y < pixelsSource.height; y++)
            {
                c = pixelsSource.GetPixel(x, y);

                if (c.a > 0.75f)
                {
                    go = Instantiate(prefab) as GameObject;
                    go.transform.parent = transform;
                    go.transform.localPosition = new Vector3(x, y);
                    go.GetComponent<MeshRenderer>().material.SetColor("_Color", c);
                }
                
            }
        }
    }

    public void DestroyPixelCubes()
    {
        int totalChildren = transform.childCount;

        for (int i = 0; i < totalChildren; i++)
        {
        #if UNITY_EDITOR
            DestroyImmediate(transform.GetChild(0).gameObject);
        #endif
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(PixelCubes))]
public class PixelCubesEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PixelCubes cubes = target as PixelCubes;

        if (cubes == null) return;

        GUILayout.Label(cubes.pixelsSource, GUILayout.Width(300f));

        if (GUILayout.Button("Generate"))
        {
            cubes.GeneratePixelCubes();
        }

        if (GUILayout.Button("Destroy"))
        {
            cubes.DestroyPixelCubes();
        }
    }

}
#endif