using System;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T:new()
{
    static object locker = new object();
    private static T _instance;
    public static T instance
    {
        get {
            if (_instance == null)
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }

    protected Singleton()
    {
        Debug.Log("init--"+typeof(T).ToString());
        Init();
    }
    protected virtual void Init()
    {

    }
}
