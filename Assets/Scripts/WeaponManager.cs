using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int ammo;
    public GameObject bulletPrefab;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        if(inventory.currentWeapon == "pistol")
        {
            ammo = 12;
        }
        else if (inventory.currentWeapon == "machinegun")
        {
            ammo = 30;
        }
        else if (inventory.currentWeapon == "sniper")
        {
            ammo = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(ammo == 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
                Vector2 bulletPosition;
                bulletPosition.x = transform.position.x + 0.35f;
                bulletPosition.y = transform.position.y + 0.7f;
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
                ammo--;
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(5);
        if (inventory.currentWeapon == "pistol")
        {
            ammo = 12;
        }
        else if (inventory.currentWeapon == "machinegun")
        {
            ammo = 30;
        }
        else if (inventory.currentWeapon == "sniper")
        {
            ammo = 5;
        }
    }
}
