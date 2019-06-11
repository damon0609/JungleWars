using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessagePanel:BasePanel
    {
        public AnimationConfig animConfig;

        public Text content;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("进入:"+UIPanelType.ToString());
        }
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("退出:" + UIPanelType.ToString());

        }

        public override void Destroy()
        {
            base.Destroy();
            UnRegisterEvent();
        }

        void RegisterEvent()
        {
            GameFacade.instance.event01.Addlister(UpdateContent);
        }
        void UnRegisterEvent()
        {
            GameFacade.instance.event01.RemoveListener(UpdateContent);
        }
        protected override void OnAwake()
        {
            base.OnAwake();
            RegisterEvent();
        }

        protected override void OnStart()
        {
            base.OnStart();
          
            AnimationType type = animConfig.animationType;
            if (type == AnimationType.Alpha)
            {

            }
        }
        private void UpdateContent(string str)
        {
            content.text = str;
        }
    }
}


