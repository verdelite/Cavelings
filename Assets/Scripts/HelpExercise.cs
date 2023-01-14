using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct HelpExercise
{

    //HelpManager helpManager;
    //public HelpManager.Exercise exerciseType;

    public string title;

    [TextArea(3,10)]
    public string body;

}
