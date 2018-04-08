using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {

    private static SSDirector _instance;
    public ISceneController currentSceneController { get; set; }
    public bool running { get; set; }
    private List<int> passedDevils;
    private List<int> passedPriests;

    SSDirector()
    {
        passedDevils = new List<int>();
        passedPriests = new List<int>();
    }

    public static SSDirector getInstance()
    {
        if (_instance == null)
            _instance = new SSDirector();
        return _instance;
    }

    public int getFPS()
    {
        return Application.targetFrameRate;
    }

    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }
    public void NextScene()
    {

    }
}
