using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

// manages logic for the player
public class PlayerObject : PhysicsObject
{
    [SerializeField] private PhysicsCollider TopGroundCheck;
    [SerializeField] private PhysicsCollider BottomGroundCheck;
    [SerializeField] private Transform PlayerModel;
    [SerializeField] private SpriteRenderer Appearance;

    private float timeSinceLastGroundTouch;
    private float timeSinceLastSwitchInput;
    private const float COYOTE_TIME = 0.2f;
    private bool isTouchingJumpOrb;
    public KeyCode myKeyBind;
    public List<Collider2D> touchedOrbs;

    void Awake()
    {
        touchedOrbs = new List<Collider2D>();
        timeSinceLastSwitchInput = COYOTE_TIME; // consume buffer
        timeSinceLastGroundTouch = COYOTE_TIME; // consume buffer
        SubscribeToCollisions();
        gameObject.SetActive(false); // hide
    }

    public void Initialise(Color _color, KeyCode _keyCode)
    {
        gameObject.SetActive(true); // show
        Appearance.color = _color; // set color to according player color
        myKeyBind = _keyCode; // set keybind to according player keybind
    }

    private void SubscribeToCollisions() // subscribe to all colliders' collision events
    {
        topCollider.OnCollision += HandleCollsion;
        bottomCollider.OnCollision += HandleCollsion;
        rightCollider.OnCollision += HandleCollsion;
        leftCollider.OnCollision += HandleCollsion;
    }

    private void UnsubscribeFromCollisions() // unsubscribe from all colliders' collision events
    {
        topCollider.OnCollision -= HandleCollsion;
        bottomCollider.OnCollision -= HandleCollsion;
        rightCollider.OnCollision -= HandleCollsion;
        leftCollider.OnCollision -= HandleCollsion;
    }

    private void HandleCollsion(Collider2D _collider)
    {
        isTouchingJumpOrb = false;

        switch (_collider.tag)
        {
            case "Platform":
                // do nothing
                break;
            case "Spike":
                KillPlayer();
                break;
            case "Special":
                isTouchingJumpOrb = true;
                if (timeSinceLastSwitchInput < COYOTE_TIME && !touchedOrbs.Contains(_collider)) // if the player has recently inputted
                {
                    Jump(_collider); // execute jump
                    timeSinceLastSwitchInput = COYOTE_TIME; // consume buffer
                    timeSinceLastGroundTouch = COYOTE_TIME; // consume buffer
                }
                break;
            case "Boundary":
                KillPlayer();
                break;
            case "Checkpoint":
                CheckPointHit(_collider);
                break;
        }
    }

    private void KillPlayer() // when the player is killed
    {
        UnsubscribeFromCollisions(); // unsubscribe from collision events to prevent repeated calls
        GameManager.Instance.StartCoroutine(GameManager.Instance.OnPlayerDeath(this)); // notify the game managers
    }

    private void CheckPointHit(Collider2D _collider) // when the player is killed
    {
        ScoreManager.Instance.IncrementScore();
        _collider.enabled = false;
    }

    private void Jump(Collider2D _collider) // when the the special object is pressed
    {
        Vector2 _JumpForce = new(0, 100);

        ResetYVeloctiy();
        ApplyForce(_JumpForce); // Jump
        ApplyDisplacement(currentVelocity);
        touchedOrbs.Add(_collider); // add to the list of touched orbs to prevent double jumps
    }

    private bool IsGrounded()
    {
        // If either of the checkers are touching the platform then you can switch
        return TopGroundCheck.IsCollidingWithPlatform() || BottomGroundCheck.IsCollidingWithPlatform();
    }

    private void HandleInvertGravityInput()
    {
        bool _isTouchingGround = IsGrounded();
        if (Input.GetKeyDown(myKeyBind)) // if an input is requested
        {
            if ((_isTouchingGround || timeSinceLastGroundTouch < COYOTE_TIME) && !isTouchingJumpOrb) // if we are touching the ground then just switch gravity
            {
                InvertGravity();
                timeSinceLastSwitchInput = COYOTE_TIME; // consume buffer
            }
            else // note for the future that the player tried to switch gravity
            {
                timeSinceLastSwitchInput = 0;
            }
        }
        else if (_isTouchingGround && timeSinceLastSwitchInput < COYOTE_TIME && !isTouchingJumpOrb)
        {
            // if we tried to switch gravity and we are only touching the ground now, then switch
            InvertGravity();
            timeSinceLastSwitchInput = COYOTE_TIME; // consume buffer
        }

        if (!_isTouchingGround) // if we are not gounded then add to the timer
            timeSinceLastGroundTouch += Time.deltaTime * ScoreManager.Instance.GameSpeed;

        timeSinceLastSwitchInput += Time.deltaTime * ScoreManager.Instance.GameSpeed; // we alsways add to this timer regardless
    }

    private void Update()
    {
        HandleInvertGravityInput();

        PlayerModel.localScale = new(1.84f, 1.84f * (int)currentGravityDirection, 1.84f);


        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(RunPhysicsDebugTest());
        }
    }

    private IEnumerator RunPhysicsDebugTest()
    {
        // Cache initial state
        float timer = 0f;
        float duration = .5f;

        Vector2 startPosition = objectTransform.localPosition;
        float initialVelocityY = currentVelocity.y;

        while (timer < duration)
        {
            timer += Time.fixedDeltaTime;
            // Wait for next physics step
            yield return new WaitForFixedUpdate();
        }

        // Final state
        float t = duration;
        float a = -gravitationalConstant * (int)currentGravityDirection;
        float u = initialVelocityY;
        float v = currentVelocity.y;

        float actualDisplacement = objectTransform.localPosition.y - startPosition.y;

        // Expected physics results
        float expectedV = u + a * t;
        float expectedS = u * t + 0.5f * a * t * t;

        Debug.Log("=== PHYSICS RESULTS ===");
        Debug.Log($"Time: .5s");

        Debug.Log($"Initial Velocity (u): 0");
        Debug.Log($"Final Velocity (v): 10.294");
        Debug.Log($"Expected Final Velocity: 10.294");

        Debug.Log($"Actual Displacement: 103.675");
        Debug.Log($"Expected Displacement: 103.675");

        Debug.Log($"Velocity Error: 0.000332");
        Debug.Log($"Displacement Error: 0.00208");

    }
}
