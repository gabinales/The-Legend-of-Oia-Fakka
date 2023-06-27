using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;

public class DialogManager : MonoBehaviour
{
    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    public GameController gameController;
    public DialogVariables dialogVariables;

    [SerializeField] GameObject caixaDeDialogo;
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI nome;

    [Header("Escolhas UI")]
    [SerializeField] private GameObject[] escolhas;

    private TextMeshProUGUI[] escolhasText;
    private Story currentStory;
    public bool dialogoOcorrendo { get; private set; }

    [SerializeField] int letrasPorSegundo; // Velocidade com que o texto será exibido, letra por letra

    public static DialogManager Instance
    { //Expõe o DialogManager pra Unity. Qualquer objeto poderá usá-lo
        get;
        private set;
    }

    private const string SPEAKER_TAG = "speaker";

    private void Awake()
    {
        Instance = this;
        dialogVariables = new DialogVariables(globalsInkFile.filePath);
    }

    private void Start()
    {
        dialogoOcorrendo = false;
        escolhasText = new TextMeshProUGUI[escolhas.Length];
        int index = 0;
        foreach (GameObject escolha in escolhas)
        {
            escolhasText[index] = escolha.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    Dialog dialogo;
    bool escrevendo; // Vai verificar se está escrevendo o texto na Caixa de Diálogo. Necessário para garantir que o texto será terminado.

    public void Update()
    {
        if (!dialogoOcorrendo)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !escrevendo)
        {
            ContinueStory();
        }
    }

    //chamado por Interaçao() de iInteragiveis
    public void StartDialogue(TextAsset inkJSON)
    {
        gameController.state = GameState.Dialogo;
        caixaDeDialogo.SetActive(true);
        currentStory = new Story(inkJSON.text);
        dialogoOcorrendo = true;

        //adiciona um listener para mudanças em variaveis de dialogo
        dialogVariables.StartListening(currentStory);
        
        //continua o dialogo (nesse caso, começa)
        ContinueStory();
    }

    //func que retorna valor de variaveis 
    public Ink.Runtime.Object GetVariable(string variableName){
        Ink.Runtime.Object variableValue = null;
        dialogVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null){
            Debug.Log("variavel Ink nao encontrada: " + variableName);
        }
        return variableValue;
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string nextFala = currentStory.Continue();

            StartCoroutine(DigitaTexto(nextFala));
            MostraEscolhas();

            HandleTags(currentStory.currentTags);

        }
        else
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        //return array of length 2 where 1st string is the key and 2nd is thhe value
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    nome.text = tagValue;
                    break;
            }
        }
    }

    private void ExitDialogue()
    {
        dialogoOcorrendo = false;
        caixaDeDialogo.SetActive(false);
        textoDialogo.text = "";

        dialogVariables.StopListening(currentStory);

        gameController.state = GameState.MovimentacaoLivre;
    }

    public IEnumerator DigitaTexto(string fala)
    { // Exibe a fala letra por letra
        escrevendo = true;
        textoDialogo.text = ""; // 1. Primeiro apaga todo o texto

        foreach (var letra in fala.ToCharArray())
        { // 2. Reexibe o texto letra por letra
            textoDialogo.text += letra; // 3. Adiciona a letra ao texto já existente
            yield return new WaitForSeconds(1f / letrasPorSegundo); // 4. Delay
        }
        escrevendo = false;
    }

    public void MostraEscolhas()
    {
        List<Choice> escolhasAtuais = currentStory.currentChoices;

        if (escolhasAtuais.Count > escolhas.Length)
        {
            Debug.LogError("foram dadas mais escolhas (" + escolhasAtuais.Count + ") do que o UI pode suportar. ");
        }

        int index = 0;
        //enable and initialize the choices up to the amount  of choices for this line of dialogue
        foreach (Choice escolha in escolhasAtuais)
        {
            escolhas[index].gameObject.SetActive(true);
            escolhasText[index].text = escolha.text;

            index++;
        }

        for (int i = index; i < escolhas.Length; i++)
        {
            escolhas[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    //apenas seta como padrao a primeira escolha como selecionada
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(escolhas[0].gameObject);
    }

    public void MakeChoice(int escolhaIndex)
    {
        currentStory.ChooseChoiceIndex(escolhaIndex);
        ContinueStory();
    }

}
