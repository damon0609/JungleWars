using UnityEngine;
using UnityEditor;
public class MenuTool
{
    [MenuItem("Damon Tool/AssetBundle",false,100)]
    private static void CreateAssetBundle()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("Build AssetBundle");
        win.Show();
    }
}
