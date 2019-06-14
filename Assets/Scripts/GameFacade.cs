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

    void Awake()
    {
        _instance = this;
       networkManager = NetworkManager.instance;

        resManager = ResourcesManager.instance;
        uiManager = UIManager.instance;
    }
	void Start () {
        for (int i = 0; i < 10; i++)
        {
            networkManager.SendAsync(i+"");
        }
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (event01 != null)
                event01.Invoke("Hello damon!");
        }
	}
    void OnDestroy()
    {
        networkManager.Close();
    }
}
