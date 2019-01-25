using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript : PickupBaseScript {

    public int minCoinValue = 1;
    public int maxCoinValue = 5;
    
    protected override void OnPickup()
    {
        GameManager.instance.currencyManager.AddCoins(Random.Range(minCoinValue, maxCoinValue));
    }
}
