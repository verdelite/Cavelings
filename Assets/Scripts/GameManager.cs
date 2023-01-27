using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public SoundManager soundManager;

    public int currentXP = 0;
    public int missingXP = 100;
    public int currentLevel = 1;

    int baseXPNeeded = 200;

    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public List<bool> accessoriesActive;

    public Dictionary<int, StoryChapterMenu> storyChapters = new Dictionary<int, StoryChapterMenu>();
    public List<bool> chaptersUnlocked;
    public List<bool> chaptersCleared;

    public CompetController compet;
    public UIController uiController;


    public enum GameMode
    {
        Standard,
        Story
    }

    public GameMode activeGameMode = GameMode.Standard;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        } 
    }

    void Start() 
    {
        UpdateXPBar();
        UpdateLevel();
    }

    public void GainXP(int gainedXP)
    {
        currentXP += gainedXP;
        missingXP -= gainedXP;
        UpdateLevel();
        UpdateXPBar();
    }

    public void UpdateLevel()
    {
        if(missingXP<=0)
        {
            // Level Up!
            currentLevel++;
            currentXP =-missingXP;
            baseXPNeeded = (int) (baseXPNeeded * 1.2f);
            missingXP = baseXPNeeded;

            soundManager.PlaySoundOnce("level up");
        }
        uiController.SetLevel(currentLevel);
    }

    public void UpdateXPBar()
    {
        uiController.SetXP(currentXP);
        uiController.SetMaxXP(currentXP + missingXP);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("On Scene Loaded: " + scene + " Mode: " + mode);
        compet = FindObjectOfType<CompetController>();
        uiController = FindObjectOfType<UIController>();
        if(uiController)
        {
            UpdateLevel();
            UpdateXPBar();
        }
    }

    public void UpdateAccessory(int index, bool active)
    {
        accessoriesActive[index] = active;
    }
}
