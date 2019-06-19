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
            networkManager.SendAsync("damon 你好!" + index);
        }
    }
    void OnDestroy()
    {
        networkManager.Close();
    }
}
