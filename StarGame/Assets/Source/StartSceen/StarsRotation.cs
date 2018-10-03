using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class StarsRotation : MonoBehaviour {

    public float speed = 5;
    public float Opacity = 1;

    // Use this for initialization
    void Start () {
        GetComponent<Image>().color = new Color(1, 1, 1, Opacity);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * -speed);
        
	}
}
