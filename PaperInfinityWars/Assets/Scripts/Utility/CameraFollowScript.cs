using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    [HideInInspector]
    public GameObject target;
    Vector2 displacement;
    Vector2 previouslocation;

    // Use this for initialization
    private void Start()
    {
        GameManager.instance.SetCameraFollowScript(this);
        this.transform.rotation = Quaternion.Euler(new Vector3(22.5f, 0, 0));
    }

    public void Initialize () {
        previouslocation = new Vector2(target.transform.position.x, target.transform.position.y);
        this.transform.position = new Vector3(previouslocation.x, previouslocation.y + 4, -7);
	}
	
	// Update is called once per frame
	void Update () {
        displacement = previouslocation - new Vector2(target.transform.position.x, target.transform.position.y);
        displacement *= -1;
        this.transform.position = new Vector3(this.transform.position.x + displacement.x, this.transform.position.y + displacement.y, this.transform.position.z);
        previouslocation = new Vector2(target.transform.position.x, target.transform.position.y);

	}
}
