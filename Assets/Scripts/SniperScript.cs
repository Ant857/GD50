using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
    private int ammo;
    public GameObject bulletPrefab;
    private Inventory inventory;
    private GameObject Player;
    private GameObject bulletAnchor;
    private bool isFireAllowed;
    public AudioSource shootAudioSource;
    public AudioSource reloadAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        bulletAnchor = Player.transform.GetChild(0).gameObject;
        inventory = Player.GetComponent<Inventory>();
        ammo = 5;
        isFireAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.currentWeapon == "sniper")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }

            if (Input.GetMouseButtonDown(0) && isFireAllowed == true)
            {
                if (isFireAllowed == true)
                {
                    StartCoroutine(Fire());
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isFireAllowed = false;
        reloadAudioSource.Play();
        yield return new WaitForSeconds(2f);
        ammo = 5;
        isFireAllowed = true;
    }
    IEnumerator Fire()
    {
        isFireAllowed = false;
        Vector2 bulletPosition = bulletAnchor.transform.position;
        GameObject proj = Instantiate(bulletPrefab, bulletPosition, Player.transform.rotation);
        proj.GetComponent<bullet>().damage = 50;
        shootAudioSource.Play();
        ammo--;
        if (ammo == 0)
        {
            StartCoroutine(Reload());
            yield break;
        }
        yield return new WaitForSeconds(1f);
        isFireAllowed = true;
    }
}
