using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();

    public TextMeshProUGUI labelText;
    public TextMeshProUGUI dialogueText;

    public Animator dialogueAnimator;

    public StoryManager storyManager;

    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
    }

    public void StartDialogue(DialogueScript dialogueScript)
    {
        dialogueAnimator.SetBool("IsOpen", true);
    
        dialogueQueue.Clear();

        foreach(Dialogue dialogue in dialogueScript.dialogueText)
        {
            dialogueQueue.Enqueue(dialogue);
        }
    
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if(dialogueQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = dialogueQueue.Dequeue();
        //dialogueText.text = currentDialogue.text;

        StopAllCoroutines();
        StartCoroutine(TypeDialogue(currentDialogue.text));

        labelText.text = currentDialogue.label;
    }

    void EndDialogue()
    {
        dialogueAnimator.SetBool("IsOpen", false);
        storyManager.ProgressStory();
    }

    IEnumerator TypeDialogue(string textToType)
    {
        dialogueText.text = "";
        foreach (char letter in textToType.ToCharArray())
        {
            dialogueText.text += letter;
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
    }
}
