using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{
    public bool enable = true;
    public bool destory = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    protected SSAction() { }

	// Use this for initialization
    public virtual void Start () {
        throw new System.NotImplementedException();
	}

    // Update is called once per frame
    public virtual void Update() {
        throw new System.NotImplementedException();
    }
}