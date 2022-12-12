using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemConsumable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    int gainedXP;
    public int cost;

    private Vector2 startPosition;

    [SerializeField]
    Canvas canvas;
    private RectTransform rectTransform;
    
    [SerializeField]
    CanvasGroup canvasGroup;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        canvasGroup.blocksRaycasts = true;
        if(rectTransform.anchoredPosition!=startPosition) rectTransform.anchoredPosition = startPosition;
    }

    public int Consume()
    {
        Debug.Log("Yum!");
        rectTransform.anchoredPosition = startPosition;
        return gainedXP;
    }
}
