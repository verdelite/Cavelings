using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemConsumable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    int gainedXP;
    public int cost;

    public string itemType;
    public int amountOwned = 0;

    private Vector2 startPosition;

    [SerializeField]
    Canvas canvas;
    private RectTransform rectTransform;

    [SerializeField]
    Image itemIcon;
    
    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    GameObject panelFood;

    GameManager gameManager;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;

        gameManager = FindObjectOfType<GameManager>();
        if(gameManager)
        {
            if(gameManager.inventory.ContainsKey(itemType))
            {
                amountOwned = gameManager.inventory[itemType];
            } else
            {
                gameManager.inventory.Add(itemType, amountOwned);
            }
        }
        
    }

    void Start()
    {
        CheckAmountOwned();
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        if(CheckAmountOwned())
        {
            canvasGroup.blocksRaycasts = false;
        }       
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if(CheckAmountOwned())
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        canvasGroup.blocksRaycasts = true;
        if(rectTransform.anchoredPosition!=startPosition) rectTransform.anchoredPosition = startPosition;
        CheckAmountOwned();
    }

    public int Consume()
    {
        if(CheckAmountOwned())
        {
            rectTransform.anchoredPosition = startPosition;
            amountOwned--;
            gameManager.inventory[itemType] = amountOwned;
            CheckAmountOwned();
            return gainedXP;
        }
    
        print("Error: Consume despite amount <= 0");
        return 0;
        
    }

    public bool CheckAmountOwned()
    {
        ToggleIcon(amountOwned > 0);
        return amountOwned > 0;
    }

    void ToggleIcon(bool toggle)
    {
        var tempColor = itemIcon.color;
        tempColor.a = toggle ? 1.0f : 0.0f;
        itemIcon.color = tempColor;
    }


}
