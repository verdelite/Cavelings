using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public int storyProgress = -1;
    
    public DialogueManager dialogueManager;

    public DialogueTrigger dialogueTrigger;

    public GameObject[] scriptedEvents;
    GameObject activeEvent;

    public GameManager gameManager;

    public Animator competAnimator;

    public AudioSource faveSong;
    float waitTimer = 0.0f;

    const int nextIndex = 4;


    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        //dialogueTrigger.TriggerDialogue("story_intro_01");
        CheckForStoryEvents();
    }

    void Update()
    {
        if(waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;

            
            if(waitTimer <= 0)
            {
                // Delay on storyload and end
                if(storyProgress == -1 || storyProgress == 9)
                {
                    ProgressStory();
                }

                else
                {
                    activeEvent.SetActive(true);
                }
                
            }
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
            case -1:
                waitTimer = 1.0f;
                break;

            case 0:
                dialogueTrigger.TriggerDialogue("story_intro_01");
                break;
            
            case 1:
                activeEvent = scriptedEvents[1];
                activeEvent.SetActive(true);
                break;

            case 2:
                competAnimator.SetTrigger("triggerShocked");
                DisableNextButton(2.0f);
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
                DisableNextButton(2.0f);
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
                DisableNextButton(5.0f);
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

            case 9:
                waitTimer = 2.0f;
                break;

            case 10:
                gameManager.LoadScene("Scenes/Menu");
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

    void DisableNextButton(float cooldown)
    {
        waitTimer = cooldown;
        activeEvent = scriptedEvents[nextIndex];
        activeEvent.SetActive(false);
    }
}
