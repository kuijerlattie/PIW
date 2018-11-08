using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float _jumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool _airControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask _groundMasks;							// A mask determining what is ground to the character
	[SerializeField] private Transform _groundCheckPosition;				    // A position marking where to check if the player is grounded.

	const float _maxGroundedDistance = .1f; // lenght of the raycast for checking ground
	private bool _grounded;            // Whether or not the player is grounded.
	private Rigidbody2D _rigidbody;
	private bool _facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 _velocity = Vector3.zero;
    [HideInInspector]
    public bool _forwardFree = true; // For determining if forward movement is possible or not.

    new public bool enabled = true;
    private GameObject _lastTrigger;

    private Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

	private void FixedUpdate()
	{
		bool wasGrounded = _grounded;
		_grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
        RaycastHit2D hit = Physics2D.Raycast(_groundCheckPosition.position, -Vector2.up, _maxGroundedDistance, _groundMasks);
        if (hit.collider != null)
        {
            //Debug.Log("should be grounded " + hit.collider.name + " with distance " + hit.distance);
            _grounded = true;
            //if (!wasGrounded)
                //OnLandEvent.Invoke();
        }

        if (_lastTrigger == null)
        {
            _forwardFree = true;
        }
        if (_animator != null)
            UpdateAnimator();
	}


	public void Move(float move, bool jump)
	{
        //only control anything when enabled. disable when dead/in cutscene
        if (enabled)
        {
            //only control the player if grounded or airControl is turned on
            if (_grounded || _airControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2((_forwardFree ? move : 0) * 10f, _rigidbody.velocity.y);
                // And then smoothing it out and applying it to the character
                _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !_facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && _facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (_grounded && jump)
            {
                Debug.Log("jumped");
                // Add a vertical force to the player.
                _grounded = false;
                _rigidbody.AddForce(new Vector2(0f, _jumpForce));
                if (_animator != null)
                    _animator.SetBool("Jump", true);
            }
        }
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != this.gameObject && 
            collision.gameObject.layer != 10 &&
            collision.isTrigger == false) //stop trigger on trigger detection
        _forwardFree = false;
        _lastTrigger = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _forwardFree = true;
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("HorizontalSpeed", Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetFloat("VerticalSpeed", _rigidbody.velocity.y);
    }
}
