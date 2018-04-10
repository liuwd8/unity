using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController{
    public CCActionManager actionManager;
    public GameObject boat;
    public List<GameObject> startPriest = new List<GameObject>();
    public List<GameObject> startDevil = new List<GameObject>();
    public List<GameObject> endPriest = new List<GameObject>();
    public List<GameObject> endDevil = new List<GameObject>();
    public List<GameObject> onBoatPriest = new List<GameObject>();
    public List<GameObject> onBoatDevil = new List<GameObject>();
    public int boatCapacity = 2;
    public int flag = 0;
    
    private void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.GenGameObjects();
    }
    private void Start()
    {
    }
    public void GenGameObjects ()
    {
        GameObject river = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/river"), Vector3.zero, Quaternion.identity);
        river.name = "river";
        river.transform.parent = this.transform;
        boat = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/boat"));
        boat.name = "boat";
        boat.transform.parent = this.transform;
        boat.transform.position = new Vector3(-3.5f,0);
        for (int i=1;i<=3;++i)
        {
            GameObject person = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Priest"));
            person.name = "Priest" + i;
            person.transform.parent = this.transform;
            person.transform.position = new Vector3(- 4.9f - i*0.6f, 1.0f, 0);
            startPriest.Add(person);
        }
        for (int i = 1; i <= 3; ++i)
        {
            GameObject person = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/Devils"));
            person.name = "Devils" + (i+3);
            person.transform.parent = this.transform;
            person.transform.position = new Vector3(-4.9f - (i+3) * 0.6f, 1.0f, 0);
            startDevil.Add(person);
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
}