using UnityEngine;

// handles the physics for a single object
public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private Transform objectTransform;
    [SerializeField] private float gravitationalConstant;

    // all of the physics colliders
    [SerializeField] private protected PhysicsCollider topCollider;
    [SerializeField] private protected PhysicsCollider bottomCollider;
    [SerializeField] private protected PhysicsCollider leftCollider;
    [SerializeField] private protected PhysicsCollider rightCollider;

    private protected GravityDirection currentGravityDirection; // current direction of gravity
    private protected Vector2 currentVelocity;

    private void Start()
    {
        SetGravity(GravityDirection.Down); // initialise gravity to be downward
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying || PauseUI.Instance.IsPaused) return;

        HandleCollision();
        HandleGravity();
    }

    private void SetGravity(GravityDirection _gravityDirection)
    {
        currentGravityDirection = _gravityDirection;
    }

    private protected void InvertGravity()
    {
        // sets gravity direction to the oposite
        SetGravity(currentGravityDirection == GravityDirection.Up ? GravityDirection.Down : GravityDirection.Up);
    }

    private void HandleGravity()
    {
        bool _isPressingCeiling = topCollider.IsCollidingWithPlatform() && currentVelocity.y > 0;
        bool _isPressingFloor = bottomCollider.IsCollidingWithPlatform() && currentVelocity.y < 0;

        if (_isPressingCeiling || _isPressingFloor) // if touching ceiling and floor
        {
            ResetYVeloctiy();
        }
        else
        {
            ApplyForce(new(0, -gravitationalConstant));
            ApplyDisplacement(new(0, currentVelocity.y)); // apply gravity every frame
        }

        bool _isPressingRight = rightCollider.IsCollidingWithPlatform() && currentVelocity.x > 0;
        bool _isPressingLeft = leftCollider.IsCollidingWithPlatform() && currentVelocity.x < 0;

        if (_isPressingRight || _isPressingLeft) // if touching ceiling and floor
        {
            ResetXVeloctiy();
        }
        else
        {
            ApplyDisplacement(new(0, 0));
        }
    }

    private void HandleCollision()
    {
        float _gravityCorrectionAmount = -currentVelocity.y; // the amount that we nudge the object by
        float _obstacleCorrectionAmount = -ObstacleGenerator.Instance.scrollRate; // the amount that we nudge the object by

        Vector2 _verticalCorrection = new(0, _gravityCorrectionAmount); // convert to vector
        Vector2 _horizontalCorrection = new(_obstacleCorrectionAmount, 0); // convert to vector

        if (topCollider.IsCollidingWithPlatform()) // if pressing into the ceiling
        {
            ApplyDisplacement(_verticalCorrection); // correct the overlap by moving the object down
            ResetYVeloctiy();
        }

        if (bottomCollider.IsCollidingWithPlatform()) // if pressing into the floor
        {
            ApplyDisplacement(_verticalCorrection); // correct the overlap by moving the object up
            ResetYVeloctiy();
        }

        if (rightCollider.IsCollidingWithPlatform() && !leftCollider.IsCollidingWithPlatform()) // if pressing into the right
        {
            ApplyDisplacement(_horizontalCorrection); // correct the overlap by moving the object left
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

    private protected void ApplyForce(Vector2 _force)
    {
        currentVelocity = new(0, currentVelocity.y + _force.y * (int)currentGravityDirection); // take gravity every frame
    }

    private protected void ApplyDisplacement(Vector2 _displacement)
    {
        Vector2 _currentPosition = objectTransform.localPosition; // cache current postion

        float _currentXPos = _currentPosition.x; // cache currentXPos
        float _currentYPos = _currentPosition.y; // cache currentYPos

        float _newXPos = _currentXPos + (_displacement.x * Time.fixedDeltaTime); // add horizontal displacement to the current postion
        float _newYPos = _currentYPos + (_displacement.y * Time.fixedDeltaTime); // add vertical displacement to the current postion

        Vector2 _newPosition = new(_newXPos, _newYPos); // calculate new postion
        objectTransform.localPosition = _newPosition; // apply new position to object
    }

    public enum GravityDirection
    {
        Up = -1,
        Down = 1
    }
}
