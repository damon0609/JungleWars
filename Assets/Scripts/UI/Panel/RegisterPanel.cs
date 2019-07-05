using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;
namespace UI
{
    public class RegisterPanel : BasePanel
    {
        public const string existUserName = "该用户名存在!!!";
        public const string userAndPassIsNull = "用户名，密码，邮箱不能为空!!!";
        public const string userNameFormat = "用户名格式不正确!!!";
        public const string passwordFormat = "密码格式不正确!!!";
        public const string successRegister = "恭喜!注册成功";
        public const string failRegister = "注册失败";

        public Text tipsText;
        public InputField userName;
        public InputField password;
        public InputField email;

        private bool isValidPassword = false;
        private bool isValidUserName = false;
        private bool isValidEmail = false;


        public Button registerBtn;
        public Button loginBtn;

        private bool isExistUser = false;


        #region 检查密码 用户名 是否合法
        private void CheckPassword(string str)
        {
            //包含大小写字符和数字且不能包含特殊字符
            string pattern = "^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(str))
            {
                tipsText.DOFade(1.0f, 0.0f);
                tipsText.text = passwordFormat;
                tipsText.DOFade(0, 0.3f).SetDelay(1.0f).OnComplete(
                () =>
                {
                    tipsText.text = "";
                    password.text = "";
                });
            }
            else
            {
                isValidPassword = true;
            }
        }
        private void CheckUserName(string str)
        {
            //检查用户名是否合法
            string pattern = @"\W";//匹配任何空白字符
            bool correct = true;
            foreach (Match match in Regex.Matches(str, pattern))
            {
                correct = false;
                break;
            }
            if (!correct)
            {
                tipsText.DOFade(1.0f, 0.0f);
                tipsText.text = userNameFormat;
                tipsText.DOFade(0, 0.3f).SetDelay(1.0f).OnComplete(
                () =>
                {
                    tipsText.text = "";
                    userName.text = "";
                });
                return;
            }

            //检查是否存在的用户名
            isExistUser = UserManager.instance.CheckUserName(str);
            if (!isExistUser)
            {
                isValidUserName = true;
                return;
            }
            else
            {
                if (tipsText != null)
                {
                    tipsText.DOFade(1.0f, 0.0f);
                    tipsText.text = existUserName;
                    tipsText.DOFade(0, 0.3f).SetDelay(1.0f).OnComplete(
                    () =>
                    {
                        tipsText.text = "";
                        userName.text = "";
                    });
                }
            }
        }
        private void CheckEmail(string str)
        {
            isValidEmail = true;
            Debug.Log("邮箱合法检测");
        }
        #endregion

        #region 按钮回调函数
        private void RegisterCallBack()
        {
            if (isExistUser) return;

            string name = userName.text;
            string passStr = password.text;
            string emailStr = email.text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password.text) || string.IsNullOrEmpty(emailStr)
                || !isValidPassword || !isValidEmail || !isValidUserName)
            {
                tipsText.DOFade(1.0f, 0.0f);
                tipsText.text = userAndPassIsNull;
                tipsText.DOFade(0, 0.3f).SetDelay(1.0f).OnComplete(
                    () =>
                    {
                        tipsText.gameObject.SetActive(false);
                        tipsText.text = "";
                    });
                return;
            }
            UserInfo info = new UserInfo { name = name, password = passStr, email = emailStr };
            bool success = UserManager.instance.RegisterUser(info);
            if (success)
            {
                tipsText.DOFade(1.0f, 0.3f);
                tipsText.text = successRegister;

                GameFacade.instance.networkManager.SendAsync<UserInfo>(info,MainCommand.Login,SubCommand.Register);
            }
            else
            {
                tipsText.DOFade(1.0f, 0.3f);
                tipsText.text = failRegister;
            }
            tipsText.DOFade(0, 0.3f).SetDelay(1.0f).OnComplete(
                   () =>
                   {
                       tipsText.text = "";
                   });
        }
        private void LoginInCallBack()
        {
            UIManager.instance.OpenWin(UIPanelType.Login, new Vector3(0, -55, 0), true);
        }

        #endregion
        protected override void InitData()
        {
            base.InitData();
            ResetValue();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        protected override void OnStart()
        {
            base.OnStart();
            userName.onEndEdit.AddListener(CheckUserName);
            password.onEndEdit.AddListener(CheckPassword);
            email.onEndEdit.AddListener(CheckEmail);

            registerBtn.onClick.AddListener(RegisterCallBack);
            loginBtn.onClick.AddListener(LoginInCallBack);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            cache.transform.DOScale(Vector3.one, 0.6f);
        }

        public override void OnExit()
        {
            base.OnExit();
            ResetValue();
            cache.transform.DOScale(Vector3.zero, 0.2f);
        }

        private void ResetValue()
        {
            userName.text = "";
            password.text = "";
            email.text = "";
            isValidPassword = false;
            isValidUserName = false;
            isValidEmail = false;
        }

        public override void Destroy()
        {
            base.Destroy();
            userName.onEndEdit.RemoveListener(CheckUserName);
            password.onEndEdit.RemoveListener(CheckPassword);
            email.onEndEdit.RemoveListener(CheckEmail);

            registerBtn.onClick.RemoveListener(RegisterCallBack);
            loginBtn.onClick.RemoveListener(LoginInCallBack);
        }
    }
}
