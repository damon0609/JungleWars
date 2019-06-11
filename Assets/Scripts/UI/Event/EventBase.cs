using UnityEngine;
using UnityEngine.Events;


public delegate void EventCall();
public delegate void EventCall<T0>(T0 t1);
public delegate void EventCall<T0, T1>(T0 t0, T1 t1);


public abstract class EventBase : ISerializationCallbackReceiver
{
    public void OnAfterDeserialize()
    {
    }
    public void OnBeforeSerialize()
    {
    }
    protected EventBase() { }
}

[System.Serializable]
public class EventHandler<T0> : EventBase
{
    [SerializeField]
    private EventCall<T0> m_call;
    public EventHandler()
    {
    }
    public void Invoke(T0 str)
    {
        if (m_call != null)
            m_call(str);
    }
    public void Addlister(EventCall<T0> call)
    {
        this.m_call = call;
    }
    public void RemoveListener(EventCall<T0> call)
    {
        this.m_call = null;
    }
}

