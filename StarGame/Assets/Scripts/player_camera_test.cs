using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera_test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    float y = 0f;
	// Update is called once per frame
	void Update () {
        y += Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
