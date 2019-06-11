using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    [System.Serializable]
    public class UIPanels
    {
        public List<UIPanelInfo> list;
    }

    public class UIManager:MonoSingleton<UIManager>
    {
        private Canvas _uiRootCanvas;
        public Canvas UiRootCanvas
        {
            get
            {
                return _uiRootCanvas;
            }
            private set { }
        }

        private Transform _uiRootTransform;
        public Transform uiRootTrnasform
        {
            get {
                if (_uiRootTransform == null)
                {
                    _uiRootCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                    _uiRootTransform = _uiRootCanvas.transform;
                }
                return _uiRootTransform;
            }
        }

        private Canvas _topCanvas;
        public Canvas topCanvas
        {
            get
            {
                if (_topCanvas == null)
                {
                    _topCanvas = GameObject.Find("Canvas/TopLayer").GetComponent<Canvas>();
                }
                return _topCanvas;
            }
            private set { }
        }

        private Stack<BasePanel> panelStack = new Stack<BasePanel>();

        void InitCanvas()
        {
            _uiRootCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            _topCanvas = GameObject.Find("Canvas/TopLayer").GetComponent<Canvas>();
            _uiRootTransform = _uiRootCanvas.transform;
        }
        void InitData()
        {
           
        }
        void DefaultPanel()
        {
            OpenWin(UIPanelType.Login,new Vector3(0,-55,0));
        }

        public void OpenWin(UIPanelType type, Vector3 pos, bool closeOther = false)
        {
            BasePanel panel = UIPanelRegister.GetPanel(type);
            if (panelStack.Count != 0)
            {
                BasePanel top = null;

                if (closeOther)
                    top = panelStack.Pop();
                else
                    top = panelStack.Peek();

                if (null != top)
                    top.OnExit();
            }
            panel.transform.localPosition = pos;
            panel.OnEnter();
            panelStack.Push(panel);
        }

        public void OpenWin(BasePanel panel, Vector3 pos,bool closeOther=false)
        {
            if (panelStack.Count != 0)
            {
                BasePanel top = null;

                if (closeOther)
                    top = panelStack.Pop();
                else
                    top = panelStack.Peek();

                if (null != top)
                    top.OnExit();
            }
            panel.transform.localPosition=pos;
            panel.OnEnter();
            panelStack.Push(panel);
        }

        public void OpenWin(BasePanel panel, bool closeOther)
        {
            OpenWin(panel,closeOther);
        }
        public void OpenWin(UIPanelType type, bool closeOther)
        {
            BasePanel target = UIPanelRegister.GetPanel(type);
            OpenWin(target,Vector3.zero,closeOther);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            InitCanvas();
            InitData();
        }

        protected override void OnStart()
        {
            base.OnStart();
            DefaultPanel();
        }
    }
}


