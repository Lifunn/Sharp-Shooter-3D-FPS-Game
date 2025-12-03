using UnityEngine;
using UnityEditor;

public class FixTMPMaterials
{
    [MenuItem("Tools/TMP/Fix Pink Materials")]
    public static void FixPinkMaterials()
    {
        // daftar shader TMP yang kemungkinan tersedia (URP/HDRP/Legacy)
        string[] tryShaders = new string[]
        {
            "TextMeshPro/Distance Field (Surface)",
            "TextMeshPro/Distance Field",
            "TextMeshPro/Sprite",
            "TextMeshPro/Mobile/Distance Field",
            "TextMeshPro/Mobile/Distance Field - 2 Pass"
        };

        Shader fallbackShader = null;

        // cari 1 shader yang benar-benar ada
        foreach (string s in tryShaders)
        {
            Shader found = Shader.Find(s);
            if (found != null)
            {
                fallbackShader = found;
                break;
            }
        }

        if (fallbackShader == null)
        {
            EditorUtility.DisplayDialog("TMP Fix", 
                "Tidak menemukan shader TMP apapun.\nPastikan TextMeshPro sudah terinstall.", 
                "OK");
            return;
        }

        int fixedCount = 0;

        // cari semua material di project
        string[] guids = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null)
                continue;

            bool pinkColor = false;

            if (mat.HasProperty("_Color"))
            {
                Color c = mat.color;
                if (c == new Color(1, 0, 1, 1)) // magenta
                    pinkColor = true;
            }

            bool shaderMissing = (mat.shader == null || !mat.shader.isSupported);

            // jika shader rusak / tidak disupport / material terlihat pink
            if (shaderMissing || pinkColor)
            {
                mat.shader = fallbackShader;
                EditorUtility.SetDirty(mat);
                fixedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("TMP Fix Complete",
            $"Materials fixed: {fixedCount}",
            "OK");

        Debug.Log($"TMP Fix done. Materials fixed: {fixedCount}");
    }
}
