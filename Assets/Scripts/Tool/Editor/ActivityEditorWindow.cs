using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ActivityEditorWindow : EditorWindow
{
    private static GameObject uiCanvas;
    private void OnEnable()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UIRoot");
    }

    Vector2 pos;
    Vector2 size;
    ViewType type;
    string content;
    Sprite sprite;
    string id;


    private string activityName;
    private ActivityType activityType=ActivityType.None;
    private Vector2 scrollPos;
    private Activity cur;
    bool on = true;


    private void CreateActivity()
    {
        EditorGUILayout.BeginVertical("box");
        activityName = EditorGUILayout.TextField("Activity Name",activityName);
        GUILayout.Space(5);
        activityType = (ActivityType)EditorGUILayout.EnumPopup("ActivityType", activityType);
        GUILayout.Space(5);
        if(GUILayout.Button("创建Activity"))
        {
            if (activityName == "")
                return;

            GameObject go = new GameObject(activityName);
            go.AddComponent<RectTransform>();

            Activity temp = go.AddComponent<Activity>();
            temp.canvas = go;
            temp.name = activityName;
            temp.activityType = activityType;
            go.transform.parent = uiCanvas.transform;
            go.transform.localScale = Vector3.one;

            RectTransform rectTransform = go.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.sizeDelta = Vector2.zero;

            activityName = "";
            ActivttyManager.Register(temp);

        }
        EditorGUILayout.EndVertical();
    }
    private void CreateActivityView()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginScrollView(scrollPos,"box");
        for (int i = 0; i < ActivttyManager.count;i++)
        {
            Activity temp = ActivttyManager.Get(i);
            string name = temp.name;
        }

        GUIContent content = new GUIContent();
        content.text = "test";
        content.image = null;
        on = EditorGUILayout.ToggleLeft(content,on);

        EditorGUILayout.EndScrollView();


        EditorGUILayout.EndHorizontal();

    }
    private void OnGUI()
    {
        //先创建Activity
        CreateActivity();
        CreateActivityView();

        if(Event.current.type==EventType.MouseDown&&Event.current.button==0)
        {
        }
        if(Event.current.type==EventType.MouseUp&&Event.current.button==0)
        {
            Debug.Log("鼠标弹起:"+Event.current.mousePosition);
        }

        if(EventType.ScrollWheel==Event.current.type)
        {
            Debug.Log("gui 上事件监听");
        }

        if(Event.current.type==EventType.MouseEnterWindow)
        {
            Debug.Log("鼠标进入窗口");
        }
        if(Event.current.type==EventType.MouseLeaveWindow)
        {
            Debug.Log("离开窗口");
        }

        /*

        EditorGUILayout.LabelField("Create");

        EditorGUILayout.BeginHorizontal();
        pos = EditorGUILayout.Vector2Field("Position",pos);
        GUILayout.Space(5);
        size = EditorGUILayout.Vector2Field("Size",size);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        type = (ViewType)EditorGUILayout.EnumPopup("组件类型",type);
        GUILayout.Space(5);
        if(type==ViewType.Text)
        {
            content = EditorGUILayout.TextField("文本内容", content);
        }
        else if(type==ViewType.Image)
        {
            sprite = (Sprite)EditorGUILayout.ObjectField("Sprite",sprite, typeof(Sprite),true);
        }
        EditorGUILayout.EndVertical();

        id = EditorGUILayout.TextField("id",id);


        if(GUILayout.Button("创建"))
        {
            GameObject go = new GameObject(id);
            Text text = go.AddComponent<Text>();
            text.text = content;

            RectTransform rectTransoform = go.GetComponent<RectTransform>();
            rectTransoform.anchoredPosition = pos;
            rectTransoform.sizeDelta = size;

        }
        */
    }

}
