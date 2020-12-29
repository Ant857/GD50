using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 mousePosition;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    public Camera mainCamera;
    public Text holdableText;
    public Canvas canvas;
    public int health = 100;
    public Inventory inventory;
    private GameObject holdableItem;
    public GameObject healthBar;
    public SceneScript sceneScript;
    public bool tookDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sceneScript = GameObject.Find("SceneManager").GetComponent<SceneScript>();
        tookDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 100)
        {
            if (tookDamage == true)
            {
                CancelInvoke("healthRegen");
                StartCoroutine(waitForRegen());
            }
            else
            {
                if(!IsInvoking("healthRegen") && !IsInvoking("StormDamage"))
                {
                    InvokeRepeating("healthRegen", 0, .2f);
                }
            }
        }

        if(health == 100)
        {
            CancelInvoke("healthRegen");
        }


        Vector2 scale = healthBar.transform.localScale;
        float barscale = health / 100.00f;
        scale.x = barscale;
        if (scale.x < 0)
        {
            healthBar.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            healthBar.transform.localScale = scale;
        }

        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //point character to mouse
        pointToMouse();
        //get players speed and direction
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        if (health == 0 || health < 0)
        {
            sceneScript.GameOver(false);
            Destroy(gameObject);
        }

        if(holdableText.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.AddItem(holdableItem);
                holdableItem.transform.position = new Vector2(1000f, 1000f);
            }
        }

    }

    void FixedUpdate()
    {
        //move player
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void pointToMouse()
    {
        //point character to mouse
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Holdable")
        {
            holdableText.text = "Press 'e' to Pickup " + collision.gameObject.name.ToString();
            holdableText.enabled = true;
            holdableItem = collision.gameObject;
        }
        if (collision.gameObject.name == "Circle")
        {
            CancelInvoke("StormDamage");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(holdableText.enabled == true)
        {
            holdableText.enabled = false;
        }
        if (collision.gameObject.name == "Circle")
        {
            InvokeRepeating("StormDamage", 0, 0.2f);
            CancelInvoke("healthRegen");
        }
    }
    private void StormDamage()
    {
        health -= 1;
    }

    private void healthRegen()
    {
        if(health < 100 )
        {
            health += 1;
        }
    }

    IEnumerator waitForRegen()
    {
        yield return new WaitForSeconds(3f);
        tookDamage = false;
    }

}
