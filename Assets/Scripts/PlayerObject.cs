using System;
using UnityEngine;

public class PlayerObject : PhysicsObject
{
    [SerializeField] private PhysicsCollider topGroundCheck;
    [SerializeField] private PhysicsCollider bottomGroundCheck;
    [SerializeField] private Transform playerModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SubscribeToCollisions();
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

    private void HandleCollsion(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Platform":
                // do nothing
                break;
            case "Spike":
                KillPlayer();
                break;
            case "Special":
                Jump(collider);
                break;
            case "Boundary":
                KillPlayer();
                break;
            case "Checkpoint":
                CheckPointHit(collider);
                break;
        }
    }

    private void KillPlayer() // when the player is killed
    {
        UnsubscribeFromCollisions(); // unsubscribe from collision events to prevent repeated calls
        GameManager.Instance.StartCoroutine(GameManager.Instance.OnPlayerDeath(this)); // notify the game managers
    }

    private void CheckPointHit(Collider2D collider) // when the player is killed
    {
        ScoreManager.Instance.IncrementScore();
        collider.enabled = false;
    }

    private void Jump(Collider2D collider) // when the the special object is pressed
    {
        Vector2 JumpForce = new(0, 70);

        ResetYVeloctiy();
        ApplyForce(JumpForce); // Jump
        ApplyDisplacement(currentVelocity);
        collider.enabled = false; // disable collider
    }

    private bool IsGrounded()
    {
        // If either of the checkers are touching the platform then you can switch
        return topGroundCheck.IsCollidingWithPlatform() || bottomGroundCheck.IsCollidingWithPlatform();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            InvertGravity();
        }

        playerModel.localScale = new(1, (int)currentGravityDirection, 1);
    }
}
