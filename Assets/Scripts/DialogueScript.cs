using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueScript : MonoBehaviour
{
    public string scriptName;

    public Dialogue[] dialogueText;

    public Dialogue GetDialogueByIndex(int index)
    {
        if(index < dialogueText.Length)
        {
            return dialogueText[index];
        }
        
        print("Invalid Index given for " + scriptName);
        return null;
    }

/**
    int currentIndex = -1;

    public void SetIndex(int index)
    {
        currentIndex = index;
    }

    public Dialogue GetCurrentDialogue()
    {
        return dialogueText[currentIndex + 1];
    }

    public Dialogue TriggerDialogue()
    {
        currentIndex++;

        if(currentIndex < dialogueText.Length)
        {
            Dialogue currentDialogue = dialogueText[currentIndex];
            return currentDialogue;
        }
        else
        {
            print("End of Dialogue Part!");
            return null;
        } 
    }
**/
}
