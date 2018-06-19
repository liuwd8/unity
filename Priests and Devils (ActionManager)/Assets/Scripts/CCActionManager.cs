using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {
    public FirstSceneController sceneController;
    public CCMoveToAction moveToAction,moveToActionB;
    public UserClickAction userClickAction;
    
    protected new void Start()
    {
        sceneController = (FirstSceneController)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this;
    }
    protected new void Update()
    {
        if (Input.GetMouseButtonDown(0) && sceneController.flag == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitGameObject;
            if (Physics.Raycast(ray, out hitGameObject))
            {
                GameObject gameObject = hitGameObject.collider.gameObject;
                if (gameObject.tag == "devil" || gameObject.tag == "priest")
                {
                    OnBoat(gameObject,this);
                }
                else if (gameObject.transform.parent.name == "boat" && sceneController.boatCapacity < 2 && (moveToAction == null || !moveToAction.enable))
                {
                    MoveBoat(sceneController.boat,this);
                }
            }
        }
        base.Update();
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        if(source == moveToAction)
        {
            userClickAction.enable = !source.enable;
        }
    }
    public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        if (source == moveToAction)
        {
            foreach (GameObject a in sceneController.onBoatDevil)
            {
                OnBoat(a, this);
            }
            foreach (GameObject a in sceneController.onBoatPriest)
            {
                OnBoat(a, this);
            }
            int startDevilNum = sceneController.startDevil.Count + (source.transform.position.x < 0 ? sceneController.onBoatDevil.Count : 0);
            int startPriestNum = sceneController.startPriest.Count + (source.transform.position.x < 0 ? sceneController.onBoatPriest.Count : 0);
            if (startDevilNum > startPriestNum && startPriestNum != 0)
                sceneController.flag = 2;
            else if (startPriestNum + startDevilNum == 0)
                sceneController.flag = 1;
        }
    }
    public void Pause()
    {
        if (sceneController.flag == 0)
        {
            moveToActionB = moveToAction;
            moveToAction.enable = false;
            sceneController.flag = 4;
        }
        else if (sceneController.flag == 4)
        {
            moveToAction = moveToActionB;
            moveToAction.enable = true;
            this.RunAction(moveToAction.gameObject, moveToAction, moveToAction.callback);
            sceneController.flag = 0;
        }
    }
    public void OnBoat(GameObject gameObject,ISSActionCallback callback)
    {
        userClickAction = UserClickAction.GetSSAction();
        this.RunAction(gameObject, userClickAction, callback);
    }
    public void MoveBoat(GameObject gameObject, ISSActionCallback callback)
    {
        moveToAction = CCMoveToAction.GetSSAction(-gameObject.transform.position, 10 * Time.deltaTime);
        this.RunAction(gameObject, moveToAction, this);
    }
}
