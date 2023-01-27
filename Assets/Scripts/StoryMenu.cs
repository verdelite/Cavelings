using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class StoryMenu : MonoBehaviour
{
    public StoryChapterMenu activeChapter;

    [SerializeField]
    TextMeshProUGUI tmpTitle;

    [SerializeField]
    TextMeshProUGUI tmpDescription;

    [SerializeField]
    TextMeshProUGUI tmpConditions;

    GameManager gameManager;

    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    public void SetActiveChapter(StoryChapterMenu newChapter)
    {
        activeChapter = newChapter;

        tmpTitle.text = newChapter.title;
        tmpDescription.text = newChapter.description;
        tmpConditions.text = newChapter.unlockConditions;
    }

    public void ReadActiveChapter()
    {
        if(activeChapter.unlocked)
        {
            gameManager.LoadScene(activeChapter.levelToLoad);
        }
    }
}
