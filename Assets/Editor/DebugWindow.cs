using UnityEngine;
using UnityEditor;
using Games;

public class DebugWindow : EditorWindow
{
    [MenuItem("Tool/Debug")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DebugWindow window = (DebugWindow)EditorWindow.GetWindow(typeof(DebugWindow));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Mouse World X", TouchInput.MousePosX.ToString());
        EditorGUILayout.TextField("Mouse World Y", TouchInput.MousePosY.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Cell X", TouchInput.CellX.ToString());
        EditorGUILayout.TextField("Cell Y", TouchInput.CellY.ToString());
        EditorGUILayout.EndHorizontal();
    }

    private void Update()
    {
        Repaint();
    }
}