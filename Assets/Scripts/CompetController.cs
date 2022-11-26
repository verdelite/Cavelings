using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompetController : MonoBehaviour
{

    [SerializeField]
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked on " + gameObject);
        animator.SetTrigger("triggerHappy");
    }
}
