using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour {
    private void Start()
    {
        GameObject plane = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Plane"));
        plane.transform.parent = this.transform;
    }
}