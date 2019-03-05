using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Canvas canvasMainScreen;
    public Canvas canvasChallenges;
    public Canvas canvasMedals;
    public Canvas canvasStats;
    public Canvas canvasSettings;
    public Canvas canvasQuit;

    public Button buttonChallenges;
    public Button buttonMedals;
    public Button buttonStats;

    public string FirstLevel = "FantasyHub";
    [FMODUnity.EventRef]
    public string mainMenuMusicPath = "";

    FMOD.Studio.EventInstance mainMenuMusicEvent;


	// Use this for initialization
	void Start () {
        ShowMainScreen();
        GameManager.instance.eventManager.OnLoad.AddListener(OnLoad);
        mainMenuMusicEvent = FMODUnity.RuntimeManager.CreateInstance(mainMenuMusicPath);
        mainMenuMusicEvent.start();
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.OnLoad.RemoveListener(OnLoad);
        mainMenuMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void OnLoad(SaveData saveData)
    {
        if (GameManager.instance.savegameManager.SaveExists)
        {
            buttonChallenges.enabled = true;
            buttonMedals.enabled = true;
            buttonStats.enabled = true;
        }
        else
        {
            buttonChallenges.enabled = false;
            buttonMedals.enabled = false;
            buttonStats.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMainScreen()
    {
        canvasMainScreen.gameObject.SetActive(true);
        canvasChallenges.gameObject.SetActive(false);
        canvasMedals.gameObject.SetActive(false);
        canvasStats.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(false);
        canvasQuit.gameObject.SetActive(false);
    }

    public void ShowChallenges()
    {
        canvasMainScreen.gameObject.SetActive(false);
        canvasChallenges.gameObject.SetActive(true);
        canvasMedals.gameObject.SetActive(false);
        canvasStats.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(false);
        canvasQuit.gameObject.SetActive(false);
    }

    public void ShowMedals()
    {
        canvasMainScreen.gameObject.SetActive(false);
        canvasChallenges.gameObject.SetActive(false);
        canvasMedals.gameObject.SetActive(true);
        canvasStats.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(false);
        canvasQuit.gameObject.SetActive(false);
    }

    public void ShowStats()
    {
        canvasMainScreen.gameObject.SetActive(false);
        canvasChallenges.gameObject.SetActive(false);
        canvasMedals.gameObject.SetActive(false);
        canvasStats.gameObject.SetActive(true);
        canvasSettings.gameObject.SetActive(false);
        canvasQuit.gameObject.SetActive(false);
    }

    public void ShowSettings()
    {
        canvasMainScreen.gameObject.SetActive(false);
        canvasChallenges.gameObject.SetActive(false);
        canvasMedals.gameObject.SetActive(false);
        canvasStats.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(true);
        canvasQuit.gameObject.SetActive(false);
    }

    public void ShowQuit()
    {
        canvasMainScreen.gameObject.SetActive(false);
        canvasChallenges.gameObject.SetActive(false);
        canvasMedals.gameObject.SetActive(false);
        canvasStats.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(false);
        canvasQuit.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        if (GameManager.instance.savegameManager.SaveExists && GameManager.instance.savegameManager.saveData.lastHub != "")
        {
            SceneManager.LoadScene(GameManager.instance.savegameManager.saveData.lastHub);
        }
        else
        {
            SceneManager.LoadScene(FirstLevel);
        }
    }
}
