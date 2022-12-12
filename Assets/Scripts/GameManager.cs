using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentXP = 0;
    public int missingXP = 100;
    public int currentLevel = 1;

    public List<ItemConsumable> inventory;

    public CompetController compet;
    public UIController uiController;

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
            currentLevel++;
            currentXP =-missingXP;
            missingXP = 100;
        }
        uiController.SetLevel(currentLevel);
    }

    public void UpdateXPBar()
    {
        uiController.SetXP(currentXP);
        uiController.SetMaxXP(currentXP + missingXP);
    }
}
