using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {
    public Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    public List<SSAction> waitingAdd = new List<SSAction>();
    public List<int> waitingDelete = new List<int>();

    protected void Update()
    {
        foreach (SSAction ac in waitingAdd)
            actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();
        foreach (KeyValuePair <int, SSAction> kv in actions)
        {
            if (kv.Value.destory)
                waitingDelete.Add(kv.Value.GetInstanceID());
            else
                kv.Value.Update();
        }
        foreach (int key in waitingDelete)
        {
            DestroyObject(actions[key]);
            actions.Remove(key);
        }
        waitingDelete.Clear();
    }
    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback manager)
    {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }
}
