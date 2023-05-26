using UnityEngine;
using UnityEditor;
using System.IO;


public class AddConditionalCompilation : EditorWindow
{
    private string folderPath = "";
    private string startTag = "#if UNITY_EDITOR";
    private string endTag = "#endif";

    [MenuItem("Tools/Add Conditional Compilation")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AddConditionalCompilation));
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Conditional Compilation to Scripts", EditorStyles.boldLabel);

        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);
        startTag = EditorGUILayout.TextField("Start Tag", startTag);
        endTag = EditorGUILayout.TextField("End Tag", endTag);

        if (GUILayout.Button("Add Conditional Compilation"))
        {
            AddCompilation();
        }

        if (GUILayout.Button("Remove Conditional Compilation"))
        {
            RemoveCompilation();
        }
    }

    private void RemoveCompilation()
    {
        string[] files = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string text = File.ReadAllText(file);
            if (text.StartsWith(startTag))
            {
                text = text.Substring(startTag.Length);
                if (text.EndsWith(endTag))
                {
                    text = text.Substring(0, text.Length - endTag.Length);
                }
            }

            File.WriteAllText(file, text);
        }

        EditorUtility.DisplayDialog("Conditional Compilation Removed",
            "Conditional compilation has been removed from all scripts in the specified folder.", "OK");
    }

    private void AddCompilation()
    {
        string[] files = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string text = File.ReadAllText(file);
            if (!text.Contains(startTag))
            {
                text = startTag + "\n" + text + "\n" + endTag;
                File.WriteAllText(file, text);
            }
        }

        EditorUtility.DisplayDialog("Conditional Compilation Added",
            "Conditional compilation has been added to all scripts in the specified folder.", "OK");
    }
}