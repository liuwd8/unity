using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSequenceAction : SSAction, ISSActionCallback
{
    public List<SSAction> sequence;
    public int repeat = -1;
    public int start = 0;

    public static CCSequenceAction GetSSAction(int repeat, int start, List<SSAction> sequence)
    {
        CCSequenceAction action = CreateInstance<CCSequenceAction>();
        action.repeat = repeat;
        action.start = start;
        action.sequence = sequence;
        return action;
    }
    public override void Update()
    {
        if (sequence.Count <= 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();
        }
    }
    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameObject = this.gameObject;
            action.transform = this.transform;
            action.callback = this.callback;
            action.Start();
        }
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        source.destory = false;
        this.start++;
        if(start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destory = true;
                this.callback.SSActionEvent(this);
            }
        }
    }
    public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        
    }
    void OnDestroy()
    {
        Destroy(this);
    }
}
