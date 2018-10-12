using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnscreenDebug : MonoBehaviour {

    bool clickToKill = false;
    public Text clickToKillText;
    public Text godmodeText;

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

    public void ToggleGodmode()
    {
        GameManager.instance.player.godmode = !GameManager.instance.player.godmode;
        if (GameManager.instance.player.godmode)
            godmodeText.text = "Godmode \n on";
        else
            godmodeText.text = "Godmode \n off";
    }

    void ClickToKill()
    {
        RaycastHit2D raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        raycastHit = Physics2D.GetRayIntersection(ray, 10);
        if (raycastHit.collider != null)
        {
            KillablePawn victim = raycastHit.collider.GetComponent<KillablePawn>();
            if (victim != null)
            {
                victim.Damage(1);
            }
        }
    }

}
