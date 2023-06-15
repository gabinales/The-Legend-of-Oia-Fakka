using UnityEngine;
using static Comandos;

public class playerBehavior : MonoBehaviour
{
    public int hitPoints = 2;
    private Animator animator;
    private const float StepSize = 1f;

    // rotacoes no eixo Z
    private float up = 90;
    private float left = 180;
    private float down = 270;
    private float right = 0;
    private Vector3 Right = new Vector3(0f, 0f, 0f);
    private Vector3 Left = new Vector3(0f, 0f, 180f);
    private Vector3 Down = new Vector3(0f, 0f, 270f);
    private Vector3 Up = new Vector3(0f, 0f, 90f);

    void Awake() { animator = GetComponent<Animator>(); }

    void Update()
    {

        //ataque
        if (Input.GetMouseButtonDown(0))
        {
            //animator.SetTrigger("Atacando");
            ataca();
        }

        //movimento
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (transform.eulerAngles == Left)
                moveHorizontal = -1f;
            else
                rotaciona(left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (transform.eulerAngles == Right)
                moveHorizontal = 1f;
            else
                rotaciona(right);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (transform.eulerAngles == Down)
            {
                moveVertical = -1f;
            }
            else
                rotaciona(down);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            if (transform.eulerAngles == Up)
                moveVertical = 1f;
            else
                rotaciona(up);

        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0f) * StepSize;
        Collider2D collider = Physics2D.OverlapCircle(newPosition, 0.2f);
        if (collider != null) return;

        if (!(newPosition.x < 0f || newPosition.x > 6f ||
             newPosition.y < 0f || newPosition.y > 6f))
        {
            transform.position = newPosition;
        }

    }
    void rotaciona(float orientacao)
    {
        transform.eulerAngles = new Vector3(0f, 0f, orientacao);
    }

    public void ataca()
    {
        Vector3 angulo = transform.eulerAngles;
        float atacaHorizontal = 0f;
        float atacaVertical = 0f;

        if (angulo == Left)
        {
            atacaHorizontal = -1;
        }
        if (angulo == Right)
        {
            atacaHorizontal = 1;
        }
        if (angulo == Up)
        {
            atacaVertical = 1;
        }
        if (angulo == Down)
        {
            atacaVertical = -1;
        }
        Vector3 posicaoDoAtacado = transform.position + new Vector3(atacaHorizontal, atacaVertical, 0f);
        Debug.Log("atacando em " + posicaoDoAtacado);
        Collider2D collider = Physics2D.OverlapCircle(posicaoDoAtacado, 0.2f);

        if (collider != null && collider.CompareTag("enemy"))
        {
            Debug.Log("Enemy found at position: " + posicaoDoAtacado);
            gameScript.damage("sabreDeLuz", collider.gameObject);
        }
    }
}