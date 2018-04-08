using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClick : MonoBehaviour {
    private IUserAction action;
    // Use this for initialization
    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        action.UserClick(this.gameObject);
    }
}
