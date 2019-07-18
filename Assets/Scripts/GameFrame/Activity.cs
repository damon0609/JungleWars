using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ActivityType:int
{
    None=10001,
    Login,

}


//可视组件管理
[System.Serializable]
public class ViewItem
{
    public int id;
    [NonSerialized]
    public GameObject view;
    public string path;
    public Vector2 pos;
    public Vector2 rect;
}

[System.Serializable]
public class View
{
    public Vector2 pos;
    public Vector2 rect;
    public ViewType type;
}

public enum ViewType
{
    Text,
    Image,
    Empty,
}

public class Activity:MonoBehaviour
{
    public string name;
 
    [HideInInspector]
    public GameObject canvas;

    public ActivityType activityType;

    public void Awake()
    {
        
    }

    public void Start () {
	}
	
    public void Update () {
		
	}

    public void OnDestroy()
    {
        
    }
}
