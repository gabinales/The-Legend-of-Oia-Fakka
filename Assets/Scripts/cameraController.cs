using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;

    //Para suavização da transição:
    private Vector3 posicaoProxTela;
    public float velocidadeDaTransicao = 2f;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(instance); // Garante que só exista uma instância.
        }
    }

    //Update() é necessário para verificar se a câmera ainda está em transição.
    void Update(){
        if(transform.position != posicaoProxTela){
            transform.position = Vector3.Lerp(transform.position, posicaoProxTela, velocidadeDaTransicao * Time.deltaTime); // Interpolação.
        }
    }

    public void SetPosition(Vector2 position){
        //Sem Lerp: transform.position = new Vector3(position.x, position.y, transform.position.z); . Eixo z vai se manter sempre -10 (padrão da câmera)
        posicaoProxTela = new Vector3(position.x, position.y, transform.position.z);
    }
}
