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

    const float _petCooldown = 1.0f;
    float activePetCooldown = 0.0f;

    void Update()
    {
        if(activePetCooldown > 0) activePetCooldown -= Time.deltaTime;
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("Dropped " +eventData);
        if(eventData.pointerDrag != null)
        {
            ItemConsumable itemToConsume = eventData.pointerDrag.GetComponent<ItemConsumable>();
            if(itemToConsume)
            {
                ResetAnimationTriggers();
                GainXP(itemToConsume.Consume());
                animator.SetTrigger("triggerEating");
            }
        }
    }

    void OnMouseDown()
    {
        //comic.SetActive(false);
        if(gameObject.tag=="Compet")
        {
            if(activePetCooldown <= 0)
            {
                animator.SetTrigger("triggerHappy");
                GainXP(clickXP);
                activePetCooldown = _petCooldown;
            }
        }
        
    }

    void GainXP(int gainedXP)
    {
        gameManager.GainXP(gainedXP);
    }

    void ResetAnimationTriggers()
    {
        animator.ResetTrigger("triggerEating");
        animator.ResetTrigger("triggerHappy");
        animator.ResetTrigger("triggerShocked");
    }
}
