using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField]
    GameObject btnActivity;

    [SerializeField]
    GameObject activityMenu;

    [SerializeField]
    GameObject standardMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenActivityMenu()
    {
        activityMenu.SetActive(true);
        standardMenu.SetActive(false);
    }

    void OpenStandardMenu()
    {
        standardMenu.SetActive(true);
        activityMenu.SetActive(false);
    }
}
