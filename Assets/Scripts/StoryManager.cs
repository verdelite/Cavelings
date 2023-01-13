using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public int storyProgress = 0;
    
    public DialogueManager dialogueManager;

    public DialogueTrigger dialogueTrigger;

    public GameObject[] scriptedEvents;
    GameObject activeEvent;


    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Start()
    {
        //dialogueTrigger.TriggerDialogue("story_intro_01");
        CheckForStoryEvents();
    }

    public void ProgressStory()
    {
        storyProgress++;
        CheckForStoryEvents();
    }

    public void SetStoryProgress(int newProgress)
    {
        storyProgress = newProgress;
        CheckForStoryEvents();
    }

    void CheckForStoryEvents()
    {
        // Scripted Story Events after DialogueScripts Storylevel go here...
        switch(storyProgress)
        {
            case 0:
                dialogueTrigger.TriggerDialogue("story_intro_01");
                break;
            
            case 1:
                activeEvent = scriptedEvents[0];
                activeEvent.SetActive(true);
                break;

            case 2:
                dialogueTrigger.TriggerDialogue("story_intro_02");
                break;
            
            default:
                break;
        }
        return;
    }

    public void TriggerChoiceEvent(string choiceLabel)
    {
        // Called from clicking a DialogueChoice
        switch(choiceLabel)
        {
            default:
                activeEvent.SetActive(false);
                dialogueTrigger.TriggerDialogue(choiceLabel);
                break;
        }
    }
}
