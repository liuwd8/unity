using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager {
    public FirstSceneController sceneController;
    public List<CCMoveToAction> seq = new List<CCMoveToAction>();
    public UserClickAction userClickAction;
    public DiskFactory disks;
    
    protected void Start()
    {
        sceneController = (FirstSceneController)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this;
        disks = Singleton<DiskFactory>.Instance;
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        disks.RecycleDisk(source.gameObject);
        seq.Remove(source as CCMoveToAction);
        source.destory = true;
        if (FirstSceneController.times >= 30)
            sceneController.flag = 1;
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
            }
            sceneController.flag = 2;
        }
        else if(sceneController.flag == 2)
        {
            foreach (var k in seq)
            {
                k.enable = true;
            }
            sceneController.flag = 0;
        }
    }
    public void PlayDisk() {
        if (disks.used.Count > 0)
        {
            GameObject disk = disks.used[0];
            float x = Random.Range(-10, 10);
            CCMoveToAction moveToAction = CCMoveToAction.GetSSAction(new Vector3(x, 12, 0), 3 * (Mathf.CeilToInt(FirstSceneController.times / 10) + 1) * Time.deltaTime);
            seq.Add(moveToAction);
            this.RunAction(disk, moveToAction, this);
            disks.used.RemoveAt(0);
        }
        if (Input.GetMouseButtonDown(0) && sceneController.flag == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitGameObject;
            if (Physics.Raycast(ray, out hitGameObject))
            {
                GameObject gameObject = hitGameObject.collider.gameObject;
                if (gameObject.tag == "disk")
                {
                    foreach (var k in seq)
                    {
                        if (k.gameObject == gameObject)
                            k.transform.position = k.target;
                    }
                    userClickAction = UserClickAction.GetSSAction();
                    this.RunAction(gameObject, userClickAction, this);
                }
            }
        }
        base.Update();
    }
}
