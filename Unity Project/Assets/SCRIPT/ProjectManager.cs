using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    //All projects that will be managed
    public List<Project> projects;
    
    //The levels that each project has
    public List<int> levels;
    
    //The amount of time that each project has until it is completed
    public List<int> time;

    /// <summary>
    /// Gets the amount of time that each project has remaining
    /// </summary>
    /// <param name="project">The project you want to get the time of</param>
    /// <returns>Returns the amount of time remaining on the project</returns>
    public int GetTime(Project project){
        return time[projects.IndexOf(project)];
    }
    
    /// <summary>
    /// Gets the level of a specific project
    /// </summary>
    /// <param name="project">The project you want to get the level of</param>
    /// <returns>Returns the level of a specific project</returns>
    public int GetLevel(Project project){
        return levels[projects.IndexOf(project)];
    }

    /// <summary>
    /// Checks a project to see if it is constantly processing
    /// </summary>
    /// <param name="project">The project that you want to check</param>
    /// <returns>Returns a boolean determining whether the project is constant or not</returns>
    public bool IsConstant(Project project){
        return project.projectLength.x < 0;
    }
    
    /// <summary>
    /// Gets the level of the project and checks if it is maxed
    /// </summary>
    /// <param name="project">The project you are checking</param>
    /// <returns>Returns a boolean stating whether a project is maxed or not</returns>
    public bool IsMaxed(Project project){
        return levels[projects.IndexOf(project)] == 3;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float FX(FXT type){
        foreach (Project p in projects)
        {
            if(p.type == type){
                if(IsConstant(p)){
                    return p.amount[0] * GetLevel(p);
                }else{
                    if(GetLevel(p) == 0){
                        return 1f;
                    }else if(GetLevel(p) == 4){
                        return p.amount[2];
                    }else{
                        return p.amount[GetLevel(p)-1];
                    }
                }
            }
        }
        
        return 1f;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public int GetLength(Project project){
        if(GetLevel(project)<3){
            return project.projectLength[GetLevel(project)];
        }
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateProjects(){
        
        for (int i = 0; i < projects.Count; i++)
        {
            if(IsConstant(projects[i])){
                levels[i] = 0;
            }
        }

        // Process research
        foreach (BuildingSpot spot in GM.I.city.buildings)
        {
            if(spot.currentProject != null){
                int index = projects.IndexOf(spot.currentProject);
                if(!spot.currentProject.monthlyCost.Limited(GM.I.resource.resources)){
                    if(IsConstant(spot.currentProject)){
                        levels[index] += 1;
                    }else{
                        time[index]++;
                    }
                }
            }
        }

        // Process finished research
        for (int i = 0; i < projects.Count; i++)
        {
            if(!IsConstant(projects[i])){
                if(levels[i] < 4){
                    if(GetLength(projects[i]) <= time[i]){
                        levels[i]++;
                        time[i] = 0;
                    }
                }
            }
        }

        // Stop finished projects
        foreach (BuildingSpot spot in GM.I.city.buildings)
        {
            if(spot.currentProject != null){
                int index = projects.IndexOf(spot.currentProject);
                if((time[index] == 0 || levels[index] == 4) && !IsConstant(projects[index])){
                    spot.currentProject = null;
                }
            }
        }

        
    }

    
}
