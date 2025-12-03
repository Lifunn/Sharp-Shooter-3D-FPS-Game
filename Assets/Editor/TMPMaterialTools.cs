// Assets/Editor/TMPMaterialTools.cs
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class TMPMaterialTools
{
    [MenuItem("Tools/TMP/Report Pink Materials")]
    public static void ReportPinkMaterials()
    {
        var guids = AssetDatabase.FindAssets("t:Material");
        var report = new List<string>();
        foreach (var g in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(g);
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null) continue;

            bool pink = false;
            if (mat.HasProperty("_Color"))
            {
                var c = mat.color;
                if (Mathf.Approximately(c.r,1f) && Mathf.Approximately(c.g,0f) && Mathf.Approximately(c.b,1f))
                    pink = true;
            }

            bool shaderMissing = mat.shader == null || !mat.shader.isSupported;
            if (pink || shaderMissing)
            {
                string shaderName = mat.shader != null ? mat.shader.name : "<null>";
                report.Add($"{path}  |  Shader: {shaderName}  |  Pink:{pink}  Missing:{shaderMissing}");
            }
        }

        if (report.Count == 0)
        {
            EditorUtility.DisplayDialog("TMP Report", "No pink/missing-shader materials found.", "OK");
            Debug.Log("TMP Report: none found.");
            return;
        }

        // show in console and copy to clipboard for easy sharing
        string output = "TMP Pink/Missing Shader Materials Report:\n" + string.Join("\n", report);
        Debug.Log(output);
        EditorGUIUtility.systemCopyBuffer = output;
        EditorUtility.DisplayDialog("TMP Report", $"Found {report.Count} materials. Report copied to clipboard and Console.", "OK");
    }

    [MenuItem("Tools/TMP/Auto Fix Pink Materials")]
    public static void AutoFixPinkMaterials()
    {
        // mapping common shader names (from old TMP/HDRP/etc) -> target shader available in this project
        var mapping = new Dictionary<string,string>()
        {
            // common old TMP names or HDRP-specific names -> URP/TextMeshPro shader fallback
            {"TextMeshPro/Mobile/Distance Field - 2 Pass","TextMeshPro/Distance Field"},
            {"TextMeshPro/Mobile/Distance Field","TextMeshPro/Distance Field"},
            {"TextMeshPro/Distance Field - Mobile","TextMeshPro/Distance Field"},
            {"TextMeshPro/Distance Field","TextMeshPro/Distance Field"},
            {"TextMeshPro/Sprite","TextMeshPro/Sprite"},
            {"TMP_SDF-HDRP LIT","TextMeshPro/Distance Field (Surface)"},
            {"TMP_SDF-HDRP UNLIT","TextMeshPro/Distance Field"},
            {"Unity SDF - HDRP LIT - Bloom","TextMeshPro/Distance Field (Surface)"},
            {"Unity SDF - HDRP LIT - Outline","TextMeshPro/Distance Field (Surface)"},
            // fallback key (if shader name not in mapping) -> default
            {"<default>","TextMeshPro/Distance Field (Surface)"}
        };

        // find available target shader (fallback check)
        string defaultShaderName = mapping["<default>"];
        Shader defaultShader = Shader.Find(defaultShaderName) ?? Shader.Find("TextMeshPro/Distance Field") ?? Shader.Find("TextMeshPro/Sprite");

        if (defaultShader == null)
        {
            EditorUtility.DisplayDialog("TMP Auto Fix", "No TextMeshPro shader found in project. Make sure TextMeshPro package & resources are installed.", "OK");
            Debug.LogError("TMP Auto Fix: no TMP shader found.");
            return;
        }

        int fixedCount = 0;
        var guids = AssetDatabase.FindAssets("t:Material");
        foreach (var g in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(g);
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null) continue;

            bool pink = false;
            if (mat.HasProperty("_Color"))
            {
                var c = mat.color;
                if (Mathf.Approximately(c.r,1f) && Mathf.Approximately(c.g,0f) && Mathf.Approximately(c.b,1f))
                    pink = true;
            }
            bool shaderMissing = mat.shader == null || !mat.shader.isSupported;

            if (!(pink || shaderMissing)) continue;

            string currentShaderName = mat.shader != null ? mat.shader.name : "<null>";
            Shader newShader = null;

            if (currentShaderName != "<null>" && mapping.ContainsKey(currentShaderName))
            {
                newShader = Shader.Find(mapping[currentShaderName]);
            }
            // if not found in mapping, try to guess by substrings
            if (newShader == null)
            {
                foreach (var kv in mapping)
                {
                    if (kv.Key == "<default>") continue;
                    if (currentShaderName.IndexOf(kv.Key, System.StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        newShader = Shader.Find(kv.Value);
                        if (newShader != null) break;
                    }
                }
            }

            if (newShader == null) newShader = defaultShader;

            mat.shader = newShader;
            // reset pink color if it's magenta to white (so preview picks shader)
            if (mat.HasProperty("_Color"))
            {
                var col = mat.color;
                if (Mathf.Approximately(col.r,1f) && Mathf.Approximately(col.g,0f) && Mathf.Approximately(col.b,1f))
                    mat.color = Color.white;
            }

            EditorUtility.SetDirty(mat);
            fixedCount++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("TMP Auto Fix", $"Materials fixed: {fixedCount}", "OK");
        Debug.Log($"TMP Auto Fix: materials fixed = {fixedCount}");
    }
}
