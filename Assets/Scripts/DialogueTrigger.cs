using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public DialogueScript[] dialogueScript;
    DialogueManager dialogueManger;

    void Awake()
    {
        dialogueManger = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue(string scriptName)
    {
        DialogueScript scriptToTrigger = GetScriptByName(scriptName);
        
        if(scriptToTrigger)
        {
            dialogueManger.StartDialogue(scriptToTrigger);
        }
        else
        {
            print("Script with name " + scriptName + " not found!");
        }

    }

    public DialogueScript GetScriptByName(string scriptName)
    {
        foreach(DialogueScript scriptPart in dialogueScript)
        {
            if(scriptPart.scriptName == scriptName)
            {
                return scriptPart;
            }
        }

        return null;
    }
}
