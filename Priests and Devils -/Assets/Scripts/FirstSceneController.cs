using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController{
    private GameObject boat;
    private GameObject river;
    private int devils_s;
    private int priests_s;
    private int devils_e;
    private int priests_e;
    private int devils_boat;
    private int priests_boat;
    private List<GameObject> OnBoat;
    private bool isFull;
    private bool isRunning;
    private Vector3 endPos;
    private int flag;

    private void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.GenGameObjects();
    }
    private void Start()
    {
        devils_s = 3;
        priests_s = 3;
        devils_boat = 0;
        priests_boat = 0;
        devils_e = 0;
        priests_e = 0;
        flag = 0;
        OnBoat = new List<GameObject>();
        isFull = false;
        isRunning = false;
        endPos = boat.transform.position;
    }
    public void GenGameObjects ()
    {
        river = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/river"), Vector3.zero, Quaternion.identity);
        river.name = "river";
        river.transform.parent = this.transform;
        boat = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/boat"));
        boat.name = "boat";
        boat.transform.parent = this.transform;
        boat.transform.position = new Vector3(-3.5f,0);
        for (int i=1;i<=6;++i)
        {
            string str = i <= 3 ? "Priest" : "Devils";
            GameObject priest = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/" + str));
            priest.name = str + i;
            priest.transform.parent = this.transform;
            priest.transform.position = new Vector3(- 4.9f - i*0.6f, 1.0f, 0);
        }
    }
    public void UserClick(GameObject target)
    {
        if (!isRunning&& flag == 0)
        {
            if (OnBoat.Contains(target))
            {
                int f = boat.transform.position.x < 0 ? 1 : -1;
                int i = target.name[target.name.Length - 1] - '0';
                target.transform.parent = this.transform;
                target.transform.position = new Vector3(f * (-4.9f - i * 0.6f), 1.0f, 0);
                isFull = false;
                if (i <= 3) priests_boat--;
                else devils_boat--;
                OnBoat.Remove(target);
            }
            else if (OnBoat.Count < 2)
            {
                int i = target.name[target.name.Length - 1] - '0';
                if (i <= 3) priests_boat++;
                else devils_boat++;
                target.transform.parent = boat.transform;
                if (OnBoat.Count == 1)
                    target.transform.position = new Vector3(-OnBoat[0].transform.position.x, OnBoat[0].transform.position.y, OnBoat[0].transform.position.z) + 2*boat.transform.position;
                else
                    target.transform.position = new Vector3(-0.6f, 0.25f) + boat.transform.position;
                OnBoat.Add(target);
            }
            else
                isFull = true;
        }
    }
    public void BoatMove()
    {
        boat.transform.position = Vector3.MoveTowards(boat.transform.position, endPos, 2 * Time.deltaTime);
    }
    public void Restart()
    {
        SceneManager.LoadScene("1");
    }
    public void Pause ()
    {
        if (flag == 4)
            flag = 0;
        else
            flag = 4;
    }
    private void OnGUI()
    {
        if (isRunning && flag == 0)
        {
            BoatMove();
        }
        if (boat.transform.position == endPos || flag != 0)
        {
            isRunning = false;
            if( flag == 1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), "You Win!");
            }
            else if (flag == 2)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), "You Lose!");
            }
            else if (GUI.Button(new Rect(Screen.width / 2 - 50, 0, 100, 100), "GO") && OnBoat.Count > 0 && !isRunning && flag == 0)
            {
                int f = boat.transform.position.x < 0 ? 1 : -1;
                if (f == 1)
                {
                    priests_e += priests_boat;
                    devils_e += devils_boat;
                    priests_s = 3 - priests_e;
                    devils_s = 3 - devils_e;
                }
                else
                {
                    priests_s += priests_boat;
                    devils_s += devils_boat;
                    priests_e = 3 - priests_s;
                    devils_e = 3 - devils_s;
                }
                if (priests_s == 0 && devils_s == 0)
                {
                    flag = 1;
                }
                else if ((priests_s < devils_s && priests_s != 0) || (priests_e < devils_e && priests_e != 0))
                {
                    flag = 2;
                }
                endPos = new Vector3(boat.transform.position.x * -1, boat.transform.position.y, boat.transform.position.z);
                isRunning = true;
            }
        }
    }
}