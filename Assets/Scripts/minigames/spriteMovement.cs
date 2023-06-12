using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    private const float StepSize = 1f;

    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            moveHorizontal = -1f;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            moveHorizontal = 1f;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            moveVertical = -1f;
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            moveVertical = 1f;

        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0f) * StepSize;
        transform.position = newPosition;
    }
}