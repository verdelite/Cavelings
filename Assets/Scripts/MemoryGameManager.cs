using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MemoryGameManager : MonoBehaviour
{

    public List<MemoryCard> memoryCards = new List<MemoryCard>();

    public List<Sprite> cardImages = new List<Sprite>();

    GameManager gameManager;
    SoundManager soundManager;
    MemoryInstantiator memoryInstantiator;

    public int numGuesses;

    public TextMeshProUGUI guessCounter;

    public int totalGuesses;
    int correctGuesses;

    MemoryCard firstCardGuess;
    MemoryCard secondCardGuess;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        memoryInstantiator = FindObjectOfType<MemoryInstantiator>();
    }

    void Start() 
    {
        GetCards();
        AddMemoryCards();
        UpdateGuessCounter();
        memoryInstantiator.ShuffleCards(memoryInstantiator.cardList);
    }

    void GetCards() 
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MemoryCard");

        for(int i = 0; i < objects.Length; i++)
        {
            memoryCards.Add(objects[i].GetComponent<MemoryCard>());
            //memoryCards[i].cardImage.sprite = cardImages[i];
        }
    }

    void AddMemoryCards()
    {
        int looper = memoryCards.Count;
        int index = 0;

        for(int i=0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }

            memoryCards[i].cardImage.sprite = cardImages[index];
            index++;
        }
    }

    public void ClickCard(MemoryCard cardClicked)
    {
        soundManager.PlaySoundByName("button click");
        // First Guess
        if(numGuesses == 0)
        {
            numGuesses = 1;

            firstCardGuess = cardClicked;
        } 
        // Second Guess
        else if(numGuesses == 1)
        {
            numGuesses = 2;
            secondCardGuess = cardClicked;
            totalGuesses++;
            UpdateGuessCounter();
            StartCoroutine(CheckForMatch());
        }
    }

    public bool CanClick()
    {
        return numGuesses < 2;
    }

    IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1.0f);

        if(firstCardGuess.cardImage.sprite.name == secondCardGuess.cardImage.sprite.name)
        {
            print("Match found!");
            soundManager.PlaySoundByName("shocked");

            yield return new WaitForSeconds(0.5f);

            CheckForWin();
        } 
        else
        {
            print("Not matching!");
            soundManager.PlaySoundByName("button back");

            firstCardGuess.ResetCard();
            secondCardGuess.ResetCard();

            firstCardGuess = secondCardGuess = null;
        }

        numGuesses = 0;
    }

    void CheckForWin()
    {
        correctGuesses++;

        if(correctGuesses == memoryCards.Count/2)
        {
            soundManager.PlaySoundByName("level up");
            
            StartCoroutine(ResetGame());
        }
    }

    void UpdateGuessCounter()
    {
        guessCounter.text = "Total Guesses: " + totalGuesses;
    }

    IEnumerator ResetGame()
    {

        yield return new WaitForSeconds(3.0f);

        totalGuesses = 0;
        correctGuesses = 0;
        UpdateGuessCounter();

        foreach(MemoryCard card in memoryCards)
        {
            card.ResetCard();
        }
        memoryInstantiator.ShuffleCards(memoryInstantiator.cardList);
    }
}
