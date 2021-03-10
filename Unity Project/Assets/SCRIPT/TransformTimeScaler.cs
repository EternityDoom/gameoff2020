using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTimeScaler : MonoBehaviour
{
    //The initial scale that the imploded sun in the background has
    Vector3 startScale;
    
    //The amount that the scale of the imploded sun should change
    public float scaleFactor;
    
    //Will be true if the background is inverted
    public bool inverted = false;
    
    /// <summary>
    /// Is called initially, and sets the scale, as well as the inversion if there is inversion of the imploded sun
    /// </summary>
    private void Start() {
        startScale = transform.localScale;
        if(inverted){
            transform.localScale = Vector3.zero;
        }
    }
      
    /// <summary>
    /// Every frame the scale of the imploded sun is shrunk based on the scale factor and the current amount of time that the game has progressed
    /// </summary>
    void Update()
    {
        if(inverted){
            transform.localScale = startScale * Mathf.Clamp01(-scaleFactor+ 1f + scaleFactor * ((float)GM.I.gameplay.currentTime + GM.I.gameplay.monthTime)/(float)GM.I.gameplay.travelLenght);
        }else{
            transform.localScale = startScale * Mathf.Clamp01(1f+scaleFactor * ((float)GM.I.gameplay.currentTime + GM.I.gameplay.monthTime)/(float)GM.I.gameplay.travelLenght);
        }
    }
}
