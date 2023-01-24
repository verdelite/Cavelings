using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public SoundManager soundManager;

    public int currentXP = 0;
    public int missingXP = 200;
    public int currentLevel = 1;

    int baseXPNeeded = 200;

    public List<ItemConsumable> inventory;

    public CompetController compet;
    public UIController uiController;

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
            baseXPNeeded += 50;
            missingXP = baseXPNeeded;

            soundManager.PlaySoundByName2("level up");
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
}
