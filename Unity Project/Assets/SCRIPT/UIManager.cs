using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //The game object for the game screen, the win screen that pops up when the user wins, and the lose screen that pops up when the user loses
    public GameObject gameScreen, winScreen, looseScreen;

    //The information for the currently selected building
    public BuildingInformation buildingInformation;
    
    //The UI for the building menu
    public BuildingMenu buildingMenu;
    
    //The UI for the time keeper controls and clock
    public TimeKeeper timeKeeper;
    
    //The portion of the screen that displays the status of the population
    public PopulationMenu populationMenu;
    
    //The portion of the screen that displays the status of the resources
    public ResourceMeters resourceMeters;
    
    //The UI for the warning menu that pops up
    public WarningMenu warningMenu;

    /// <summary>
    /// Used to modify the human count into a readable number separated by apostrophess
    /// </summary>
    /// <param name="number">The number of humans in the population</param>
    /// <returns>Returns the text todisplaythe current human population</returns>
    public static string HumanNotation(uint number){
        string newString = ""+number;
        for (int i = 3; i < newString.Length; i += 4)
        {
            newString = newString.Insert(newString.Length - i, "'");
        }
        return newString;
    }
    
    /// <summary>
    /// Calls the HumanNotation method that takes in a uint after converting the passed in value to a uint
    /// </summary>
    /// <param name="number">The number of humans in the population</param>
    /// <returns>Returns the text todisplaythe current human population</returns>
    public static string HumanNotation(int number){
        return HumanNotation((uint)number);
    }

    /// <summary>
    /// Used to display the net increase of the human population
    /// </summary>
    /// <param name="number">The number of humans born into the population</param>
    /// <returns>Returns the text to display the net increase of the population</returns>
    public static string HumanNotationSigned(int number){
        string newString = ""+Mathf.Abs(number);
        for (int i = 3; i < newString.Length; i += 4)
        {
            newString = newString.Insert(newString.Length - i, "'");
        }
        if(number < 0){
            newString = "-"+newString;
        }else{
            newString = "+"+newString;
        }
        return newString;
    }

    /// <summary>
    /// Hides the game screen and shows the win screen
    /// </summary>
    public void ShowWinScreen(){
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
    }
    
    /// <summary>
    /// Hides the lose game screen and shows the lose screen
    /// </summary>
    public void ShowLooseScreen(){
        gameScreen.SetActive(false);
        looseScreen.SetActive(true);
    }

    /// <summary>
    /// Closes all openable UI
    /// </summary>
    public void CloseAllUI(){
        if(populationMenu.gameObject.activeInHierarchy){
            populationMenu.ClicPopulationMenu(false);
        }
        if(populationMenu.moodMenu.activeInHierarchy){
            populationMenu.ClicMoodMenu(false);
        }
        if(buildingMenu.gameObject.activeInHierarchy){
            buildingMenu.ClicBuildingMenu(false);
        }
        buildingInformation.ShowBuildingInfo(null);
    }

    /// <summary>
    /// Used to display the percent increase/decrease of the population per time cycle
    /// </summary>
    /// <param name="number">The amount that the population increases/decreases by</param>
    /// <returns>Returnsthe text to display the percentage increase/decrease of the population</returns>
    public static string HumanNotation(float number){
        string newString = "";
        int percent = (int)(100f * (number/1f));
        newString = "" + percent + "%";
        return newString;
    }
    
    /// <summary>
    /// Converts the time to a displayable month value
    /// </summary>
    /// <param name="time">The total time that the game has been running</param>
    /// <returns>Returns the text to display to the date portion of the UI</returns>
    public static string TimeToDate(int time){
        int month = time % 12;
        int year = (time-month)/12;
        string clockText = "";
        switch (month)
        {
            case 0:
            clockText += "jan ";
            break;
            case 1:
            clockText += "feb ";
            break;
            case 2:
            clockText += "mar ";
            break;
            case 3:
            clockText += "apr ";
            break;
            case 4:
            clockText += "may ";
            break;
            case 5:
            clockText += "jun ";
            break;
            case 6:
            clockText += "jul ";
            break;
            case 7:
            clockText += "aug ";
            break;
            case 8:
            clockText += "sep ";
            break;
            case 9:
            clockText += "oct ";
            break;
            case 10:
            clockText += "nov ";
            break;
            case 11:
            clockText += "dec ";
            break;
        }
        clockText += "" + (2150 + year);
        return clockText;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string ColoredString(string text, Color color){
        return "<color=#"+ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }

}
