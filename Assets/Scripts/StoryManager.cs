using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public int storyProgress = -1;

    public int activeStoryChapter = 1;
    
    public DialogueManager dialogueManager;

    public DialogueTrigger dialogueTrigger;

    public GameObject[] scriptedEvents;
    GameObject activeEvent;

    public GameManager gameManager;

    public Animator competAnimator;

    SoundManager soundManager;
    float waitTimer = 0.0f;

    const int nextIndex = 4;


    void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();

        gameManager.activeGameMode = GameManager.GameMode.Story;
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
                switch(activeStoryChapter)
                {
                    case 1:
                        CheckWaitEventsChapter1();
                        break;

                    case 2:
                        CheckWaitEventsChapter2();
                        break;
                    
                    default:
                        break;
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

    void CheckStoryRewards()
    {
        if(!gameManager.chaptersCleared[activeStoryChapter-1])
        {
            switch(activeStoryChapter)
            {
                case 1:
                    gameManager.GainXP(200);
                    gameManager.chaptersUnlocked[activeStoryChapter] = true;
                    break;

                case 2:
                    gameManager.GainXP(250);
                    gameManager.inventory["Rice"] = gameManager.inventory["Rice"] + 1;
                    break;

                default:
                    break;
            }
        } 
        
        gameManager.chaptersCleared[activeStoryChapter-1] = true;
  
    }

    //TODO: Decouple this from the Manager script... maybe ScriptableObject?
    void CheckForStoryEvents()
    {
        switch(activeStoryChapter)
        {
            case 1:
                CheckForStoryEventsChapter1();
                break;
            
            case 2:
                CheckForStoryEventsChapter2();
                break;

            default:
                break;
        }
        return;
    }

    //TODO: Decouple this from the Manager script... maybe ScriptableObject?
    public void TriggerChoiceEvent(string choiceLabel)
    {
        // Called from clicking a DialogueChoice
        switch(activeStoryChapter)
        {
            case 1:
                TriggerChoiceEventsChapter1(choiceLabel);
                break;

            case 2:
                TriggerChoiceEventsChapter2(choiceLabel);
                break;

            default:
                break;
        }
        
    }

    void DisableNextButton(float cooldown)
    {
        waitTimer = cooldown;
        activeEvent = scriptedEvents[nextIndex];
        activeEvent.SetActive(false);
    }

    IEnumerator MoveSceneObject(GameObject objectToMove, Vector3 targetPosition, float speed)
    {
        while(objectToMove.transform.position != targetPosition)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

    }


    #region Story Events Chapter 1
    void CheckForStoryEventsChapter1()
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
                soundManager.PlaySoundByName("shocked");
                soundManager.PlaySoundByName2("marshmallow calls! (loop as long as needed)");
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
                soundManager.PlaySoundByName("shocked");
                soundManager.PlaySoundByName2("marshmallow calls! (loop as long as needed)");
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
                soundManager.PlaySoundByName2("The Beginning - Patrick Muczczek");
                DisableNextButton(5.0f);
                break;

            case 7:
                dialogueTrigger.TriggerDialogue("story_intro_04");
                //soundManager.StopSound2();
                break;

            case 8:
                scriptedEvents[0].SetActive(true);
                scriptedEvents[2].SetActive(false);
                scriptedEvents[3].SetActive(false);
                dialogueTrigger.TriggerDialogue("story_intro_05");
                break;

            case 9:
                waitTimer = 3.0f;
                CheckStoryRewards();
                break;

            case 10:
                gameManager.activeGameMode = GameManager.GameMode.Standard;
                gameManager.LoadScene("Scenes/Menu");
                break;
            
            default:
                break;
        }
    }

    void TriggerChoiceEventsChapter1(string choiceLabel)
    {
        switch(choiceLabel)
        {
            case "story_intro_02_choice_anruf_ablehnen":
                storyProgress = -11;
                dialogueTrigger.TriggerDialogue(choiceLabel);
                scriptedEvents[3].SetActive(false);

                soundManager.StopSound2();

                break;
            
            case "story_intro_02_choice_anruf_annehmen":
                storyProgress = 4;
                dialogueTrigger.TriggerDialogue(choiceLabel);
                scriptedEvents[3].SetActive(false);

                soundManager.StopSound2();

                break;

            default:
                activeEvent.SetActive(false);
                dialogueTrigger.TriggerDialogue(choiceLabel);
                break;
        }
    }

    void CheckWaitEventsChapter1()
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

    #endregion Story Events Chapter 1

    #region Story Events Chapter 2
    void CheckForStoryEventsChapter2()
    {
        // Scripted Story Events after DialogueScripts Storylevel go here...
        switch(storyProgress)
        {
            case -1:
                waitTimer = 1.0f;
                break;

            case 0:
                dialogueTrigger.TriggerDialogue("story_02_1");
                break;

            case 1:
                activeEvent = scriptedEvents[1];
                activeEvent.SetActive(true);
                break;

            case 2:
                dialogueTrigger.TriggerDialogue("story_02_2");
                break;

            case 3:
                waitTimer = 3.0f;
                activeEvent = scriptedEvents[0];
                competAnimator.SetBool("isMoving", true);
                StartCoroutine(MoveSceneObject(activeEvent, scriptedEvents[5].transform.position, 3.0f));
                break;

            case 4:
                competAnimator.SetBool("isMoving", false);
                waitTimer = 0.5f;
                break;

            case 5:
                dialogueTrigger.TriggerDialogue("story_02_3");
                break;

            case 6:
                waitTimer = 1.0f;
                break;

            case 7:
                activeEvent = scriptedEvents[0];
                scriptedEvents[6].SetActive(false);
                scriptedEvents[8].SetActive(false);
                scriptedEvents[11].SetActive(false);
                activeEvent.transform.position = scriptedEvents[10].transform.position;
                competAnimator.SetBool("isMoving", true);
                StartCoroutine(MoveSceneObject(activeEvent, new Vector3(0.0f, -4.02f, 0.0f), 3.0f));
                waitTimer = 3.0f;
                break;

            case 8:
                competAnimator.SetBool("isMoving", false);
                StopAllCoroutines();
                dialogueTrigger.TriggerDialogue("story_02_4");
                break;

            case 9:
                activeEvent = scriptedEvents[9];
                activeEvent.SetActive(true);
                break;

            case 10:
                dialogueTrigger.TriggerDialogue("story_02_5");
                break;

            case 11:
                // Completed Story Chapter 2! Rewards:
                waitTimer = 3.0f;
                CheckStoryRewards();
                break;

            case 12:
                gameManager.activeGameMode = GameManager.GameMode.Standard;
                gameManager.LoadScene("Scenes/Menu");
                break;

            default:
                break;
        }
    }

    void TriggerChoiceEventsChapter2(string choiceLabel)
    {
        switch(choiceLabel)
        {
            default:
                activeEvent.SetActive(false);
                dialogueTrigger.TriggerDialogue(choiceLabel);
                break;
        }
    }

    void CheckWaitEventsChapter2()
    {
        // Delay on storyload and end
        if(storyProgress == -1)
        {
            ProgressStory();
        }
        else if(storyProgress == 3)
        {
            StopAllCoroutines();
            scriptedEvents[6].SetActive(true);
            scriptedEvents[0].transform.position = scriptedEvents[7].transform.position;
            scriptedEvents[8].SetActive(true);
            ProgressStory();
        }

        else if(storyProgress == 6)
        {
            scriptedEvents[11].SetActive(true);
        }

        else
        {
            //activeEvent.SetActive(true);
            ProgressStory();
        }
    }

    

    #endregion Story Events Chapter 2
}
