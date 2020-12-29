using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;
    public bool[] slotTaken;
    private int currentSlot = 0;
    public Sprite[] characters;
    public GameObject[] weapons;
    public string currentWeapon;

    void Start()
    {
        GameObject inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
        inventorySlot.GetComponent<Outline>().enabled = true;

        //number values for weapon names must match order of characters in array
        if(slots[currentSlot].enabled == true)
        {
            if (slots[currentSlot].sprite.name == "pistol")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[1];
                currentWeapon = slots[currentSlot].sprite.name;
            }
            else if (slots[currentSlot].sprite.name == "machinegun")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[2];
                currentWeapon = slots[currentSlot].sprite.name;
            }
            else if (slots[currentSlot].sprite.name == "sniper")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[3];
                currentWeapon = slots[currentSlot].sprite.name;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = characters[0];
        }
        int i = 0;
        foreach (Image slot in slots)
        {
            slotTaken[i] = true;
            i++;
        }
    }

    public void AddItem(GameObject item)
    {
        int i = 0;
        foreach (Image slot in slots)
        {
            if (slotTaken[i] != true)
            {
                slot.sprite = item.GetComponent<SpriteRenderer>().sprite;
                slot.enabled = true;
                print(slotTaken[i]);
                slotTaken[i] = true;
                i++;
                break;
            }
            i++;
        }
        //number values for weapon names must match order of characters in array
        if (slots[currentSlot].enabled == true)
        {
            if (slots[currentSlot].sprite.name == "pistol")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[1];
                currentWeapon = slots[currentSlot].sprite.name;
            }
            else if (slots[currentSlot].sprite.name == "machinegun")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[2];
                currentWeapon = slots[currentSlot].sprite.name;
            }
            else if (slots[currentSlot].sprite.name == "sniper")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[3];
                currentWeapon = slots[currentSlot].sprite.name;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = characters[0];
        }
    }
    public void RemoveItem(int index)
    {
        //number values for weapon names must match order of weapons in array
        slots[index].enabled = false;
        slotTaken[index] = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = characters[0];

        if (slots[index].sprite.name == "pistol")
        {
            GameObject pistol = Instantiate(weapons[0], gameObject.transform.position, Quaternion.identity);
            pistol.name = "pistol";
            Destroy(GameObject.Find("pistol"));
        }
        else if (slots[index].sprite.name == "machinegun")
        {
            GameObject machinegun = Instantiate(weapons[1], gameObject.transform.position, Quaternion.identity);
            machinegun.name = "machinegun";
            Destroy(GameObject.Find("machinegun"));
        }
        else if (slots[index].sprite.name == "sniper")
        {
            GameObject sniper = Instantiate(weapons[2], gameObject.transform.position, Quaternion.identity);
            sniper.name = "sniper";
            Destroy(GameObject.Find("sniper"));
        }
    }

    void Update()
    {
        if (slots[currentSlot].enabled == false)
        {
            currentWeapon = "none";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentSlot != 0)
            {
                GameObject inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
                inventorySlot.GetComponent<Outline>().enabled = false;
                currentSlot = 0;
                inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
                inventorySlot.GetComponent<Outline>().enabled = true;
                if (slots[currentSlot].enabled == true)
                {
                    currentWeapon = slots[currentSlot].sprite.name;
                    if (slots[currentSlot].sprite.name == "pistol")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[1];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                    else if (slots[currentSlot].sprite.name == "machinegun")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[2];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                    else if (slots[currentSlot].sprite.name == "sniper")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[3];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentSlot != 1)
            {
                GameObject inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
                inventorySlot.GetComponent<Outline>().enabled = false;
                currentSlot = 1;
                inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
                inventorySlot.GetComponent<Outline>().enabled = true;
                if (slots[currentSlot].enabled == true)
                {
                    currentWeapon = slots[currentSlot].sprite.name;
                    if (slots[currentSlot].sprite.name == "pistol")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[1];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                    else if (slots[currentSlot].sprite.name == "machinegun")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[2];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                    else if (slots[currentSlot].sprite.name == "sniper")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[3];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentSlot != 2)
            {
                GameObject inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
                inventorySlot.GetComponent<Outline>().enabled = false;
                currentSlot = 2;
                inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
                inventorySlot.GetComponent<Outline>().enabled = true;
                if (slots[currentSlot].enabled == true)
                {
                    currentWeapon = slots[currentSlot].sprite.name;
                    if (slots[currentSlot].sprite.name == "pistol")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[1];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                    else if (slots[currentSlot].sprite.name == "machinegun")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[2];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                    else if (slots[currentSlot].sprite.name == "sniper")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = characters[3];
                        currentWeapon = slots[currentSlot].sprite.name;
                    }
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            GameObject inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
            inventorySlot.GetComponent<Outline>().enabled = false;
            if (currentSlot < 2)
            {
                currentSlot++;
            }
            else
            {
                currentSlot = 0;
            }
            inventorySlot = slots[currentSlot].transform.parent.gameObject.transform.parent.gameObject;
            inventorySlot.GetComponent<Outline>().enabled = true;
            if(slots[currentSlot].enabled == true)
            {
                currentWeapon = slots[currentSlot].sprite.name;
            }

            //number values for weapon names must match order of characters in array
            if (slots[currentSlot].enabled == true)
            {
                if (slots[currentSlot].sprite.name == "pistol")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = characters[1];
                    currentWeapon = slots[currentSlot].sprite.name;
                }
                else if (slots[currentSlot].sprite.name == "machinegun")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = characters[2];
                    currentWeapon = slots[currentSlot].sprite.name;
                }
                else if (slots[currentSlot].sprite.name == "sniper")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = characters[3];
                    currentWeapon = slots[currentSlot].sprite.name;
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = characters[0];
            }
        }
    }
}
