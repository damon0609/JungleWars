using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventHandler : EventBase
{
    [SerializeField]
    private EventCall m_call;

    [SerializeField]
    private GameObject m_target;

    public EventHandler()
    {
    }
    public void Invoke()
    {
        if (m_call != null)
            m_call();
    }
    public void Addlister(EventCall call)
    {
        this.m_call = call;
    }
    public void RemoveListener(EventCall call)
    {
        this.m_call = null;
    }
}
