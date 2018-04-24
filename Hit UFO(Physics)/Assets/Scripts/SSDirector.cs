using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {

    private static SSDirector _instance;
    public ISceneController currentSceneController { get; set; }

    SSDirector() { }

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
