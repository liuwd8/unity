using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiAction : MonoBehaviour {
    FirstSceneController sceneController;
    GameObject myobject = null;
    GameObject player;
    public delegate void AddScore();
    public static event AddScore myAddScore;

    public delegate void GameOver();
    public static event GameOver myGameOver;

    private void Start()
    {
        sceneController = SSDirector.getInstance().currentSceneController as FirstSceneController;
        player = sceneController.player;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            int k = this.name[this.name.Length - 1] - '0';
            myobject = sceneController.patrols[k];
            foreach (var i in sceneController.actionManager.seq)
            {
                if (i.gameObject == myobject)
                {
                    i.enable = false;
                    Vector3 a = new Vector3(myobject.transform.position.x, 0, myobject.transform.position.z);
                    Vector3 b = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                    Quaternion rotation = Quaternion.LookRotation(b - a, Vector3.up);
                    myobject.transform.rotation = rotation;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            int k = this.name[this.name.Length - 1] - '0';
            foreach (var i in sceneController.actionManager.seq)
            {
                if (i.gameObject == myobject)
                {
                    i.enable = true;
                    Vector3 a = new Vector3(myobject.transform.position.x, 0, myobject.transform.position.z);
                    Vector3 b = new Vector3(i.target.x, 0, i.target.z);
                    Quaternion rotation = Quaternion.LookRotation(b - a, Vector3.up);
                    myobject.transform.rotation = rotation;
                }
            }
            myobject = null;
            myAddScore();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(this.tag == "patrol" && collision.gameObject == player)
        {
            myGameOver();
            var k = collision.gameObject.GetComponent<Animator>();
            k.SetBool("death",true);
        }
    }
    private void Update()
    {
        if (myobject != null && sceneController.flag == 0)
        {
            myobject.transform.position = Vector3.MoveTowards(myobject.transform.position, player.transform.position, 3 * Time.deltaTime);
        }
    }
}
