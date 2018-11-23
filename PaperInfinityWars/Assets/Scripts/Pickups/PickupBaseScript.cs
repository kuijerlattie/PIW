using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBaseScript : MonoBehaviour {
    
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(5, -5), Random.Range(1, 4));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
}
