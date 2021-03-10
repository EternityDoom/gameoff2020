using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    //The text field that displays the current date of the game
    public Text clock;

    //The image that shows up 'behind' the clock
    public Image clockBar;

    //The line that surrounds the outer edge of the game
    public Image border;

    //The bar at the top of the game, it is a line with a 'moon' used to display time remaining until the user wins the game
    public Slider travelIndicator;

    //The playand pause button game objects
    public GameObject playButton, pauseButton;

    //The toggle that is attatched to all of the buttons regarding the time keeper
    public Toggle pauseToggle, playToggle, fastToggle, superFastToggle;

    //
    public Tooltip travelTip;

    /// <summary>
    /// Used to update the clock that is displayed in the UI
    /// </summary>
    /// <param name="time">The current time that the game has progressed.</param>
    /// <param name="smoothTime">The time progressed within the current month, divided by the current speed.</param>
    public void UpdateClock(int time, float smoothTime){
        int month = time % 12;
        int year = (time-month)/12;
        
        clock.text = UIManager.TimeToDate(time);
        clockBar.fillAmount = (smoothTime + (float)month)/12f;

        travelIndicator.value = (float)time/(float)GM.I.gameplay.travelLenght;
        travelTip.tip = GM.I.gameplay.travelLenght-time + " months left";
    }

    /// <summary>
    /// Is called when the PauseButton is clicked.
    /// </summary>
    /// <param name="value">The default value for on click for Toggle</param>
    public void PauseButton(bool value){
        if(value){
            GM.I.gameplay.PauseTime(true);
            GM.I.sfx.Play(SFX.Pause);
        }
    }

    /// <summary>
    /// Is called when the PlayButton is clicked.
    /// </summary>
    /// <param name="value">The default value for on click for Toggle</param>
    public void PlayButton(bool value){
        if(value){
            GM.I.gameplay.PauseTime(false);
            GM.I.sfx.Play(SFX.Play);
        }
    }

    /// <summary>
    /// Is called when the FastButton is clicked.
    /// </summary>
    /// <param name="value">The default value for on click for Toggle</param>
    public void FastButton(bool value){
        if(value){
            GM.I.gameplay.FastTime();
            GM.I.sfx.Play(SFX.PlayFast);
        }
    }

    /// <summary>
    /// Is called when the SuperFastButton is clicked.
    /// </summary>
    /// <param name="value">The default value for on click for Toggle</param>
    public void SuperFastButton(bool value){
        if (value){
            GM.I.gameplay.SuperFastTime();
            GM.I.sfx.Play(SFX.PlaySuperFast);
        }
    }

    /// <summary>
    /// Updates the color displayed on the buttons to match with whichever button is toggled, then disables the toggle on all buttons aside from the currently toggled one.
    /// </summary>
    public void UpdatePausedStatus(){
        if(GM.I.gameplay.timePaused){
            clock.color = GM.I.art.red;
            border.color = GM.I.art.red;
            clockBar.color = GM.I.art.red;
            pauseToggle.SetIsOnWithoutNotify(true);
            playToggle.SetIsOnWithoutNotify(false);
            fastToggle.SetIsOnWithoutNotify(false);
            superFastToggle.SetIsOnWithoutNotify(false);
        }
        else if (GM.I.gameplay.currentSpeed == GM.I.gameplay.timeSpeed){
            clock.color = GM.I.art.light;
            border.color = GM.I.art.light;
            clockBar.color = GM.I.art.light;
            pauseToggle.SetIsOnWithoutNotify(false);
            playToggle.SetIsOnWithoutNotify(true);
            fastToggle.SetIsOnWithoutNotify(false);
            superFastToggle.SetIsOnWithoutNotify(false);
        }
        else if (GM.I.gameplay.currentSpeed == GM.I.gameplay.fastTimeSpeed){
            clock.color = GM.I.art.greenLight;
            border.color = GM.I.art.greenLight;
            clockBar.color = GM.I.art.greenLight;
            pauseToggle.SetIsOnWithoutNotify(false);
            playToggle.SetIsOnWithoutNotify(false);
            fastToggle.SetIsOnWithoutNotify(true);
            superFastToggle.SetIsOnWithoutNotify(false);
        }
        else if (GM.I.gameplay.currentSpeed == GM.I.gameplay.superFastTimeSpeed){
            clock.color = GM.I.art.otherBlueLight;
            border.color = GM.I.art.otherBlueLight;
            clockBar.color = GM.I.art.otherBlueLight;
            pauseToggle.SetIsOnWithoutNotify(false);
            playToggle.SetIsOnWithoutNotify(false);
            fastToggle.SetIsOnWithoutNotify(false);
            superFastToggle.SetIsOnWithoutNotify(true);
        }
    }

    
}
