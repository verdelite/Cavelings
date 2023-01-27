using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class StoryChapterMenu : MonoBehaviour
{

    public string title;

    [TextArea(3,10)]
    public string description;

    [TextArea(3,10)]
    public string unlockConditions;

    public bool unlocked = false;

    public GameObject lockObject;
    GameManager gameManager;

    public string levelToLoad;

    public int storyIndex;

    [SerializeField]
    TextMeshProUGUI tmpTitle;

    Button[] buttons;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        buttons = GetComponentsInChildren<Button>();
        
        if(gameManager && !gameManager.storyChapters.ContainsKey(storyIndex))
        {
            gameManager.storyChapters.Add(storyIndex, this);
        }

        unlocked = gameManager.chaptersUnlocked[storyIndex];

        if(!unlocked)
        {
            if(!gameManager.chaptersUnlocked[storyIndex-1])
            {
                foreach(Button button in buttons)
                {
                    button.interactable = false;
                }
            }
        } else
        {
            foreach(Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }

    void Start()
    {
        tmpTitle.text = title;
        lockObject.SetActive(!unlocked);
    }

}
