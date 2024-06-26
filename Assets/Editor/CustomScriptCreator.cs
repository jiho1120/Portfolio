using UnityEditor;
using UnityEngine;

public class CustomScriptCreator : EditorWindow
{
    [MenuItem("Assets/Create/C# Script %&z")] // Ctrl + Shift + N
    private static void CreateNewScript()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
            "Assets/Editor/Templates/NewBehaviourScript.cs.txt", "NewScript.cs");
    }
}
