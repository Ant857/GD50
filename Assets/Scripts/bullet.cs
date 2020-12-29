using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private float speed = 7f;
    private float lifeTimer = 10f;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health = collision.gameObject.GetComponent<PlayerController>().health - damage;
            collision.gameObject.GetComponent<PlayerController>().tookDamage = true;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "AI")
        {
            if (collision.gameObject.GetComponent<AIControllerPistol>() != null)
            {
                collision.gameObject.GetComponent<AIControllerPistol>().health = collision.gameObject.GetComponent<AIControllerPistol>().health - damage;
            }
            else if (collision.gameObject.GetComponent<AIControllerSniper>() != null)
            {
                collision.gameObject.GetComponent<AIControllerSniper>().health = collision.gameObject.GetComponent<AIControllerSniper>().health - damage;
            }
            else if (collision.gameObject.GetComponent<AIControllerMachineGun>() != null)
            {
                collision.gameObject.GetComponent<AIControllerMachineGun>().health = collision.gameObject.GetComponent<AIControllerMachineGun>().health - damage;
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;

        if (lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
