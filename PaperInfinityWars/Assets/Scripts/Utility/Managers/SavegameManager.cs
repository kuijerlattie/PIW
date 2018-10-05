using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavegameManager : MonoBehaviour {

    private SaveData saveData = new SaveData();
	// Use this for initialization
	void Start () {
        Load();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        GameManager.instance.eventManager.OnSave.Invoke(saveData);
        StartCoroutine(SaveAfterPrepping());
    }

    public void Load()
    {
        //load stuff
        GameManager.instance.eventManager.OnLoad.Invoke(saveData);
    }

    private IEnumerator SaveAfterPrepping()
    {
        //wait for next frame to make sure all objects that need to fill in save info are done
        //actual save code here
        return null;
    }
}

public class SaveData
{
    int health = 0;
    int maxhealth = 0;
    int xp = 0;
}

