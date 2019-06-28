using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System.Text;

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
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (event01 != null)
                event01.Invoke("Hello damon!");
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

            SocketMessage socketMessage = new SocketMessage(Module.Login,SubModule.Attack,"damon is strong");
            networkManager.SendAsync(socketMessage);

        }
    }
    void OnDestroy()
    {
        networkManager.Close();
    }
}
