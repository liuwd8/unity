using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public GameObject center;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = center.transform.position;
        this.transform.Rotate(Vector3.up, 10.0f * Time.deltaTime);
    }
}
