using System.Collections.Generic;
using UnityEngine;
using TMPro;

class FloatingTextScript : MonoBehaviour
{
    public float DestroyTime = 1f;
    public TextMesh floatingText;
    private Vector3 gravity = new Vector3(0, -0.5f, 0);
    private Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 displacement = new Vector3(0, 2, 0);
    public bool hasVelocity = true;
    public bool hasGravity = true;
    public bool doesScale = true;
    public float scaleOverTime = 0.98f;


    public void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.position += displacement;
        velocity.x = Random.Range(1, 10);
        velocity.y = Random.Range(5, 10);
    }

    public void Update()
    {
        if (hasVelocity)
        {
            if (hasGravity)
            {
                velocity += gravity;
            }
            if (doesScale)
            transform.position += velocity * Time.deltaTime;
        }
    }

    public void SetText(string sText)
    {
        floatingText.text = sText;
    }
}
