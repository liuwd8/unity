using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour {
    public List<GameObject> used = new List<GameObject>();
    public List<GameObject> free = new List<GameObject>();

	// Use this for initialization
	void Start () { }

    public void GenDisk()
    {
        GameObject disk;
        if(free.Count == 0)
        {
            disk = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity);
        }
        else
        {
            disk = free[0];
            free.RemoveAt(0);
        }
        float x = Random.Range(-10.0f, 10.0f);
        disk.transform.position = new Vector3(x, 0, 0);
        disk.transform.Rotate(new Vector3(x < 0? -x*9 : x*9, 0, 0));
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        disk.transform.GetComponent<Renderer>().material.color = color;
        used.Add(disk);
    }
    public void RecycleDisk(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        free.Add(obj);
    }
}