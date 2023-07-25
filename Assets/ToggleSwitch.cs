using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour, ISwitch
{
    [Header("Geral")]
    public bool estaAtivado;
    //public List<GameObject> objetosAfetados;
    public EventReference sfxSwitchOn;
    public EventReference sfxSwitchOff;
    public bool autoReset;
    public float timerReset;

    [Header("Configurações")]
    public bool temAnimator;
    Animator animator;

    public UnityEvent onSwitchAtivado;
    public UnityEvent onSwitchDesativado;

    private void Awake() {
        if(temAnimator){
            animator = GetComponent<Animator>();
        }
    }

    public void Toggle(){
        StopAllCoroutines();

        estaAtivado = !estaAtivado;

        if(animator != null){
            animator.SetBool("ligado", estaAtivado);
        }
        
        if(estaAtivado){
            var fmodEvent = RuntimeManager.CreateInstance(sfxSwitchOn);
            fmodEvent.start();
            onSwitchAtivado.Invoke();
        }
        else if(!estaAtivado){
            var fmodEvent = RuntimeManager.CreateInstance(sfxSwitchOff);
            fmodEvent.start();
            onSwitchDesativado.Invoke();
        }

        // Objeto(s) afetado(s) pela alavanca:
        /*foreach(GameObject obj in objetosAfetados){
            ISwitch switchObject = obj.GetComponent<ISwitch>();
            if(switchObject != null){
                switchObject.Toggle();
            }
        }*/

        //

        if(estaAtivado && autoReset){ 
            StartCoroutine(SwitchReset());
        }
    }

    private IEnumerator SwitchReset(){
        yield return new WaitForSeconds(timerReset);
        var fmodEvent = RuntimeManager.CreateInstance(sfxSwitchOff);
        fmodEvent.start();

        estaAtivado = false;

        onSwitchDesativado.Invoke();

        if(animator != null){
            animator.SetBool("ativado", estaAtivado);
        }
    }
}
