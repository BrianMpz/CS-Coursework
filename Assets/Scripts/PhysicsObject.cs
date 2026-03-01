using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private Transform objectTransform;
    [SerializeField] private float gravitationalConstant;

    [SerializeField] private protected PhysicsCollider topCollider;
    [SerializeField] private protected PhysicsCollider bottomCollider;
    [SerializeField] private protected PhysicsCollider leftCollider;
    [SerializeField] private protected PhysicsCollider rightCollider;

    [SerializeField] private protected GravityDirection currentGravityDirection;
    [SerializeField] private protected Vector2 currentVelocity;

    private void Start()
    {
        SetGravity(GravityDirection.Down);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying || PauseUI.Instance.IsPaused) return;

        HandleCollision();
        HandleGravity();
    }

    private void SetGravity(GravityDirection gravityDirection)
    {
        currentGravityDirection = gravityDirection;
    }

    private protected void InvertGravity()
    {
        // sets gravity direction to the oposite
        SetGravity(currentGravityDirection == GravityDirection.Up ? GravityDirection.Down : GravityDirection.Up);
    }

    private void HandleGravity()
    {
        bool isPressingCeiling = topCollider.IsCollidingWithPlatform() && currentVelocity.y > 0;
        bool isPressingFloor = bottomCollider.IsCollidingWithPlatform() && currentVelocity.y < 0;

        if (isPressingCeiling || isPressingFloor) // if touching ceiling and floor
        {
            ResetYVeloctiy();
        }
        else
        {
            ApplyForce(new(0, -gravitationalConstant));
            ApplyDisplacement(new(0, currentVelocity.y)); // apply gravity every frame
        }

        bool isPressingRight = rightCollider.IsCollidingWithPlatform() && currentVelocity.x > 0;
        bool isPressingLeft = leftCollider.IsCollidingWithPlatform() && currentVelocity.x < 0;

        if (isPressingRight || isPressingLeft) // if touching ceiling and floor
        {
            ResetXVeloctiy();
        }
        else
        {
            ApplyDisplacement(new(0, 0)); // apply gravity every frame
        }
    }

    private void HandleCollision()
    {
        float gravityCorrectionAmount = -currentVelocity.y; // the amount that we nudge the object by
        float obstacleCorrectionAmount = -ObstacleGenerator.Instance.scrollRate; // the amount that we nudge the object by

        Vector2 verticalCorrection = new(0, gravityCorrectionAmount); // convert to vector
        Vector2 horizontalCorrection = new(obstacleCorrectionAmount, 0); // convert to vector

        if (topCollider.IsCollidingWithPlatform()) // if pressing into the ceiling
        {
            ApplyDisplacement(verticalCorrection); // correct the overlap by moving the object down
            ResetYVeloctiy();
        }

        if (bottomCollider.IsCollidingWithPlatform()) // if pressing into the floor
        {
            ApplyDisplacement(verticalCorrection); // correct the overlap by moving the object up
            ResetYVeloctiy();
        }

        if (rightCollider.IsCollidingWithPlatform() && !leftCollider.IsCollidingWithPlatform()) // if pressing into the right
        {
            ApplyDisplacement(horizontalCorrection); // correct the overlap by moving the object left
            ResetXVeloctiy();
        }
    }

    private protected void ResetYVeloctiy()
    {
        currentVelocity = new(currentVelocity.x, 0); // halt vertical movement
    }

    private void ResetXVeloctiy()
    {
        currentVelocity = new(0, currentVelocity.y); // halt horizontal movement
    }

    private protected void ApplyForce(Vector2 force)
    {
        currentVelocity = new(0, currentVelocity.y + force.y * (int)currentGravityDirection); // take gravity every frame
    }

    private protected void ApplyDisplacement(Vector2 displacement)
    {
        Vector2 currentPosition = objectTransform.localPosition; // cache current postion

        float currentXPos = currentPosition.x; // cache currentXPos
        float currentYPos = currentPosition.y; // cache currentYPos

        float newXPos = currentXPos + (displacement.x * Time.fixedDeltaTime); // add horizontal displacement to the current postion
        float newYPos = currentYPos + (displacement.y * Time.fixedDeltaTime); // add vertical displacement to the current postion

        Vector2 newPosition = new(newXPos, newYPos); // calculate new postion
        objectTransform.localPosition = newPosition; // apply new position to object
    }

    public enum GravityDirection
    {
        Up = -1,
        Down = 1
    }
}
