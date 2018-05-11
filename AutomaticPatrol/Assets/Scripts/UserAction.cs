using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAction : SSAction {
    // Use this for initialization
    private float speed;
    private float rspeed = 90;
    
    public static UserAction GetSSAction(float speed)
    {
        UserAction action = CreateInstance<UserAction>();
        action.speed = speed;
        return action;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        if(enable)
        {
            float translationX = Input.GetAxis("Horizontal");
            float translationZ = Input.GetAxis("Vertical");
            if (translationX != 0 || translationZ != 0)
            {
                gameObject.GetComponent<Animator>().SetBool("running", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("running", false);
            }
            gameObject.transform.Translate(translationX * speed * Time.deltaTime * 0.1f, 0,translationZ*speed*Time.deltaTime);
            gameObject.transform.Rotate(0, translationX * rspeed * Time.deltaTime, 0);
            if (gameObject.transform.localEulerAngles.x != 0 || gameObject.transform.localEulerAngles.z != 0)
            {
                gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localEulerAngles.y, 0);
            }
            if (gameObject.transform.position.y != 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            }
        }
    }
}
