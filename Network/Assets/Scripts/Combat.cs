using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SyncVar]
    public int health = maxHealth;

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            health = 100;
            GetComponent<Animator>().SetBool("death", true);
            GetComponent<Animator>().SetBool("death", false);
            RpcRespawn();
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}