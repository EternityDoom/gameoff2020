﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeters : MonoBehaviour
{
    //The image for the energy, water, and material resources
    public Image energy, water, material;

    //The text value for the energy, water, and material resources
    public Text energyText, waterText, materialText;
    
    //The text value for the change in energy, water, and material resources
    public Text energyDeltaText, waterDeltaText, materialDeltaText;
    
    //The text value for how much resources are being produce
    public Text energyProductionText, waterProductionText, materialProductionText;
    
    //The text value for how much resources are being used
    public Text energyCostText, waterCostText, materialCostText;
    
    //The last amount of resources the user had
    public Resource lastDelta;
    
    //A list of all resource boxes that should be displayed
    public List<RessourceBox> buildingDeltaBoxes;

    /// <summary>
    /// Updates the text values for all of the resources
    /// </summary>
    public void UpdateResources(){
        Resource r = GM.I.resource.resources;
        Resource delta = new Resource();
        delta.Add(r);
        delta.Add(lastDelta.Multiply(-1f));
        energy.fillAmount = r.Energy / GM.I.resource.resourcesLimit.Energy;
        water.fillAmount = r.Water / GM.I.resource.resourcesLimit.Water;
        material.fillAmount = r.Material / GM.I.resource.resourcesLimit.Material;
        energyText.text = ""+ (int)r.Energy;
        waterText.text = ""+ (int)r.Water;
        materialText.text = ""+ (int)r.Material;
        if(r.Energy < lastDelta.Energy){
            energyText.color = GM.I.art.red;
        }else{
            energyText.color = GM.I.art.yellow;
        }
        if(r.Water < lastDelta.Water){
            waterText.color = GM.I.art.red;
        }else{
            waterText.color = GM.I.art.blue;
        }
        if(r.Material < lastDelta.Material){
            materialText.color = GM.I.art.red;
        }else{
            materialText.color = GM.I.art.brown;
        }

        energyDeltaText.text = ""+ (Mathf.Round(delta.Energy*10f))/10f;
        waterDeltaText.text = ""+ (Mathf.Round(delta.Water*10f))/10f;
        materialDeltaText.text = ""+ (Mathf.Round(delta.Material*10f))/10f;

        if(delta.Energy >= 0){
            energyDeltaText.text = "+"+energyDeltaText.text;
            energyDeltaText.color = GM.I.art.yellow;
        }else{
            energyDeltaText.color = GM.I.art.red;
        }
        if(delta.Water >= 0){
            waterDeltaText.text = "+"+waterDeltaText.text;
            waterDeltaText.color = GM.I.art.blue;
        }else{
            waterDeltaText.color = GM.I.art.red;
        }
        if(delta.Material >= 0){
            materialDeltaText.text = "+"+materialDeltaText.text;
            materialDeltaText.color = GM.I.art.brown;
        }else{
            materialDeltaText.color = GM.I.art.red;
        }

        energyProductionText.text = "+"+(Mathf.Round(GM.I.resource.production.Energy*10f))/10f;
        waterProductionText.text = "+"+(Mathf.Round(GM.I.resource.production.Water*10f))/10f;
        materialProductionText.text = "+"+(Mathf.Round(GM.I.resource.production.Material*10f))/10f;

        energyCostText.text = ""+(Mathf.Round(GM.I.resource.cost.Energy*10f))/10f;
        waterCostText.text = ""+(Mathf.Round(GM.I.resource.cost.Water*10f))/10f;
        materialCostText.text = ""+(Mathf.Round(GM.I.resource.cost.Material*10f))/10f;

        lastDelta.r[0] = r.Energy;
        lastDelta.r[1] = r.Water;
        lastDelta.r[2] = r.Material;
        if(GM.I.resource.buildingDeltas.Count > 0){
            buildingDeltaBoxes[0].UpdateRessourceBox(GM.I.resource.buildingDeltas[1]);
            buildingDeltaBoxes[1].UpdateRessourceBox(GM.I.resource.buildingDeltas[2]);
            buildingDeltaBoxes[2].UpdateRessourceBox(GM.I.resource.buildingDeltas[3]);
            buildingDeltaBoxes[3].UpdateRessourceBox(GM.I.resource.buildingDeltas[4].Add(GM.I.resource.buildingDeltas[0]));
            buildingDeltaBoxes[4].UpdateRessourceBox(GM.I.resource.buildingDeltas[5]);
            buildingDeltaBoxes[5].UpdateRessourceBox(GM.I.resource.buildingDeltas[6]);
        }

    }
}
