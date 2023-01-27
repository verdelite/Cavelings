using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CompetController : MonoBehaviour, IDropHandler
{

    [SerializeField]
    Animator animator;

    public GameManager gameManager;
    SoundManager soundManager;

    const int clickXP = 5;
    const float _petCooldown = 1.0f;
    float activePetCooldown = 0.0f;

    public List<GameObject> accessoryList;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Start()
    {
        InitAccessories();
    }

    void Update()
    {
        if(activePetCooldown > 0) activePetCooldown -= Time.deltaTime;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            ItemConsumable itemToConsume = eventData.pointerDrag.GetComponent<ItemConsumable>();

            if(itemToConsume && itemToConsume.CheckAmountOwned())
            {
                // Item consumed successfully!

                ResetAnimationTriggers();
                GainXP(itemToConsume.Consume());
                animator.SetTrigger("triggerEating");

                soundManager.PlaySoundByName("eating");
            }
        }
    }

    void OnMouseDown()
    {
        if(gameObject.tag=="Compet" && gameManager.activeGameMode == GameManager.GameMode.Standard)
        {
            if(activePetCooldown <= 0)
            {
                // Pet successfully!
                animator.SetTrigger("triggerHappy");
                GainXP(clickXP);
                activePetCooldown = _petCooldown;

                soundManager.PlaySoundByName("happy");
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

    public void ToggleAccessory(int index)
    {
        bool newActive = !accessoryList[index].gameObject.activeSelf;
        accessoryList[index].gameObject.SetActive(newActive);

        gameManager.UpdateAccessory(index, newActive);
    }

    void InitAccessories()
    {
        for(int i=0; i < accessoryList.Count; i++)
        {
            accessoryList[i].gameObject.SetActive(gameManager.accessoriesActive[i]);
        }
    }
}
