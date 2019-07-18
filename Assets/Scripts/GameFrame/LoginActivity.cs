using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LoginActivity : MonoBehaviour {

    public Activity activity;
	// Use this for initialization
	void Start () {

        if (activity == null) return;

        activity.Awake();
	}
	void Update () 
    {
        
	}

    private void OnDestroy()
    {
        if (activity != null)
            activity.OnDestroy();
    }
}
