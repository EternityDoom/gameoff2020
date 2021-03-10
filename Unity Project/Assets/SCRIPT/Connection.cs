using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public BuildingSpot spotStart, spotEnd;

    /// <summary>
    /// Gets the other building spot
    /// </summary>
    /// <param name="building">The building to find the other of</param>
    /// <returns>Returns the other building spot</returns>
    public BuildingSpot GetOther(BuildingSpot building){
        if(building == spotStart){
            return spotEnd;
        }else{
            return spotStart;
        }
    }
}
