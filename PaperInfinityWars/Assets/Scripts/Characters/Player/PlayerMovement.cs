using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    [SerializeField]
    private Animator animator;

    float horizontalMove = 0f;
    bool jump = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (animator != null)
            SetAnimationParameters();
	}

    private void FixedUpdate()
    {
        controller.Move(horizontalMove, jump);
        if (animator != null)
            animator.SetBool("Jump", jump);
        jump = false;
    }

    private void SetAnimationParameters()
    {
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(controller.GetVelocity().x));
        animator.SetFloat("VerticalSpeed", controller.GetVelocity().y);
    }
}
