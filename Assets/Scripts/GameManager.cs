using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static int level = 1;
    public Text levelText;


    // Start is called before the first frame update
    void Start()
    {
        levelText = GameObject.Find("levelText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + level;
    }
}
