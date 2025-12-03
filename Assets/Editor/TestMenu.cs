using UnityEditor;
using UnityEngine;

public class TestMenu
{
    [MenuItem("Tools/TEST TEST TEST")]
    public static void Test()
    {
        Debug.Log("Menu test clicked!");
        EditorUtility.DisplayDialog("Test Menu", "Menu test berhasil", "OK");
    }
}
