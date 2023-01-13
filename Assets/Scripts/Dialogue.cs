using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string label;
    public string text;

    public string GetDialogueText()
    {
        return label + "\n" + text;
    }
}
