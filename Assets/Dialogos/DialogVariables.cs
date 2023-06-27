using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogVariables
{
    //                tipo da chave e valor        nome do dict
    public Dictionary<string, Ink.Runtime.Object> variables {get; private set;}

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        //adiciona a funçao variable observer como o "listener" quando uma varivel muda
        story.variablesState.variableChangedEvent += VariableObserver;

    }
    public void StopListening(Story story)
    {
        //adiciona a funçao variableObserver como o "listener" quando uma varivel muda
        //variableChangedEvent é um metodo do ink que recebe nome e valor da variavel
        story.variablesState.variableChangedEvent -= VariableObserver;

    }

    void VariableObserver(string name, Ink.Runtime.Object value)
    {
        //apenas afeta o dict variables, que é global
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    public DialogVariables(string globalsFilePath)
    {
        //como o arquivo globals.ink não compila em Json, essa funçao serve para compilar
        //manualmente o arquivo em um objeto do tipo Story
        string inkFileContents = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("nova variável global adicionada: " + name + " = " + value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
