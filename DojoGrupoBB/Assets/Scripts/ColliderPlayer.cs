using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayer : MonoBehaviour
{
    public bool arma;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < Main.instance.player.Length; i++)
        {
            if (other.gameObject == Main.instance.player[i].player)
            {
                other.gameObject.GetComponent<ColliderPlayer>().arma = Main.instance.player[i].arma;
            }
            if (Main.instance.player[i].player == this.gameObject)
            {
                
                Main.instance.player[i].OnCollisionEnter(other);

            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < Main.instance.player.Length; i++)
        {
            Main.instance.player[i].OnCollisionExit(other);
        }

    }

}
