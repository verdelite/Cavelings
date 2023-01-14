using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpManager : MonoBehaviour
{
    public enum Exercise
    {
        Breathing, // 0
        Thinking, // 1
        Motivational // 2
    }

    public List<HelpExercise> breathingExercises;
    public List<HelpExercise> thinkingExercises;
    public List<HelpExercise> motivationalExercises;

    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textBody;

    HelpExercise activeExercise;

    Exercise activeExerciseType;

    /**public void AddExercise(HelpExercise newExercise)
    {
        switch(newExercise.exerciseType)
        {
            case Exercise.Breathing:
                breathingExercises.Add(newExercise);
                break;
            
            case Exercise.Thinking:
                thinkingExercises.Add(newExercise);
                break;

            case Exercise.Motivational:
                motivationalExercises.Add(newExercise);
                break;

            default:
                break;
        }
    }**/

    public void SetExerciseType(int index)
    {
        activeExerciseType = (Exercise)index;
        SetRandomExercise();
    }

    HelpExercise GetRandomExercise()
    {
        switch(activeExerciseType)
        {
            case Exercise.Breathing:
                return breathingExercises[Random.Range(0, breathingExercises.Count)];
            
            case Exercise.Thinking:
                return thinkingExercises[Random.Range(0, thinkingExercises.Count)];

            case Exercise.Motivational:
                return motivationalExercises[Random.Range(0, motivationalExercises.Count)];

            default:
                print("Error! Active Exercise Type is invalid!");
                return new HelpExercise();
        }
    }

    public void SetRandomExercise()
    {
        HelpExercise newExercise = GetRandomExercise();

        // Prevent getting the same exercise in a row. CAUTION: If only one exercise exists in List, this will loop infinitely
        while(newExercise.body == activeExercise.body)
        {
            newExercise = GetRandomExercise();
        }

        SetExercise(newExercise);
    }

    void SetExercise(HelpExercise newExercise)
    {
        activeExercise = newExercise;
        UpdateDisplayText();
    }

    void UpdateDisplayText()
    {
        textTitle.text = activeExercise.title;
        textBody.text = activeExercise.body;
    }
}
