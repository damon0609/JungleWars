using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System.Text;
using ProtoBuf;
using UnityEngine.Networking;

/*
[ProtoContract]
public class LoginInfo
{
    [ProtoMember(1)]
    public string name { get; set; }

    [ProtoMember(2)]
    public string password { get; set; }
}

[ProtoContract]
public class Attack
{
    [ProtoMember(1)]
    public float act { get; set; }
}
*/


public class GameFacade : MonoBehaviour {

    public UIManager uiManager;
    public ResourcesManager resManager;
    public NetworkManager networkManager;

    private static GameFacade _instance;
    public static GameFacade instance
    {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<GameFacade>();
            return _instance;
        }
    }

    public EventHandler<string> event01 = new EventHandler<string>();
    private float timer = 0.5f;
    private int index;


    public Canvas uiCanvas;

    //服务器上地址
    private const string path = @"D:\Damon\Year_2019\Private\Project_C#\服务器\接受Protobuf数据的服务器\bin\Debug\userList.txt";
    private string dataStr;
    void Awake()
    {
        _instance = this;

        //通过www在后台将用户数据下载到本地
        //StartCoroutine(LoadData());

        /*
        networkManager = NetworkManager.instance;
        resManager = ResourcesManager.instance;
        uiManager = UIManager.instance;
        */
    }

    void Init()
    {
        
    }

	void Start () {


        #region Test 部分
        //SocketMessage socketMessage = new SocketMessage(Module.Login, SubModule.Attack, "damon is strong");
        //networkManager.SendAsync(socketMessage);

        //SocketMessage socketMessage01 = new SocketMessage(Module.Role, SubModule.Walk, "damon02 is strong");
        //networkManager.SendAsync(socketMessage01);

        //LoginInfo loginInfo = new LoginInfo { name="damon",password="CWH919609cwh"};
        //networkManager.SendAsync<LoginInfo>(loginInfo,MainCommand.Login,SubCommand.Login);
        #endregion
    }
    void Update () {


        #region Test部分
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (event01 != null)
                event01.Invoke("Hello damon!");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack attack = new Attack { act = 1.9f };
            networkManager.SendAsync<Attack>(attack, MainCommand.Attack, SubCommand.None);
        }

        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            index++;
            timer = 0.5f;

            NetPacket netPacket = new NetPacket(0,index,(int)CommandType.Login,10,200,new string[2] { "damon01",NetworkManager.instance.client.socket.LocalEndPoint.ToString()});
            netPacket.ReadData();
            networkManager.SendAsync(netPacket);

            //SocketMessage socketMessage = new SocketMessage(Module.Login,SubModule.Attack,"damon is strong");
            //networkManager.SendAsync(socketMessage);
        }
        */

        #endregion 
    }
    void OnDestroy()
    {
        //networkManager.Close();
    }

    /*
    IEnumerator LoadData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://pan.baidu.com/s/1eDwUD0r3UhgqYFX7D63Ptw"))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
                Debug.Log(request.isNetworkError);
            else
                Debug.Log(request.downloadHandler.text);
        }
    }
    */
}
