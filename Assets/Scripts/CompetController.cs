using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CompetController : MonoBehaviour, IDropHandler
{

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject comic;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    int clickXP = 5;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            ItemConsumable itemToConsume = eventData.pointerDrag.GetComponent<ItemConsumable>();
            if(itemToConsume)
            {
                GainXP(itemToConsume.Consume());
                animator.SetTrigger("triggerHappy");
            }
        }
    }

    void OnMouseDown()
    {
        //comic.SetActive(false);
        if(gameObject.tag=="Compet") animator.SetTrigger("triggerHappy");
        GainXP(clickXP);
    }

    void GainXP(int gainedXP)
    {
        gameManager.GainXP(gainedXP);
    }
}
