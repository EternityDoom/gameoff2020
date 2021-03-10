using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningMenu : MonoBehaviour
{
    //The image shat shows up for the warning
    public GameObject warningPannel;

    //The title, description, and flavor text of the warning
    public Text title, description, flavorText;

    //The list of potential choices that the user has in response to the warning
    public List<WarningChoice> choices;

    /// <summary>
    /// The function that is used to set up and display the warning menu
    /// </summary>
    /// <param name="e">The specific event for the warning that is being called</param>
    public void InitEventMenu(Event e){
        warningPannel.SetActive(true);
        title.text = e.eventName;
        description.text = e.description;
        foreach (WarningChoice choice in choices)
        {
            choice.InitChoice(null);
        }
        for (var i = 0; i < e.choices.Count; i++)
        {
            choices[i].InitChoice(e.choices[i]);
        }
    }

    /// <summary>
    /// The function that is called in order to display the flavor text for the warning
    /// </summary>
    /// <param name="text">Thetext that is meant to be displayed on the warning</param>
    public void ShowFlavorText(string text){
        flavorText.text = text;
    }
}
