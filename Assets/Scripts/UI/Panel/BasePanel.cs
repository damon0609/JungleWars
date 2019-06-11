using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public enum UIPanelType
    {
        None,
        Message,
        Login,
        Register,

    }

    [System.Serializable]
    public class UIPanelInfo : ISerializationCallbackReceiver
    {
        public string path;
        public string panelType;

        [System.NonSerialized]
        public UIPanelType type;
               
        public void OnAfterDeserialize()
        {
            type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType),panelType);
        }
        public void OnBeforeSerialize()
        {

        }
    }

    public class BasePanel : MonoBehaviour,IUIPanel
    {
        public UIPanelType UIPanelType;

        [HideInInspector]
        public Transform cache;

        public virtual void Destroy()
        {
            Destroy(cache);
            UIPanelRegister.UnRegisterPanel(this);
        }
        public virtual void OnEnter()
        {

        }
        public virtual void OnExit()
        {

        }
        protected virtual void OnAwake()
        {
            cache = transform;
            InitData();
        }
        protected virtual void InitData()
        {

        }

        protected virtual void OnStart()
        {
            UIPanelRegister.RegisterPanel(UIPanelType,this);
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
}
