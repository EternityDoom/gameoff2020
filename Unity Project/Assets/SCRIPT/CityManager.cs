using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public GameObject standardMoon, holoMoon;
    public List<BuildingSpot> buildings;
    public List<Connection> connections;

    /// <summary>
    /// Instantiates the building spots and connections on game start
    /// </summary>
    private void Awake() {
        foreach (BuildingSpot spot in buildings)
        {
            spot.connections.Clear();
        }
        foreach (Connection item in connections)
        {
            item.spotStart.connections.Add(item);
            item.spotEnd.connections.Add(item);
        }
    }

    /// <summary>
    /// Returns the number of building spots that are currently housing spaces
    /// </summary>
    /// <returns></returns>
    public int HousingSpace(){
        int space = 0;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && building.Built && building.currentBuilding.housing){
                space += building.currentBuilding.populationRequirement;
            }
        }
        return space;
    }

    /// <summary>
    /// Returns the number of building spots that are currently culture spaces
    /// </summary>
    /// <returns></returns>
    public int Culture(){
        int space = 2;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && building.Built && building.currentBuilding.research){
                space ++;
            }
        }
        return space;
    }

    /// <summary>
    /// Returns the number of building spots that are currently housing spaces
    /// </summary>
    /// <returns></returns>
    public int Housing(){
        int housing = 0;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && building.Built && building.currentBuilding.housing){
                housing ++;
            }
        }
        return housing;
    }

    /// <summary>
    /// Returns the number of building spots that are currently workplace spaces
    /// </summary>
    /// <returns></returns>
    public int WorkplaceSpace(){
        int space = 0;
        foreach (BuildingSpot b in buildings)
        {
            if(b.currentBuilding != null && b.Built && 
            !b.currentBuilding.housing && !(b.currentBuilding.productor && (!b.producing || b.maintenance)) && 
            !(b.currentBuilding.research && (b.currentProject == null || b.maintenance)))
            {
                space += b.currentBuilding.populationRequirement;
            }
        }
        return space;
    }

    /// <summary>
    /// Returns the number of building spots that are currently workplace spaces
    /// </summary>
    /// <returns></returns>
    public int Workplace(){
        int workplace = 0;
        foreach (BuildingSpot b in buildings)
        {
            if(b.currentBuilding != null && b.Built && 
            !b.currentBuilding.housing && !(b.currentBuilding.productor && (!b.producing || b.maintenance)) && 
            !(b.currentBuilding.research && (b.currentProject == null || b.maintenance))){
                workplace ++;
            }
        }
        return workplace;
    }

    /// <summary>
    /// Returns the number of building spots that are currently storage spaces
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float Storage(int type){
        float storage = 0;
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding != null && ((building.Built && !building.currentBuilding.housing && building.currentBuilding.ressourceType == type)||building.currentBuilding.control)){
                storage += building.storage;
            }
        }
        return storage;
    }

    /// <summary>
    /// Returns whether the resources have a shortage or not
    /// </summary>
    /// <returns></returns>
    public bool ResourceShortage(){
        return buildings[0].Cost.Limited(GM.I.resource.resources);
    }

    /// <summary>
    /// Updates the building spots to what mode is needed
    /// </summary>
    /// <param name="mode">The mode to set the spots to</param>
    public void SetBuildingSpotMode(BuildingSpotMode mode){
        foreach (BuildingSpot building in buildings)
        {
            building.mode = mode;
            building.UpdateVisual();
        }
    }

    /// <summary>
    /// Unselects all of the building spots
    /// </summary>
    public void UnselectAll(){
        foreach (BuildingSpot building in buildings)
        {
            building.selected = false;
            building.UpdateVisual();
        }
    }

    /// <summary>
    /// Updates every building in the city
    /// </summary>
    public void UpdateCity(){
        foreach (BuildingSpot building in buildings)
        {
            building.UpdateBuilding();
        }
    }

    /// <summary>
    /// Updates all of the building visuals in the city
    /// </summary>
    public void UpdateCityVisuals(){
        foreach (BuildingSpot building in buildings)
        {
            building.UpdateVisual();
        }
    }

    /// <summary>
    /// Updates the moon mesh
    /// </summary>
    /// <param name="holo">If true then the moon needs to use the holo mesh, otherwise use the non-holo mesh</param>
    public void ShowHoloMoon(bool holo){
        standardMoon.SetActive(!holo);
        holoMoon.SetActive(holo);
    }

    /// <summary>
    /// Updates the integrity of the buildings
    /// </summary>
    /// <param name="amount">The amount to update the building integrity by</param>
    public void ModifyIntegrity(float amount){
        foreach (BuildingSpot building in buildings)
        {
            if(building.currentBuilding == null){
                return;
            }
            if(!building.currentBuilding.control){
                building.integrity = Mathf.Clamp(building.integrity + amount,0,1);
            }
        }
    }
}
