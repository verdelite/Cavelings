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

    public Animator competAnimator;

    public AudioSource faveSong;
    float songWait = 0.0f;


    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Start()
    {
        //dialogueTrigger.TriggerDialogue("story_intro_01");
        CheckForStoryEvents();
    }

    void Update()
    {
        if(songWait > 0)
        {
            songWait -= Time.deltaTime;
            if(songWait <= 0) activeEvent.SetActive(true);
        }
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
                activeEvent = scriptedEvents[1];
                activeEvent.SetActive(true);
                break;

            case 2:
                competAnimator.SetTrigger("triggerShocked");
                dialogueTrigger.TriggerDialogue("story_intro_02");
                break;

            case 3:
                competAnimator.ResetTrigger("triggerShocked");
                dialogueTrigger.TriggerDialogue("story_intro_02_2");
                scriptedEvents[0].SetActive(false); // Hide Cavi
                scriptedEvents[2].SetActive(true); // Change BG Image
                activeEvent = scriptedEvents[3];
                scriptedEvents[3].SetActive(true);  // Enable Call buttons
                break;

            case 4:
                print("Anruf annehmen?");
                //scriptedEvents[3].SetActive(true); // Enable buttons
                break;

            case -10:
                dialogueTrigger.TriggerDialogue("story_intro_02_choice_anruf_ablehnen_loop_1");
                scriptedEvents[0].SetActive(true);
                scriptedEvents[2].SetActive(false);
                scriptedEvents[3].SetActive(false);
                break;

            case -9:
                dialogueTrigger.TriggerDialogue("story_intro_02_choice_anruf_ablehnen_loop_2");
                competAnimator.SetTrigger("triggerShocked");
                break;

            case -8:
                dialogueTrigger.TriggerDialogue("story_intro_02_choice_anruf_ablehnen_loop_3");
                competAnimator.ResetTrigger("triggerShocked");
                scriptedEvents[0].SetActive(false); // Hide Cavi
                scriptedEvents[2].SetActive(true); // Change BG Image
                activeEvent = scriptedEvents[3];
                scriptedEvents[3].SetActive(true);  // Enable Call buttons
                break;

            case 5:
                dialogueTrigger.TriggerDialogue("story_intro_03");
                break;

            case 6:
                dialogueTrigger.TriggerDialogue("story_intro_03_song");
                faveSong.Play();
                songWait = 10.0f;
                activeEvent = scriptedEvents[4];
                activeEvent.SetActive(false);
                break;

            case 7:
                dialogueTrigger.TriggerDialogue("story_intro_04");
                break;

            case 8:
                scriptedEvents[0].SetActive(true);
                scriptedEvents[2].SetActive(false);
                scriptedEvents[3].SetActive(false);
                dialogueTrigger.TriggerDialogue("story_intro_05");
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
            case "story_intro_02_choice_anruf_ablehnen":
                storyProgress = -11;
                dialogueTrigger.TriggerDialogue(choiceLabel);
                scriptedEvents[3].SetActive(false);
                break;
            
            case "story_intro_02_choice_anruf_annehmen":
                storyProgress = 4;
                dialogueTrigger.TriggerDialogue(choiceLabel);
                scriptedEvents[3].SetActive(false);
                break;

            default:
                activeEvent.SetActive(false);
                dialogueTrigger.TriggerDialogue(choiceLabel);
                break;
        }
    }
}
