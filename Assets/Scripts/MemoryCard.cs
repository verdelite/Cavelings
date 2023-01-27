using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{
    public Image cardImage;

    public Button button;

    public string name;

    MemoryGameManager memoryGameManager;

    void Awake()
    {
        memoryGameManager = FindObjectOfType<MemoryGameManager>();
    }

    void Start() 
    {
        button = GetComponent<Button>();
        name = gameObject.name;
    }

    public void ClickCard()
    {
        if(!memoryGameManager.CanClick() || cardImage.gameObject.activeSelf) return;

        cardImage.gameObject.SetActive(true);
        print("Clicked on " + name);

        memoryGameManager.ClickCard(this);
    }

    public void ResetCard()
    {
        cardImage.gameObject.SetActive(false);
    }
}
