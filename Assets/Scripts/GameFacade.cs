using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System.Text;
using ProtoBuf;

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

    void Awake()
    {
        _instance = this;
        networkManager = NetworkManager.instance;
        resManager = ResourcesManager.instance;
        uiManager = UIManager.instance;
    }
	void Start () {

        //SocketMessage socketMessage = new SocketMessage(Module.Login, SubModule.Attack, "damon is strong");
        //networkManager.SendAsync(socketMessage);

        //SocketMessage socketMessage01 = new SocketMessage(Module.Role, SubModule.Walk, "damon02 is strong");
        //networkManager.SendAsync(socketMessage01);

        LoginInfo loginInfo = new LoginInfo { name="damon",password="CWH919609cwh"};
        networkManager.SendAsync<LoginInfo>(loginInfo,MainCommand.Login,SubCommand.Login);
    }
    void Update () {

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

            /*
            NetPacket netPacket = new NetPacket(0,index,(int)CommandType.Login,10,200,new string[2] { "damon01",NetworkManager.instance.client.socket.LocalEndPoint.ToString()});
            netPacket.ReadData();
            networkManager.SendAsync(netPacket);
            */

            //SocketMessage socketMessage = new SocketMessage(Module.Login,SubModule.Attack,"damon is strong");
            //networkManager.SendAsync(socketMessage);

        }
    }
    void OnDestroy()
    {
        networkManager.Close();
    }
}
