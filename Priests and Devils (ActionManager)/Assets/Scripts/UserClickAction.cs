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
        FirstSceneController sceneController = SSDirector.getInstance().currentSceneController as FirstSceneController;
        if (sceneController.onBoatDevil.Contains(gameObject) || sceneController.onBoatPriest.Contains(gameObject))
        {
            int f = sceneController.boat.transform.position.x < 0 ? 1 : -1;
            int i = gameObject.name[gameObject.name.Length - 1] - '0';
            gameObject.transform.parent = sceneController.transform;
            gameObject.transform.position = new Vector3(f * (-4.9f - i * 0.6f), 1.0f, 0);
            if(gameObject.tag == "devil")
            {
                sceneController.onBoatDevil.Remove(gameObject);
                if (f > 0) sceneController.startDevil.Add(gameObject);
                else sceneController.endDevil.Add(gameObject);
            }
            else
            {
                sceneController.onBoatPriest.Remove(gameObject);
                if (f > 0) sceneController.startPriest.Add(gameObject);
                else sceneController.endPriest.Add(gameObject);
            }
            sceneController.boatCapacity++;
        }
        else if (sceneController.boatCapacity > 0)
        {
            float i = sceneController.boat.transform.position.x * gameObject.transform.position.x;
            if (i < 0) return;
            int f = sceneController.boat.transform.position.x < 0 ? 1 : -1;
            gameObject.transform.parent = sceneController.boat.transform;
            if (sceneController.boatCapacity == 1)
            {
                Vector3 onBoatGameObjectPosition = (sceneController.onBoatPriest.Count == 1 ? sceneController.onBoatPriest[0] : sceneController.onBoatDevil[0]).transform.position;
                gameObject.transform.position = new Vector3(-onBoatGameObjectPosition.x, onBoatGameObjectPosition.y,onBoatGameObjectPosition.z) + 2 * sceneController.boat.transform.position;
            }
            else
                gameObject.transform.position = new Vector3(-0.6f, 0.25f) + sceneController.boat.transform.position;
            if (gameObject.tag == "devil")
            {
                sceneController.onBoatDevil.Add(gameObject);
                if (f > 0) sceneController.startDevil.Remove(gameObject);
                else sceneController.endDevil.Remove(gameObject);
            }
            else
            {
                sceneController.onBoatPriest.Add(gameObject);
                if (f > 0) sceneController.startPriest.Remove(gameObject);
                else sceneController.endPriest.Remove(gameObject);
            }
            sceneController.boatCapacity--;
        }
        this.destory = true;
    }
}
