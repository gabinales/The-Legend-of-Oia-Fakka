using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject caixaDeDialogo;
    [SerializeField] TextMeshProUGUI textoDialogo;

    [SerializeField] int letrasPorSegundo; // Velocidade com que o texto será exibido, letra por letra

    public event Action OnMostraDialogo;
    public event Action OnOcultaDialogo;
    
    public static DialogManager Instance{ //Expõe o DialogManager pra Unity. Qualquer objeto poderá usá-lo
        get;
        private set;
    }

    private void Awake(){
        Instance = this; 
    }

    Dialog dialogo;
    int falaAtual = 0;
    bool escrevendo; // Vai verificar se está escrevendo o texto na Caixa de Diálogo. Necessário para garantir que o texto será terminado.

    public void HandleUpdate(){
        // Avisa ao sistema que o jogo está entrando e saindo do estado de Diálogo
        if(Input.GetKeyDown(KeyCode.C) && !escrevendo){ // Se eu já pressionei o botão C, e aperto de novo...
            ++falaAtual;
            if(falaAtual < dialogo.Falas.Count){ // Se ainda há falas sobrando...
                StartCoroutine(DigitaTexto(dialogo.Falas[falaAtual]));
            }
            else{
                caixaDeDialogo.SetActive(false); // Oculta a Caixa de Diálogo
                falaAtual = 0; // Reseta o diálogo.
                OnOcultaDialogo?.Invoke();
            }
        }
    }

    public IEnumerator MostraDialogo(Dialog dialogo){
        OnMostraDialogo?.Invoke();
        yield return new WaitForEndOfFrame();// Para evitar que possa ser pressionado mais de uma vez.
        this.dialogo = dialogo;
        caixaDeDialogo.SetActive(true); // Exibe a Caixa de Diálogo (gameObject)
        StartCoroutine(DigitaTexto(dialogo.Falas[0]));
    }

    public IEnumerator DigitaTexto(string fala){ // Exibe a fala letra por letra
        escrevendo = true;
        textoDialogo.text = ""; // 1. Primeiro apaga todo o texto
        foreach (var letra in fala.ToCharArray()){ // 2. Reexibe o texto letra por letra
            textoDialogo.text += letra; // 3. Adiciona a letra ao texto já existente
            yield return new WaitForSeconds(1f / letrasPorSegundo); // 4. Delay
        }
        escrevendo = false;
    }
}
