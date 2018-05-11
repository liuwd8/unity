using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController{
    public CCActionManager actionManager;
    public GameObject player;
    public List<GameObject> patrols = new List<GameObject>();
    public PatrolFactory pf;
    public int flag = 0;
    public int score = 0;

    private void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        this.gameObject.AddComponent<PatrolFactory>();
        pf = Singleton<PatrolFactory>.Instance;
        this.gameObject.AddComponent<UserGUI>();
        director.currentSceneController.GenGameObjects();
        this.gameObject.AddComponent<CCActionManager>();
    }
    private void OnEnable()
    {
        ColiAction.myAddScore += AddScore;
        ColiAction.myGameOver += GameOver;
    }
    private void OnDisable()
    {
        ColiAction.myAddScore -= AddScore;
        ColiAction.myGameOver -= GameOver;
    }

    private void GameOver()
    {
        Pause();
        flag = 1;
    }

    private void Start()
    {
    }
    public void GenGameObjects ()
    {
        int count = 0;
        GameObject plane = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Plane"));
        plane.transform.parent = this.transform;
        for(int i=0;i<3;++i)
        {
            for(int j=0;j<3;++j)
            {
                if (i == 0 && j == 2)
                {
                    player = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/player"));
                    player.transform.position = new Vector3(plane.transform.position.x + 9 * (i - 1), 0, plane.transform.position.z + 9 * (j - 1));
                    if (player.GetComponent<Rigidbody>())
                    {
                        player.GetComponent<Rigidbody>().freezeRotation = true;
                    }
                }
                else
                {
                    GameObject trigger = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Trigger"));
                    trigger.transform.parent = plane.transform;
                    trigger.transform.position = new Vector3(plane.transform.position.x + 9 * (i - 1), 0, plane.transform.position.z + 10 * (j - 1));
                    trigger.name = "trigger" + count;
                    count++;
                    GameObject patrol = pf.GetPatrol();
                    patrol.transform.position = trigger.transform.position;
                    patrols.Add(patrol);
                }
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("1");
    }
    public void Pause ()
    {
        actionManager.Pause();
    }
    private void AddScore()
    {
        score++;
    }
}