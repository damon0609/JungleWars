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

    [MenuItem("Damon Tool/Activity Tool", false, 100)]
    private static void CreateActivity()
    {
        ActivityEditorWindow win = EditorWindow.GetWindow<ActivityEditorWindow>();
        win.titleContent = new GUIContent("Activity Editor");
        win.Show();
    }

}
