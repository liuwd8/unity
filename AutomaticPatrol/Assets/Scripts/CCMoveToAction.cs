using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    public Vector3 target;
    public float speed;
    public Vector3 initPosition;

    public static CCMoveToAction GetSSAction(Vector3 target, float speed,Vector3 initPosition)
    {
        CCMoveToAction action = CreateInstance<CCMoveToAction>();
        action.target = target;
        action.speed = speed;
        action.initPosition = initPosition;
        return action;
    }

    public override void Start()
    {
        
    }
    public override void Update()
    {
        if(enable)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed*Time.deltaTime);
            if (this.transform.position == target)
            {
                this.enable = false;
                this.destory = true;
                this.callback.SSActionEvent(this);
            }
        }
    }
}