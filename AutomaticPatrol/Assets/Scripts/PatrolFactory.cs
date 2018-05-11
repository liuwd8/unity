using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour {
    public List<GameObject> used = new List<GameObject>();
    public List<GameObject> free = new List<GameObject>();

	// Use this for initialization
	void Start () { }

    public void GenPatrol()
    {
        if(free.Count == 0)
        {
            GameObject patrol = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/patrol"), Vector3.zero, Quaternion.identity);
            used.Add(patrol);
        }
        else
        {
            foreach(var g in free)
            {
                used.Add(g);
            }
        }
    }
    public GameObject GetPatrol()
    {
        if(used.Count == 0)
        {
            GenPatrol();
        }
        GameObject g = used[0];
        used.RemoveAt(0);
        return g;
    }
    public void RecyclePatrol(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        free.Add(obj);
    }
}