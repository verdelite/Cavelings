using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;
    public UIXPBar friendshipBar;

    [SerializeField]
    TextMeshProUGUI levelDisplay;

    public void SetMaxXP(int maxXP)
    {
        friendshipBar.SetMaxXP(maxXP);
    }

    public void SetXP(int currentXP)
    {
        friendshipBar.SetXP(currentXP);
    }

    public void SetLevel(int level)
    {
        levelDisplay.text = level.ToString();
    }
}
