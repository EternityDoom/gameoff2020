using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameplayManager : MonoBehaviour
{
    [Header("TIME")]
    public int currentTime = 0;
    public float timeSpeed;
    public float fastTimeSpeed;
    public float superFastTimeSpeed;
    public float currentSpeed;
    public float monthTime;
    public bool timePaused = false;

    [Header("TRAVEL")]
    public int travelLenght;
    public float timer = 0;

    public MoonRotator rotator;

    private void Start() {
        UpdateTime(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(false);
        
    }

    /// <summary>
    /// Updates the time and processes all events that happen when time changes
    /// </summary>
    /// <param name="force">If true then the process is forced to go through regardless of if time is paused</param>
    public void UpdateTime(bool force){
        timer += Time.deltaTime;
        if(!timePaused){
            monthTime += Time.deltaTime;
            if(monthTime >= currentSpeed){
                currentTime++;
                monthTime = 0;
                GM.I.people.ProcessAging();
                GM.I.city.UpdateCity();
                GM.I.resource.UpdateResources();
                GM.I.people.ProcessMood();
                GM.I.project.UpdateProjects();
                GM.I.eventManager.UpdateEvents();
                GM.I.ui.buildingInformation.UpdateMenuInfo();
                CheckLooseConditions();
                CheckWinConditions();
                // Decade Analytics
                // if(monthTime % 120 == 0){
                //     Analytics.CustomEvent("DecadeGame", new Dictionary<string, object>
                //     {
                //         { "day", currentTime },
                //         { "population", GM.I.people.TotalPopulation },
                //         { "needs", GM.I.people.needs },
                //         { "comfort", GM.I.people.comfort },
                //         { "culture", GM.I.people.culture },
                //         { "hope", GM.I.people.hope },
                //         { "unemployement", GM.I.people.Unemployement },
                //         { "energy", GM.I.resource.resources.Energy },
                //         { "water", GM.I.resource.resources.Water },
                //         { "material", GM.I.resource.resources.Material }
                //     });
                // }
            }
            GM.I.ui.timeKeeper.UpdateClock(currentTime, monthTime/currentSpeed);
        }
        if(force){
            GM.I.people.ProcessAging();
            GM.I.city.UpdateCity();
            GM.I.people.ProcessMood();
            GM.I.people.ProcessAging();
            GM.I.city.UpdateCity();
            GM.I.ui.resourceMeters.UpdateResources();
            GM.I.people.ProcessMood();
            GM.I.ui.timeKeeper.UpdateClock(currentTime, monthTime/currentSpeed);
        }
    }

    /// <summary>
    /// Determines if the timer needs paused or should play at normal speed
    /// </summary>
    /// <param name="shouldStop">If true then pauses, otherwise plays at normal speed</param>
    public void PauseTime(bool shouldStop){
        timePaused = shouldStop;
        currentSpeed = timeSpeed;
        GM.I.ui.timeKeeper.UpdatePausedStatus();
    }

    /// <summary>
    /// Processes the game at fast time speed
    /// </summary>
    public void FastTime(){
        PauseTime(false);
        currentSpeed = fastTimeSpeed;
        GM.I.ui.timeKeeper.UpdatePausedStatus();
    }

    /// <summary>
    /// Processes the game at super fast time speed
    /// </summary>
    public void SuperFastTime(){
        PauseTime(false);
        currentSpeed = superFastTimeSpeed;
        GM.I.ui.timeKeeper.UpdatePausedStatus();
    }

    /// <summary>
    /// Checks if the game has met the win requirements
    /// </summary>
    void CheckWinConditions(){
        if(currentTime >= travelLenght){
            rotator.interactable = false;
            rotator.target.eulerAngles = Vector3.zero;
            GM.I.ui.ShowWinScreen();
            GM.I.introManager.myAnimator.enabled = true;
            GM.I.introManager.myAnimator.Play("Rocket");
            timePaused = true;

            PlayerPrefs.SetInt("NewHome", 1);
            if(GM.I.people.TotalPopulation >= 1000000){
                if(PlayerPrefs.GetInt("BabyBoom",0) == 0){
                    Analytics.CustomEvent("BabyBoom");
                }
                PlayerPrefs.SetInt("BabyBoom", 1);
            }
            if(timer < 60f*45f){
                if(PlayerPrefs.GetInt("LightSpeed",0) == 0){
                    Analytics.CustomEvent("LightSpeed");
                }
                PlayerPrefs.SetInt("LightSpeed", 1);
            }
            if(GM.I.people.hope <= 0.25f){
                if(PlayerPrefs.GetInt("Hopeless",0) == 0){
                    Analytics.CustomEvent("Hopeless");
                }
                PlayerPrefs.SetInt("Hopeless", 1);
            }
            if(GM.I.people.holiday){
                if(PlayerPrefs.GetInt("Holiday",0) == 0){
                    Analytics.CustomEvent("Holiday");
                }
                PlayerPrefs.SetInt("Holiday", 1);
            }
            int terraIncognitCount = 0;
            foreach (BuildingSpot spot in GM.I.city.buildings)
            {
                if(spot.terraIncognita){terraIncognitCount++;}
            }
            if(terraIncognitCount == 0){
                PlayerPrefs.SetInt("TerraIncognita", 1);
            }

            Analytics.CustomEvent("WinGame", new Dictionary<string, object>
            {
                { "day", currentTime },
                { "population", GM.I.people.TotalPopulation },
                { "needs", GM.I.people.needs },
                { "comfort", GM.I.people.comfort },
                { "culture", GM.I.people.culture },
                { "hope", GM.I.people.hope },
                { "unemployement", GM.I.people.Unemployement },
                { "energy", GM.I.resource.resources.Energy },
                { "water", GM.I.resource.resources.Water },
                { "material", GM.I.resource.resources.Material }
            });
        }
    }

    /// <summary>
    /// Checks if the game has met the lose requirements
    /// </summary>
    void CheckLooseConditions(){
        if(GM.I.people.TotalPopulation < 1){
            GM.I.ui.ShowLooseScreen();
            timePaused = true;
            Analytics.CustomEvent("LooseGame", new Dictionary<string, object>
            {
                { "day", currentTime },
                { "population", GM.I.people.TotalPopulation },
                { "needs", GM.I.people.needs },
                { "comfort", GM.I.people.comfort },
                { "culture", GM.I.people.culture },
                { "hope", GM.I.people.hope },
                { "unemployement", GM.I.people.Unemployement },
                { "energy", GM.I.resource.resources.Energy },
                { "water", GM.I.resource.resources.Water },
                { "material", GM.I.resource.resources.Material }
            });
        }
    }

    /// <summary>
    /// Restarts the game from the start as if the user just pressed the Play Button
    /// </summary>
    public void Restart(){
        Analytics.CustomEvent("RestartGame", new Dictionary<string, object>
        {
            { "day", currentTime },
            { "population", GM.I.people.TotalPopulation },
            { "needs", GM.I.people.needs },
            { "comfort", GM.I.people.comfort },
            { "culture", GM.I.people.culture },
            { "hope", GM.I.people.hope },
            { "unemployement", GM.I.people.Unemployement },
            { "energy", GM.I.resource.resources.Energy },
            { "water", GM.I.resource.resources.Water },
            { "material", GM.I.resource.resources.Material }
        });
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Returns the player to the main menu
    /// </summary>
    public void GoToMenu(){
        Analytics.CustomEvent("RestartGame", new Dictionary<string, object>
        {
            { "day", currentTime },
            { "population", GM.I.people.TotalPopulation },
            { "needs", GM.I.people.needs },
            { "comfort", GM.I.people.comfort },
            { "culture", GM.I.people.culture },
            { "hope", GM.I.people.hope },
            { "unemployement", GM.I.people.Unemployement },
            { "energy", GM.I.resource.resources.Energy },
            { "water", GM.I.resource.resources.Water },
            { "material", GM.I.resource.resources.Material }
        });
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
