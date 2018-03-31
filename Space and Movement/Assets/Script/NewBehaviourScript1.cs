using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {
    private float r;
    private Vector3 axis;
    // Use this for initialization
    void Start () {
        r = this.transform.position.x;
        float rx = Random.Range(-1.0f, 1.0f);
        float rz = Mathf.Sqrt(1 - rx * rx) * (Random.Range(0,2) == 1 ? -1 : 1);
        axis = new Vector3(rx == 0.0f ? 1.0f : -rz/rx, 0 ,rx == 0.0f ? 0.0f : 1.0f);
        this.transform.position = new Vector3(rx,0, rz) * r;
    }
	
	// Update is called once per frame
	void Update () {
        float speed = 100.0f * Mathf.Sqrt(1.0f / (r * r * r));
        this.transform.RotateAround(Vector3.zero, axis,speed *Time.deltaTime);
        this.transform.Rotate(axis, 10.0f* Time.deltaTime);
	}
}
