using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryInstantiator : MonoBehaviour
{

    [SerializeField]
    Transform memoryPanel;

    [SerializeField]
    GameObject card;

    public List<GameObject> cardList = new List<GameObject>();

    public int count = 8;

    void Awake() 
    {
        for(int i = 0; i < count; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.name = "Card " + i;
            newCard.transform.SetParent(memoryPanel, false);
            cardList.Add(newCard);
        }
    }

    public void ShuffleCards(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            GameObject tempCard = list[i];
            int randomIndex = Random.Range(0, list.Count);

            list[i].transform.SetSiblingIndex(randomIndex);
            list[randomIndex] = tempCard;
        }
    }
}
