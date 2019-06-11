using System;
using System.Collections.Generic;
using UnityEngine;

//泛型类型如果需要构造函数时需要约束一下
public class ObjectPool<T> where T:new()
{
    private EventCall _getCallBack;
    private EventCall _releaseCallBack;

    private readonly Stack<T> pool;

    public int countAll;
    public int countActive { get { return countAll - countInactive; } }
    public int countInactive { get { return countAll - pool.Count; } }

    public ObjectPool(EventCall getCallBack, EventCall releaseCallBack)
    {
        pool = new Stack<T>();
        _getCallBack = getCallBack;
        _releaseCallBack = releaseCallBack;
    }
    public T Get()
    {
        if (_getCallBack != null)
            _getCallBack();

        if (pool != null)
        {
            T element;
            if (pool.Count == 0)
            {
                countAll++;
                element = new T();
                pool.Push(element);
            }
            else
            {
                element = pool.Pop();
            }
            return element;
        }
        else
        {
            return default(T);
        }

       
    }
    public void Release(T element)
    {
        if (pool.Count > 0 && ReferenceEquals(pool.Peek(), element))
            Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");

        if (pool != null)
            pool.Push(element);

        if (_releaseCallBack != null)
            _releaseCallBack();
    }
}
