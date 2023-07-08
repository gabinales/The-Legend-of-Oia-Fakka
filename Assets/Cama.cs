using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cama : MonoBehaviour, iInteragivel
{
    [SerializeField] private GameObject globalLight;
    [SerializeField] private GameObject gameController;

    [SerializeField] private TextAsset inkJSON;

    private UnityEngine.Rendering.Universal.Light2D light2D;

    private void Start()
    {
        // Get the Light2D component from the globalLight GameObject
        light2D = globalLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    public void Interacao()
    {
        DialogManager.Instance.StartDialogue(inkJSON);

        light2D.intensity -= 0.3f;
        light2D.color = Color.red;
        /*  iluminacao = globalLight.GetComponent < Light 2D > ().Intensity;
         iluminacao++; */
    }

}
