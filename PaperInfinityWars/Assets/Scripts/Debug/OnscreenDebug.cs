﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnscreenDebug : MonoBehaviour {

    bool clickToKill = false;
    public Text clickToKillText;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (clickToKill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickToKill();
            }
        }
	}

    public void ToggleClickToKill()
    {
        clickToKill = !clickToKill;
        if (clickToKill)
            clickToKillText.text = "Clickdamage \n on";
        else
            clickToKillText.text = "Clickdamage \n off";

    }

    void ClickToKill()
    {
        RaycastHit2D raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        raycastHit = Physics2D.GetRayIntersection(ray, 10);
        if (raycastHit.collider != null)
        {
            Hitpoints victim = raycastHit.collider.GetComponent<Hitpoints>();
            if (victim != null)
            {
                victim.Damage(1);
            }
        }
    }

}