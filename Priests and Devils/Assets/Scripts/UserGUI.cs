using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
	// Use this for initialization
	void Start () {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
	}

    // Update is called once per frame
    private void OnGUI()
    {
        GUIStyle fontstyle1 = new GUIStyle();
        fontstyle1.fontSize = 50;
        fontstyle1.normal.textColor = new Color(255, 255, 255);
        if (GUI.Button(new Rect(0, 80, 80, 60), "RESTART"))
        {
            action.Restart();
        }
        if (GUI.Button(new Rect(0, 160, 80, 60), "Pause"))
        {
            action.Pause();
        }
    }
}
