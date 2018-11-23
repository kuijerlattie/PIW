using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript : PickupBaseScript {

    public int coinvalue = 1;
    
    protected override void OnPickup()
    {
        GameManager.instance.player.AddCoins(coinvalue);
    }
}
