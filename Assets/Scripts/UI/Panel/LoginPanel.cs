using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class LoginPanel : BasePanel
    {
        public AnimationConfig animConfig;

        public Button loginBtn;
        public Button registerBtn;

        public InputField userName;
        public InputField password;

        private bool isLogin = false;
        public EventHandler<bool> isLoginHandler = new EventHandler<bool>();

        public Text tipsText;

        public override void OnEnter()
        {
            base.OnEnter();
            cache.transform.DOScale(Vector3.one,0.6f);
        }
        public override void OnExit()
        {
            base.OnExit();
            cache.transform.DOScale(Vector3.zero,0.2f);
        }
        protected override void OnAwake()
        {
            base.OnAwake();
        }

        #region 按钮回调
        private void LoginBtnCallBack()
        {
            string nameStr = userName.text;
            int passwordValue = int.Parse(password.text);

            UserInfo info = new UserInfo { name = nameStr, password = passwordValue };
            isLogin = UserManager.instance.CheckLoginState(info);

            if (isLogin)
            {
                Debug.Log("登录成功");
            }

            if (isLoginHandler != null)
                isLoginHandler.Invoke(isLogin);
        }
        private void RegisterCallBack()
        {
           UIManager.instance.OpenWin(UIPanelType.Register,new Vector3(0,-20,0),true);
        }
        #endregion
        protected override void InitData()
        {
            base.InitData();
        }
        private void CheckPassword(string str)
        {

        }
        private void CheckUserName(string str)
        {
            bool isTrue = UserManager.instance.CheckUserName(str);
            if (!isTrue)
            {
                DoTweenTool.DoFade(tipsText,1);
                tipsText.text = "不存在的用户名或者用户名错误";
                DoTweenTool.DoFade(tipsText, 0.3f).SetDelay(1.0f).OnComplete(
                   () =>
                   {
                       DoTweenTool.DoFade(tipsText, 0);
                       tipsText.text = "";
                   });
            }
        }
        protected override void OnStart()
        {
            base.OnStart();
            userName.onEndEdit.AddListener(CheckUserName);
            password.onEndEdit.AddListener(CheckPassword);
            registerBtn.onClick.AddListener(RegisterCallBack);
            loginBtn.onClick.AddListener(LoginBtnCallBack);
        }

        public override void Destroy()
        {
            base.Destroy();
            userName.onEndEdit.RemoveListener(CheckUserName);
            password.onEndEdit.RemoveListener(CheckPassword);
            registerBtn.onClick.RemoveListener(RegisterCallBack);
            loginBtn.onClick.RemoveListener(LoginBtnCallBack);
        }
    }
}
