using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public Text backButton;
    public GameObject pauseScreen;

    // Use this for initialization
    void Start () {
		if (GameManager.instance.gameMode != null)
        {
            backButton.text = "End match";
        }
        else
        {
            backButton.text = "Main menu";
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
	}

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void OnBackButton()
    {
        UnPause();
        if (GameManager.instance.gameMode != null)
        {
            Debug.Log("end game");
            GameManager.instance.eventManager.EndGame.Invoke();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
