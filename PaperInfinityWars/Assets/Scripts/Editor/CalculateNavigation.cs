using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CalculateNavigation {


    public static GameObject container;

    [MenuItem("Tools/CreateNaviGrid")]
    public static void CreateNavGrid()
    {
        container = new GameObject("navgrid");
        List<Collider2D> collider2Ds = new List<Collider2D>();
        collider2Ds.AddRange(GameObject.FindObjectsOfType<Collider2D>());
        collider2Ds.RemoveAll(X => X.gameObject.layer != 11);
        foreach (Collider2D c in collider2Ds)
        {
            CreateNodeGridOnCollider(c);
        }
    }

    static void CreateNodeGridOnCollider(Collider2D collider)
    {
        Vector2 topleft;
        Vector2 topright;
        topleft = collider.transform.position - (new Vector3(collider.bounds.size.x / 2, -collider.bounds.size.y, 0));
        topright = collider.transform.position + (collider.bounds.size / 2);
        Vector2 disposition = topright - topleft;
        float distancebetweennodes = Vector2.Distance(topleft, topright);
        float interval = distancebetweennodes / (Mathf.Round(distancebetweennodes));
        disposition.Normalize();
        disposition *= interval;

        for (int i = 1; i < Mathf.Round(distancebetweennodes); i++)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.SetPositionAndRotation(topleft + (disposition * i), Quaternion.identity);
            gameObject.transform.SetParent(container.transform);
            
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
