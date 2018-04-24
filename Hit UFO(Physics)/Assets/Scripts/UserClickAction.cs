using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClickAction : SSAction {
    // Use this for initialization

    public static UserClickAction GetSSAction()
    {
        UserClickAction action = CreateInstance<UserClickAction>();
        return action;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        if(enable)
        {
            FirstSceneController sc = SSDirector.getInstance().currentSceneController as FirstSceneController;
            sc.score = sc.score + Mathf.CeilToInt(FirstSceneController.times/10) + Mathf.FloorToInt(75/(transform.rotation.x+30));
            destory = true;
        }
    }
}
