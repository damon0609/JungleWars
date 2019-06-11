using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System.IO;
using System.Xml;
public class UserManager : Singleton<UserManager>
{
    private const string pathJson = "/Data/UserList.json";
    private readonly string pathXML = "/Data/UserList.xml";
    private List<UserInfo> list = new List<UserInfo>();

    public UserManager()
    {
        pathXML = Application.dataPath + pathXML;
        XmlDocument xml = new XmlDocument();
        if (File.Exists(pathXML))
        {
            xml.Load(pathXML);
            XmlNode root = xml.SelectSingleNode("root");

            if (root.ChildNodes.Count == 0) return;

            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                XmlElement el = root.ChildNodes[i] as XmlElement;
                string name = el.GetAttribute("name");
                string pass = el.GetAttribute("password");

                UserInfo info = new UserInfo {name=name,password=int.Parse(pass) };
                list.Add(info);
            }
        }
        else
        {
            XmlDeclaration del = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(del);
            XmlNode root = xml.CreateElement("root"); //创建节点
            xml.AppendChild(root);
            xml.Save(pathXML);
        }
    }

    public bool CheckUserName(string name)
    {
        UserInfo info = list.Find(item =>
        {
            if (item.name == name && !string.IsNullOrEmpty(name))
                return true;
            else
                return false;
        });
        return info != null;
    }
    public bool CheckLoginState(UserInfo info)
    {
        if (string.IsNullOrEmpty(info.name) || string.IsNullOrEmpty(info.password.ToString()))
            return false;

        UserInfo temp = list.Find(item =>
        {
            if (item.name == info.name && item.password == info.password)
                return true;
            else
                return false;
        });
        return temp != null;
    }
    public bool RegisterUser(UserInfo info)
    {
        if (list != null)
        {
            list.Add(info);
            AddUserToList(info);
        }
        return true;
    }
    private void AddUserToList(UserInfo info)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(pathXML);
        XmlNode root = xml.SelectSingleNode("root");

        XmlElement el = xml.CreateElement("User");

        XmlAttribute nameAttribute = xml.CreateAttribute("name");
        nameAttribute.Value = info.name;
        el.SetAttributeNode(nameAttribute);

        XmlAttribute passwordAttribute = xml.CreateAttribute("password");
        passwordAttribute.Value = info.password.ToString();
        el.SetAttributeNode(passwordAttribute);

        XmlAttribute emailAttribute = xml.CreateAttribute("email");
        emailAttribute.Value = info.email.ToString();
        el.SetAttributeNode(emailAttribute);

        root.AppendChild(el);
        xml.Save(pathXML);
    }
}
