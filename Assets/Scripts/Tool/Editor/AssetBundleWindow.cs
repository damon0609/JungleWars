using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class AssetBundleWindow : EditorWindow
{
    public readonly List<string> filterExtensions = new List<string> { ".png", ".txt", ".prefab", ".fbx", ".unity", ".asset",".jpg",".json",".xml",".cs" };
    private Vector2 m_ScrollViewAllAssetPos;
    private int itemHeight = 18;
    private int itemWidth = 300;
    private Rect wholeRect;
    AssetInfo root;
    int viewRectHeight = 18;

    GUIStyle m_box = new GUIStyle("Box");
    GUIContent folder = new GUIContent("Folder Icon");

    void Init()
    {
        string targetPath = Application.dataPath.Replace("/", @"\");

        root = new AssetInfo();
        root.unfold = true;
        root.assetType = AssetType.Folder;
        root.fullPath = targetPath;
        root.name = targetPath.Substring(targetPath.LastIndexOf('\\') + 1);
        GetAllAsset(targetPath, root);


        /*序列化数据
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            binaryFormatter.Serialize(stream,root);
            File.WriteAllBytes(@"D:\test.txt",stream.ToArray());
        }
        */
    }
    private void GetAllAsset(string path, AssetInfo root)
    {
        FileSystemInfo[] all = new DirectoryInfo(path).GetFileSystemInfos();
        for (int i = 0; i < all.Length; i++)
        {
            FileSystemInfo info = all[i];
            string mPath = info.FullName;
            if (info is DirectoryInfo)
            {
                AssetInfo folder = new AssetInfo();
                folder.assetType = AssetType.Folder;
                folder.fullPath = mPath;//c#中的路径
                folder.name = mPath.Substring(mPath.LastIndexOf('\\') + 1);
                folder.relativePath = path.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
                root.childrens.Add(folder);

                //新的父节点
                GetAllAsset(info.FullName, folder);
            }
            else if (info is FileInfo)
            {
                if (filterExtensions.Contains(info.Extension))
                {
                    AssetInfo children = new AssetInfo();
                    children.fullPath = mPath;
                    children.assetType = AssetType.Asset;
                    children.name = Path.GetFileNameWithoutExtension(mPath);
                    children.extension = Path.GetExtension(mPath);
                    children.relativePath = mPath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
                    children.obj = AssetDatabase.LoadAssetAtPath(children.relativePath, typeof(UnityEngine.Object));
                    root.childrens.Add(children);
                }
            }
        }
    }
    
    private void OnEnable()
    {
        Init();
    }
    private void OnGUI()
    {
        wholeRect = new Rect(5, 5, itemWidth, position.height - 10);

        DrawAssetsGUI(wholeRect);

        Rect rect02 = new Rect(itemWidth + 10,5, itemWidth, position.height-10);
        GUI.Box(rect02,"");
    }
    void DrawAssetsGUI(Rect rect)
    {
        Rect mScrollRect = new Rect(0, 0, itemWidth, viewRectHeight);

        m_ScrollViewAllAssetPos = GUI.BeginScrollView(wholeRect, m_ScrollViewAllAssetPos, mScrollRect, true, true);//可视区域的高度应根据折叠还是展开来定义高度
        GUI.BeginGroup(mScrollRect, m_box);
        viewRectHeight = 18;
        DrawAsset(root, 5);
        GUI.EndGroup();
        GUI.EndScrollView();

        if (viewRectHeight < wholeRect.height)
            viewRectHeight = (int)wholeRect.height;
    }
    private void DrawAsset(AssetInfo mRoot, int indent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(indent);
        if (mRoot.assetType == AssetType.Folder) //绘制文件夹类型的
        {
            folder.text = mRoot.name;
            if (mRoot.childCount > 0)
            {
                mRoot.unfold = EditorGUILayout.Foldout(mRoot.unfold, folder);
            }
            else
            {
                GUI.enabled = false;
                mRoot.unfold = EditorGUILayout.Foldout(mRoot.unfold, folder);
                GUI.enabled = true;
            }
        }
        else
        {
            GUIContent content = new GUIContent();
            content.text = mRoot.name;

            if (mRoot.extension == filterExtensions[0] || mRoot.extension == filterExtensions[6])
            {
                content.image = (Texture2D)mRoot.obj;
                mRoot.isSelected = EditorGUILayout.ToggleLeft(content, mRoot.isSelected);
            }
            else if (mRoot.extension == filterExtensions[1] || mRoot.extension == filterExtensions[7] || mRoot.extension == filterExtensions[8])
            {
                content = GUIContentIconUnility.textAsset;
                mRoot.isSelected = EditorGUILayout.ToggleLeft(content, mRoot.isSelected);
            }
            else if (mRoot.extension == filterExtensions[2])
            {
                content = GUIContentIconUnility.prebabNormal;
                mRoot.isSelected = EditorGUILayout.ToggleLeft(content, mRoot.isSelected);
            }
            else if (mRoot.extension == filterExtensions[4])
            {
                content = GUIContentIconUnility.unityLogo;
                mRoot.isSelected = EditorGUILayout.ToggleLeft(content, mRoot.isSelected);
            }
            else if (mRoot.extension == filterExtensions[3])
            {
                content = GUIContentIconUnility.gamObject;
                mRoot.isSelected = EditorGUILayout.ToggleLeft(content, mRoot.isSelected);
            }
            else if (mRoot.extension == filterExtensions[5])
            {
                content = GUIContentIconUnility.scriptableObject;
                mRoot.isSelected = EditorGUILayout.ToggleLeft(content, mRoot.isSelected);
            }
        }//绘制资源类

        viewRectHeight += 18;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();


        //子对象的绘制
        if (mRoot.unfold && mRoot.childCount > 0)
        {
            indent += 15;
            for (int i = 0; i < mRoot.childCount; i++)
                DrawAsset(mRoot.childrens[i], indent);
        }
    }
}

public static class GUIContentIconUnility
{
    public static GUIContent gamObject { get { return EditorGUIUtility.IconContent("GameObject Icon"); } private set { } }
    public static GUIContent scriptableObject { get { return EditorGUIUtility.IconContent("ScriptableObject Icon"); } private set { } }
    public static GUIContent unityLogo { get { return EditorGUIUtility.IconContent("UnityLogo"); } private set { } }
    public static GUIContent prebabNormal { get { return EditorGUIUtility.IconContent("PrefabNormal Icon"); } private set { } }
    public static GUIContent textAsset { get { return EditorGUIUtility.IconContent("TextAsset Icon"); } private set { } }
    public static GUIContent folder { get { return EditorGUIUtility.IconContent("Folder Icon"); } private set { } }
}

public static class GUIStyleUnility
{
    public static GUIStyle box { get { return new GUIStyle("Box"); } private set { } }
    public static GUIStyle preButton { get { return new GUIStyle("PreButton"); } private set { } }
    public static GUIStyle preDropDown { get { return new GUIStyle("PreDropDown"); } private set { } }
    public static GUIStyle LRSelect { get { return new GUIStyle("LODSliderRangeSelected"); } private set { } }
    public static GUIStyle prefabLabel { get { return new GUIStyle("PR PrefabLabel"); } private set { } }
    public static GUIStyle miniButtonLeft { get { return new GUIStyle("MiniButtonLeft"); } private set { } }
    public static GUIStyle miniButtonRight { get { return new GUIStyle("MiniButtonRight"); } private set { } }
    public static GUIStyle oLMinus { get { return new GUIStyle("OL Minus"); } private set { } }
    public static GUIStyle iconButton = new GUIStyle("IconButton");
}


[System.Serializable]
public class AssetInfo
{
    [SerializeField]
    public List<AssetInfo> childrens = new List<AssetInfo>();
    [SerializeField]
    public int childCount
    {
        get
        {
            int count = 0;
            if (childrens != null)
                count = childrens.Count;
            return count;
        }
    }
    [SerializeField]
    public string fullPath; //全路径
    [SerializeField]
    public string relativePath; //unity中路径
    [SerializeField]
    public string extension;//拓展名
    [SerializeField]
    public bool unfold = false;//文件夹是否展开
    [SerializeField]
    public AssetType assetType;//资源类型
    [SerializeField]
    public string name;
    
    [System.NonSerialized]
    public UnityEngine.Object obj;
    [SerializeField]
    public int index;
    [SerializeField]
    public bool isSelected = false;
}

[System.Serializable]
public enum AssetType
{
    Folder,
    Asset,
}