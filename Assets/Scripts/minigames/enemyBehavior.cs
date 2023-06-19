using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float delay = 0.9f;
    // Start is called before the first frame update
    public int hitPoints = 2;
    private Animator animator;
    public float StepSize = 1f;

    // rotacoes no eixo Z
    private float up = 90;
    private float left = 180;
    private float down = 270;
    private float right = 0;
    private Vector3 Right = new Vector3(0f, 0f, 0f);
    private Vector3 Left = new Vector3(0f, 0f, 180f);
    private Vector3 Down = new Vector3(0f, 0f, 270f);
    private Vector3 Up = new Vector3(0f, 0f, 90f);

    Vector3 playerPosition;
    Vector3 enemyPosition;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PerformActionsWithDelay());
    }

    //IA movimentacao e atk
    IEnumerator PerformActionsWithDelay()
    {
        while (true)
        {
            Vector3 direction = getPlayerDirection();
            float epsilon = 0.001f; // Small epsilon for float comparison

            //Se x = y (diagonal)...
            if (Mathf.Abs(direction.x - direction.y) < epsilon)
            {
                if (direction.x < 0)
                    move("left");
                else if (direction.x > 0)
                    move("right");
            }


            else if (direction.y < 0)
            {
                move("down");
            }
            else if (direction.y > 0)
            {
                move("up");
            }
            else if (direction.x < 0)
            {
                move("left");
            }
            else if (direction.x > 0)
            {
                move("right");
            }
            yield return new WaitForSeconds(delay);
        }
    }

    Vector3 getPlayerDirection()
    {
        playerPosition = GameObject.Find("Player(Clone)").transform.position;
        enemyPosition = transform.position;
        Vector3 direction = playerPosition - enemyPosition;
        //direction.Normalize();
        Debug.DrawRay(enemyPosition, direction, Color.red, 0.3f);
        // Debug.Log("direction.x = " + direction.x);
        // Debug.Log("direction.y = " + direction.y);
        return direction;
    }

    public void move(string direction)
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (direction == "left")
        {
            if (transform.eulerAngles == Left)
                moveHorizontal = -1f;
            else
                rotaciona(left);
        }
        if (direction == "right")
        {
            if (transform.eulerAngles == Right)
                moveHorizontal = 1f;
            else
                rotaciona(right);
        }
        if (direction == "down")
        {
            if (transform.eulerAngles == Down)
                moveVertical = -1f;
            else
                rotaciona(down);
        }
        if (direction == "up")
            if (transform.eulerAngles == Up)
                moveVertical = 1f;
            else
                rotaciona(up);

        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0f) * StepSize;
        if (newPosition != transform.position)
        {
            Collider2D collider = Physics2D.OverlapCircle(newPosition, 0.1f);
            if (collider == null)
            {
                if (!(newPosition.x < 0f || newPosition.x > 6f ||
                     newPosition.y < 0f || newPosition.y > 6f))
                {
                    transform.position = newPosition;
                }
            }
        }
    }

    void rotaciona(float orientacao)
    {
        transform.eulerAngles = new Vector3(0f, 0f, orientacao);
    }

    void OnTriggerEnter2D(Collider2D colisor){
        Debug.Log(colisor);
    }
        
    
}





