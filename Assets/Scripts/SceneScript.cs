using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{

    public bool isLoadingNextScene = false;
    public GameObject PauseMenu;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void GameOver(bool didPlayerWin)
    {
        StartCoroutine(WaitForLoadScene(didPlayerWin));
    }

    private IEnumerator WaitForLoadScene(bool didPlayerWin)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
            yield return null;
        yield return new WaitForEndOfFrame();
        GameObject canvas = GameObject.Find("Canvas");
        GameObject winText = canvas.transform.GetChild(2).gameObject;
        GameObject lossText = canvas.transform.GetChild(1).gameObject;
        if (didPlayerWin == true)
        {
            lossText.SetActive(false);
        }
        else
        {
            winText.SetActive(false);
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Play" && isLoadingNextScene == false)
        {
            if(GameObject.Find("EnemyAI_MachineGun(Clone)") == null && GameObject.Find("EnemyAI_Pistol(Clone)") == null && GameObject.Find("EnemyAI_Sniper(Clone)") == null)
            {
                GameOver(true);
                isLoadingNextScene = true;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Pause();
            }
        }
    }
}
