using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningChoice : MonoBehaviour
{
    //The name of the choice and the effect that it has on your resources
    public Text choiceName, choiceEffect;

    //The button that is bound to the event choice
    public Button myButton;

    //The users currently selected choice
    public EventChoice currentChoice;

    /// <summary>
    /// The method that is called in order to create the button that is displayed for the warning menu
    /// </summary>
    /// <param name="data">The data that is meant to be displayed on the button</param>
    public void InitChoice(EventChoice data){
        if(data == null){
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(true);
            currentChoice = data;
            choiceName.text = currentChoice.choiceName;
            choiceEffect.text = EffectString(currentChoice.type, currentChoice.amount);
            if(currentChoice.typeSecondary != FXT.None){
                choiceEffect.text += ", " + EffectString(currentChoice.typeSecondary, currentChoice.amountSecondary);
            }
            if(IsLimited(currentChoice.type, currentChoice.amount)||IsLimited(currentChoice.typeSecondary, currentChoice.amountSecondary)){
                myButton.interactable = false;
                choiceName.color = GM.I.art.gray;
            }else{
                myButton.interactable = true;
                choiceName.color = GM.I.art.white;
            }
        }
    }

    /// <summary>
    /// The method used to display the amount the choice effects your resources
    /// </summary>
    /// <param name="fxt">The resource that will be effected</param>
    /// <param name="amount">The amount that the specific resource will sbe effected</param>
    /// <returns>Returns the text value that will display the amount the choice effects the resources</returns>
    string EffectString(FXT fxt, float amount){
        string output = "";
        if(fxt == FXT.Needs || fxt == FXT.Comfort || fxt == FXT.Culture || fxt == FXT.Hope || fxt == FXT.Integrity){
            output += ""+UIManager.HumanNotation(amount) + " "+fxt.ToString();
            if(amount > 0){
                output = UIManager.ColoredString(output, GM.I.art.green);
            }else{
                output = UIManager.ColoredString(output, GM.I.art.red);
            }
        }else if(fxt == FXT.Death){
            output += ""+UIManager.HumanNotation(-(1f-amount)) + " Population";
            if(amount > 1){
                output = UIManager.ColoredString(output, GM.I.art.green);
            }else{
                output = UIManager.ColoredString(output, GM.I.art.red);
            }
        }else if (fxt == FXT.Energy || fxt == FXT.Water || fxt == FXT.Material){
            if(amount > 0){
                output += "+"+ amount + " "+fxt.ToString();
                output = UIManager.ColoredString(output, GM.I.art.green);
            }else{
                output += ""+ amount + " "+fxt.ToString();
                output = UIManager.ColoredString(output, GM.I.art.red);
            }
        }
        return output;
    }

    /// <summary>
    /// The method used to display the flavor text of the choice
    /// </summary>
    /// <param name="onOff">A boolean used to determine what description is shown, if true then it will display the description otherwise it will not</param>
    public void ShowFlavorText(bool onOff){
        if(onOff){
            GM.I.ui.warningMenu.ShowFlavorText(currentChoice.description);
        }else{
            GM.I.ui.warningMenu.ShowFlavorText("");
        }
    }

    /// <summary>
    /// The function that is called when a choice is selected and finalized.
    /// </summary>
    public void MakeChoice(){
        GM.I.ui.warningMenu.ShowFlavorText("");
        GM.I.ui.warningMenu.warningPannel.SetActive(false);
        ProcessEffect(currentChoice.type, currentChoice.amount);
        ProcessEffect(currentChoice.typeSecondary, currentChoice.amountSecondary);

        GM.I.ui.resourceMeters.UpdateResources();
        GM.I.ui.populationMenu.UpdateMenu();
        
    }

    /// <summary>
    /// Updates your resources based on what the choices resource effects are.
    /// </summary>
    /// <param name="type">Which resource is effected by the choice</param>
    /// <param name="amount">The amount that the resource is effected by the choice</param>
    void ProcessEffect(FXT type, float amount){
        switch (type)
        {
            case FXT.Needs:
                GM.I.people.needs += amount;
                break;
            case FXT.Birth:
                GM.I.people.Population[0] += (int)amount;
                break;
            case FXT.Comfort:
                GM.I.people.comfort += amount;
                break;
            case FXT.Culture:
                GM.I.people.culture += amount;
                break;
            case FXT.Death:
                GM.I.people.Kill(amount);
                break;
            case FXT.Energy:
                GM.I.resource.resources.r[0] += amount;
                break;
            case FXT.Hope:
                GM.I.people.hope += amount;
                break;
            case FXT.Integrity:
                GM.I.city.ModifyIntegrity(amount);
                break;
            case FXT.Material:
                GM.I.resource.resources.r[2] += amount;
                break;
            case FXT.Water:
                GM.I.resource.resources.r[1] += amount;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates the choice to be unclickable if the amount of resources the user does not have enough resources for it
    /// </summary>
    /// <param name="type">The resource that is tied to the choice</param>
    /// <param name="amount">The amount of the resource that the user has</param>
    /// <returns>Returns a boolean that says whether an option is limited</returns>
    bool IsLimited(FXT type, float amount){
        switch (type)
        {
            case FXT.Needs:
                if(amount < -GM.I.people.needs){return true;}
                break;
            case FXT.Comfort:
                if(amount < -GM.I.people.comfort){return true;}
                break;
            case FXT.Culture:
                if(amount < -GM.I.people.culture){return true;}
                break;
            case FXT.Energy:
                if(amount < -GM.I.resource.resources.Energy){return true;}
                break;
            case FXT.Material:
                if(amount < -GM.I.resource.resources.Material){return true;}
                break;
            case FXT.Water:
                if(amount < -GM.I.resource.resources.Water){return true;}
                break;
            default:
                break;
        }
        return false;
    }
}
