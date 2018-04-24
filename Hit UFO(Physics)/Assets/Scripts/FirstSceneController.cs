using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController{
    public IActionManager actionManager;
    public GameObject disk;
    protected DiskFactory df;
    public int flag = 0;
    private float interval = 3;
    public int score = 0;
    public static int times = 0;

    private void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        this.gameObject.AddComponent<DiskFactory>();
        this.gameObject.AddComponent<PhysicsActionManager>();
        this.gameObject.AddComponent<UserGUI>();
        df = Singleton<DiskFactory>.Instance;
    }
    private void Start()
    {
    }
    public void GenGameObjects ()
    {
    }
    public void Restart()
    {
        SceneManager.LoadScene("1");
    }
    public void Pause ()
    {
        actionManager.Pause();
    }
    public void Update()
    {
        if (times < 30 && flag == 0)
        {
            if (interval <= 0)
            {
                interval = Random.Range(3, 5);
                times++;
                df.GenDisk();
            }
            interval -= Time.deltaTime;
        }
        actionManager.PlayDisk();
    }
}