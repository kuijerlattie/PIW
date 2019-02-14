using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIScript : MonoBehaviour {

    public GameObject UI;
    public GameObject interactIcon;
    bool UIActive = false;
    bool inRange = false;

	// Use this for initialization
	void Start () {
        UI.SetActive(false);
        interactIcon.SetActive(false);
        UIActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            Debug.Log("f got pressed");
            UI.SetActive(!UI.activeInHierarchy);
            interactIcon.SetActive(!UI.activeInHierarchy);
            UIActive = UI.activeInHierarchy;
            GameManager.instance.player.GetComponent<CharacterController2D>().enabled = !UIActive;
            if (UIActive)
                GameManager.instance.eventManager.ShowCoins.Invoke(9999f);
            else
                GameManager.instance.eventManager.HideCoins.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactIcon.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(false);
            interactIcon.SetActive(false);
            UIActive = false;
            inRange = false;
            GameManager.instance.player.GetComponent<CharacterController2D>().enabled = true;
        }
    }
}
