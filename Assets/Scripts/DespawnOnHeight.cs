using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DespawnOnHeight : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //check if player has fallen in a hole
        if (transform.position.y <= -1)
        {
            Destroy(GameObject.Find("WhisperSource"));
            SceneManager.LoadScene("GameOver");
        }
    }
}
