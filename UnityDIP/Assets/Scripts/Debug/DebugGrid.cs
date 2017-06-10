using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IColorGrid
{
    string GetWindowTitle();
    int GetGridWidth();
    int GetGridHeight();
    Color GetColor(int x, int y);
}

public enum GridType
{
    None,
    FromMeshRenderer
}

public class DebugGrid : MonoBehaviour, IColorGrid {

    public GridType gridType;

    public GameObject targetObject;

    public string windowTitle;

    public int maxGridWidth = 80;
    public int maxGridHeight = 80;

    public Vector2 currentUV;
    public int currentPixelCoordX, currentPixelCoordY;

    private static List<DebugGrid> multipleInstances = new List<DebugGrid>();

    #if UNITY_EDITOR
    EditorWindow editorWindow = null;

    public static DebugGrid AddEditorWindow(EditorWindow window)
    {
        for (int i = 0; i < multipleInstances.Count; i++)
        {
            if (multipleInstances[i] != null && multipleInstances[i].editorWindow == null)
            {
                multipleInstances[i].SetupEditorWindow(window);
                return multipleInstances[i];
            }
        }

        return null;
    }

    public void SetupEditorWindow(EditorWindow editor)
    {
        editorWindow = editor;
    }

    #endif

    public string GetWindowTitle()
    {
        return windowTitle;
    }

    public int GetGridWidth() {
        return maxGridWidth;
    }

    public int GetGridHeight() {
        return maxGridHeight;
    }

    public Color GetColor(int x, int y) {
        if (gridType == GridType.FromMeshRenderer && targetObject != null)
        {
            Texture2D tex = (Texture2D) (targetObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));

            if (tex == null) return Color.white;

            currentPixelCoordX = (int)(currentUV.x * tex.width);
            currentPixelCoordY = (int) (currentUV.y * tex.height);

            x -= maxGridWidth / 2;
            y -= maxGridHeight / 2;

            y *= -1;

            x = (tex.width + currentPixelCoordX + x) % tex.width;
            y = (tex.height + currentPixelCoordY + y) % tex.height;

            return tex.GetPixel(x, y);
        }

        return Color.white;
    }

    void Awake()
    {
        multipleInstances.Add(this);
    }

    void Start()
    {
        currentUV = Vector2.zero;
    }

    void Update()
    {
        if (gridType == GridType.None) return;

        if (gridType == GridType.FromMeshRenderer && targetObject != null && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!targetObject.GetComponent<MeshCollider>().Raycast(ray, out hit, 100.0F))
                return;

            currentUV = hit.textureCoord;
        }

        #if UNITY_EDITOR
        if (editorWindow != null)
        {
            editorWindow.Repaint();
        }
        #endif
    }
}
