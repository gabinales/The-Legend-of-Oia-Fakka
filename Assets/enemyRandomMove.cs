using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRandomMove : MonoBehaviour
{
    public float maxXOffset = 5f; // Maximum offset on the X-axis
    public float maxYOffset = 5f; // Maximum offset on the Y-axis
    public float repeatInterval = 3f; // Time interval between repetitions
    public float movementDuration = 1f; // Duration of the movement

    private Vector3 initialPosition; // Initial position of the object
    private Vector3 targetPosition; // Target position for interpolation
    private float movementTimer; // Timer for tracking the movement duration

    private void Start()
    {
        initialPosition = transform.position; // Store the initial position
        StartCoroutine(RepeatMovement());
    }

    private IEnumerator RepeatMovement()
    {
        while (true)
        {
            // Generate random offsets for X and Y positions
            float randomXOffset = Random.Range(-maxXOffset, maxXOffset);
            float randomYOffset = Random.Range(-maxYOffset, maxYOffset);

            // Calculate the target position
            targetPosition = initialPosition + new Vector3(randomXOffset, randomYOffset, 0f);

            // Reset the movement timer
            movementTimer = 0f;

            while (movementTimer < movementDuration)
            {
                movementTimer += Time.deltaTime;

                // Calculate the interpolation factor
                float t = movementTimer / movementDuration;

                // Smoothly move towards the target position
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

                yield return null;
            }

            // Update the initial position for the next iteration
            initialPosition = transform.position;

            yield return new WaitForSeconds(repeatInterval);
        }
    }
}
