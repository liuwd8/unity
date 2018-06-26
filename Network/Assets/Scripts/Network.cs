using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : NetworkBehaviour {
    public GameObject bulletPrefab;
    public Color color = Color.red;
    private float speed = 3;
    private float rspeed = 90;
    private Camera myCamera;

    public override void OnStartLocalPlayer()
    {
        color = Color.green;
        myCamera = Camera.main;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }
    // Update is called once per frame
    void Update () {
        if(isLocalPlayer)
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
            gameObject.transform.Translate(translationX * speed * Time.deltaTime * 0.1f, 0, translationZ * speed * Time.deltaTime);
            gameObject.transform.Rotate(0, translationX * rspeed * Time.deltaTime, 0);
            if (gameObject.transform.localEulerAngles.x != 0 || gameObject.transform.localEulerAngles.z != 0)
            {
                gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localEulerAngles.y, 0);
            }
            if (gameObject.transform.position.y != 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            }
            myCamera.transform.position = transform.position + transform.forward * -3 + new Vector3(0, 4, 0);
            myCamera.transform.forward = transform.forward + new Vector3(0, -0.5f, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<Animator>().SetBool("shoot", true);
                CmdFire();
                gameObject.GetComponent<Animator>().SetBool("shoot", false);
            }
        }
    }
    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var bullet = (GameObject)Instantiate(bulletPrefab,transform.position + transform.forward + new Vector3(0,1,0),Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 4;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 2.0f);
    }
}
