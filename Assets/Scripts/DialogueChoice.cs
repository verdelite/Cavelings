using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueChoice : MonoBehaviour
{
    public TextMeshProUGUI choice;

    public string choiceText;

    public string choiceLabel;

    StoryManager storyManager;

    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        if(choice) choice.text = choiceText;
    }

    public void TriggerChoiceEvent()
    {
        print("Triggering Choice event! " + choiceLabel);
        storyManager.TriggerChoiceEvent(choiceLabel);
    }
}
