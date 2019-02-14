using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : PickupBaseScript {

    public int healingValue = 1;

    protected override void OnPickup()
    {
        GameManager.instance.player.Heal(healingValue);
    }
}
