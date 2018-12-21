using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChallengeHoverScript : MonoBehaviour, IPointerEnterHandler
{
    int challengeID;
    ChallengesScript owner;

    public void SetChallengeID(int i)
    {
        challengeID = i;
    }

    public void SetOwner(ChallengesScript oOwner)
    {
        owner = oOwner;
    }

    public void OnPointerEnter(PointerEventData pointereventdata)
    {
        owner.ShowChallengeWithID(challengeID);
    }
}
