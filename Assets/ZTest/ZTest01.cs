using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTest01 : MonoBehaviour {

    void Start () {

        NetworkStream stream = new NetworkStream("damon");
        foreach (byte b in stream.data)
        {
            Debug.Log(b);
        }
	}
	void Update () {
		
	}
}
