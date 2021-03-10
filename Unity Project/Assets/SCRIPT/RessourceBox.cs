using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceBox : MonoBehaviour
{
    // The objects displayed for energy, water, and material
    public GameObject energy, water, material;

    //The text objects that show the amount of energy, water, and material the user has
    public Text energyText, waterText, materialText;

    //Determines whether you will show or hide the zeros
    public bool hideZero;

    /// <summary>
    /// Is called whenever the resource box needs to have its display updated
    /// </summary>
    /// <param name="delta">the new resource value to update to</param>
    public void UpdateRessourceBox(Resource delta){
        ProcessBox(energy, energyText, GM.I.art.yellow, delta.r[0]);
        ProcessBox(water, waterText, GM.I.art.blue, delta.r[1]);
        ProcessBox(material, materialText, GM.I.art.brown, delta.r[2]);
    }

    /// <summary>
    /// Updates the text to display the current gain/loss of resources
    /// </summary>
    /// <param name="box">The box holding the resource</param>
    /// <param name="text">The text on the resource box</param>
    /// <param name="goodColor">The color that is used if the resource is increasing</param>
    /// <param name="value">The amount the resource is changing by</param>
    void ProcessBox(GameObject box, Text text, Color goodColor, float value){
        if(value > 0){
            if(hideZero)
                box.SetActive(true);
            text.text = "+" + Mathf.Round(value*10f)/10f;
            text.color = goodColor;
        }else if(value < 0){
            if(hideZero)
                box.SetActive(true);
            text.text = ""+Mathf.Round(value*10f)/10f;
            text.color = GM.I.art.red;
        }else{
            text.text = "0";
            text.color = GM.I.art.gray;
            box.SetActive(!hideZero);
        }
    }
}
