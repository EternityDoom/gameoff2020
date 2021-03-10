using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public string linkedAchievement;
    public Text nameText;
    public GameObject icon, lockIcon, iconHolder, hint;
    public Color color;

    /// <summary>
    /// Called when the achievement is enabled, shows/hides information about the achievement
    /// </summary>
    private void OnEnable() {
        if(PlayerPrefs.GetInt(linkedAchievement, 0) == 1){
            iconHolder.SetActive(true);            
            hint.SetActive(false);
            icon.SetActive(true);
            lockIcon.SetActive(false);
        }else{
            nameText.text = "??????";
            nameText.color = color;
            iconHolder.SetActive(true);            
            hint.SetActive(false);
            icon.SetActive(false);
            lockIcon.SetActive(true);
        }
    }

    /// <summary>
    /// Displays the hint of the achievement
    /// </summary>
    /// <param name="value">A boolean used to determine whether the hit needs to be enabled or disabled</param>
    public void ShowHint(bool value){
        hint.SetActive(value);
        iconHolder.SetActive(!value);
    }

}
