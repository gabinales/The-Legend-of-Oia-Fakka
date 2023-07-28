using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvarChaoSeguro : MonoBehaviour
{
    [SerializeField] private float saveFrequency = 3f;

    public Vector2 SafeGroundLocation {get; private set; } = new Vector2(0f,0f);

    private Coroutine safeGroundCoroutine;

    private TestarSolo groundCheck;

    private void Start(){
        safeGroundCoroutine = StartCoroutine(SaveGroundLocation());

        // Inicializa uma posição segura inicial
        SafeGroundLocation = transform.position;

        groundCheck = GetComponent<TestarSolo>();
    }

    private IEnumerator SaveGroundLocation(){
        // Atualiza o timer
        float elapsedTime = 0f;
        while(elapsedTime < saveFrequency){
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Se o jogador está tocando o chão, atualiza a posição de SafeGroundLocation
        if(groundCheck.IsGrounded()){
            SafeGroundLocation = transform.position;
        }

        // Recomeça a coroutine
        safeGroundCoroutine = StartCoroutine(SaveGroundLocation());
    }

    public void WarpPlayerToSafeGround(){
        transform.position = SafeGroundLocation;
    }
}
