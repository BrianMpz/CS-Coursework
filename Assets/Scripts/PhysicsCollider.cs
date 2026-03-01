using System;
using UnityEngine;

public class PhysicsCollider : MonoBehaviour
{
    [SerializeField] private new Collider2D collider2D;

    public event Action<Collider2D> OnCollision;

    public bool IsCollidingWithPlatform()
    {
        Collider2D[] hits = GetCollidingObjects();

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Platform")) return true; // true if its a platform
        }

        return false;
    }

    private Collider2D[] GetCollidingObjects()
    {
        return Physics2D.OverlapBoxAll(collider2D.bounds.center, collider2D.bounds.size, 0f);
        // get an array of objects this im overlapping with
    }

    private void FixedUpdate()
    {
        CheckForOtherCollisions();
    }

    private void CheckForOtherCollisions()
    {
        Collider2D[] others = GetCollidingObjects();

        foreach (Collider2D other in others)
        {
            if (other.CompareTag("ignore")) continue; // ignore this object

            OnCollision?.Invoke(other);
        }
    }
}
