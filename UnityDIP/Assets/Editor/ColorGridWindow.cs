using UnityEngine;
using UnityEditor;

// Simple script that creates a new dockable window
public class ColorGridWindow : EditorWindow
{
    private IColorGrid colorGrid = null;

    private const int maxInnerWidth = 600;
    private const int maxInnerHeight = 600;

    public void SetupColorGrid(IColorGrid colorGridInterface)
    {
        colorGrid = colorGridInterface;
    }

    [MenuItem("Debug/ColorGrid")]
    static void Initialize()
    {
        Debug.Log("GilLog - ColorGridWindow::Initialize");

        ColorGridWindow window  = (ColorGridWindow)CreateInstance<ColorGridWindow>();

        IColorGrid colorGridInstance = DebugGrid.AddEditorWindow(window);

        Debug.Log("GilLog - ColorGridWindow::Initialize - colorGridInstance " + colorGridInstance + " ");

        if (colorGridInstance == null) return;

        window.title = colorGridInstance.GetWindowTitle();

        window.SetupColorGrid(colorGridInstance);

        window.Show();
    }

    void OnGUI()
    {
        // Number of Cells
        int cols = 15, rows = 15;

        if (colorGrid != null)
        {
            cols = colorGrid.GetGridWidth();
            rows = colorGrid.GetGridHeight();
        }

        float gridItemWidth = maxInnerWidth/(1.0f * cols);

        GUI.Box(new Rect(5,5, 800, 800), "Colors");
        GUILayout.BeginArea(new Rect(10, 10, 700, 700));
        GUILayout.BeginVertical();
        for (int y = 0; y < rows; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < cols; x++)
            {
                if (colorGrid != null) {
                    EditorGUILayout.ColorField(label:GUIContent.none,
                                               value:colorGrid.GetColor(x, y),
                                               showEyedropper:false, 
                                               showAlpha:true, 
                                               hdr:false, 
                                               options:GUILayout.Width(gridItemWidth));
                } else {
                    EditorGUILayout.ColorField(label:GUIContent.none,
                                               value:Color.Lerp(Color.red, Color.blue, (x + y * cols)/(1.0f * rows * cols)),
                                               showEyedropper:false, 
                                               showAlpha:true, 
                                               hdr:false, 
                                               options:GUILayout.Width(gridItemWidth));
                }

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}