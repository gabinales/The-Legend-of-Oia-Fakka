using UnityEngine;

public class collisionDetection : MonoBehaviour
{
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with BoxCollider1
        if (collision.collider == boxCollider1)
        {
            // Handle collision with BoxCollider1
            Debug.Log("Collided with BoxCollider1");
        }

        // Check if the collision is with BoxCollider2
        if (collision.collider == boxCollider2)
        {
            // Handle collision with BoxCollider2
            Debug.Log("Collided with BoxCollider2");
        }
    }
}