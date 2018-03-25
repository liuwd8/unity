using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //public Transform prefab;

    private void Start()
    {
       // Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        gameObject.BroadcastMessage("ApplyDamage", 5.0F);
    }
    void ApplyDamage(float damage)
    {
        print(damage);
    }
}