using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {
    public FirstSceneController sceneController;
    public List<CCMoveToAction> seq = new List<CCMoveToAction>();
    public UserAction userAction;
    public PatrolFactory pf;

    protected new void Start()
    {
        sceneController = (FirstSceneController)SSDirector.getInstance().currentSceneController;
        userAction = UserAction.GetSSAction(5);
        sceneController.actionManager = this;
        pf = sceneController.pf;
        RunAction(sceneController.player, userAction, this);
        foreach (var i in sceneController.patrols)
        {
            float x = Random.Range(-3.0f, 3.0f);
            int z = Random.Range(0, 4);
            Vector3 target = new Vector3(z % 2 == 0 ? (z - 1) * 3 : x, 0, z % 2 != 0 ? (z - 2) * 3 : x);
            CCMoveToAction k = CCMoveToAction.GetSSAction(target+i.transform.position,100,i.transform.position);
            seq.Add(k);
            Quaternion rotation = Quaternion.LookRotation(target, Vector3.up);
            i.transform.rotation = rotation;
            RunAction(i, k, this);
        }
    }
    protected new void Update()
    {
        base.Update();
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        if(source != userAction)
        {
            CCMoveToAction cCMoveTo = source as CCMoveToAction;
            float x = Random.Range(-3.0f, 3.0f);
            int z = Random.Range(0, 4);
            Vector3 target = new Vector3(z % 2 == 0 ? (z - 1) * 3.0f : x, 0, z % 2 == 0 ? x : (z - 2) * 3.0f);
            CCMoveToAction k = CCMoveToAction.GetSSAction(target + cCMoveTo.initPosition, 1.5f, cCMoveTo.initPosition);
            seq.Remove(cCMoveTo);
            source.destory = true;
            seq.Add(k);
            Quaternion rotation = Quaternion.LookRotation(target + cCMoveTo.initPosition - source.transform.position, Vector3.up);
            source.transform.rotation = rotation;
            RunAction(source.gameObject, k, this);
        }
    }
    public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
    }
    public void Pause()
    {
        if(sceneController.flag == 0)
        {
            foreach (var k in seq)
            {
                k.enable = false;
                k.gameObject.GetComponent<Animator>().SetBool("running", false);
            }
            userAction.enable = false;
            sceneController.flag = 2;
        }
        else if(sceneController.flag == 2)
        {
            foreach (var k in seq)
            {
                k.enable = true;
                k.gameObject.GetComponent<Animator>().SetBool("running", true);
            }
            userAction.enable = true;
            sceneController.flag = 0;
        }
    }
}
