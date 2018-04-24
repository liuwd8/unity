using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private FirstSceneController action;
    private GUIStyle fontstyle1 = new GUIStyle();
    // Use this for initialization
    void Start () {
        action = SSDirector.getInstance().currentSceneController as FirstSceneController;
        fontstyle1.fontSize = 50;
    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 80, 80, 60), "RESTART"))
        {
            action.Restart();
        }
        if (GUI.Button(new Rect(0, 160, 80, 60), "Pause"))
        {
            action.Pause();
        }
        if (action.flag == 0)
        {
            int k = (Mathf.CeilToInt(FirstSceneController.times / 10) + 1);
            fontstyle1.normal.textColor = Color.green;
            GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Score: " +
                action.score + ", Round: " + (k > 3 ? 3 : k), fontstyle1);
        }
        else if (action.flag == 1)
        {
            fontstyle1.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Your score is : " + action.score, fontstyle1);
        }
        else
        {
            int k = (Mathf.CeilToInt(FirstSceneController.times / 10) + 1);
            fontstyle1.normal.textColor = Color.green;
            GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Score: " +
                action.score + ", Round: " + (k > 3 ? 3 : k), fontstyle1);
            fontstyle1.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height/2-50, 300, 100), "Pause!", fontstyle1);
        }
    }
}
