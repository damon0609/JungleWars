using System;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> :MonoBehaviour where T:UnityEngine.Component
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }
}
