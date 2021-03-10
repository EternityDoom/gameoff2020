using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectChoice : MonoBehaviour
{
    //The text that displays with a specific project
    public Text text;
    
    //The image that displays with a specific project
    public Image image;
    
    //The image that shows if a project is selected
    public Image selectedImage;
    
    //The list of all level markers available to an object
    public List<GameObject> levelMarkers;
    
    //Aditional information that shows for a project
    public GameObject additionnalInfo;
    
    //The time for the project
    public Image time;
    
    //The tooltip that displays when the project is hovered over
    public Tooltip tooltip;
    
    //The specific project linked to the choice
    public Project project;
    
    /// <summary>
    /// Initializes the project 
    /// </summary>
    /// <param name="_project">The project that needs initialized</param>
    /// <param name="spot">The building spot the object should be placed on</param>
    public void Init(Project _project, BuildingSpot spot){
        project = _project;
        text.text = project.projectName;
        image.sprite = project.sprite;
        selectedImage.enabled = spot.currentProject == project;
        tooltip.tip = project.effectDescription;
        foreach (GameObject marker in levelMarkers)
        {
            marker.SetActive(false);
        }
        additionnalInfo.SetActive(!GM.I.project.IsConstant(project));
        if(GM.I.project.IsConstant(project)){
            time.fillAmount = 0f;
        }else{
            for (var i = 1; i <= GM.I.project.GetLevel(project); i++)
            {
                if(levelMarkers.Count > i-1){
                    levelMarkers[i-1].SetActive(true);
                }
            }
            time.fillAmount = (float)GM.I.project.GetTime(project)/(float)GM.I.project.GetLength(project);
        }
    }

    /// <summary>
    /// Updates the building menu to show which project is selected
    /// </summary>
    public void Clic(){
        GM.I.ui.buildingInformation.SelectProject(project);
    }
}
