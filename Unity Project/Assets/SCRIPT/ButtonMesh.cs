using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonMesh : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject normal;
    public GameObject highlight;
    public GameObject click;

    public UnityEvent onClic;
    public UnityEvent onEnter;
    public UnityEvent onExit;
    
    /// <summary>
    /// Called when the button is enabled
    /// </summary>
    private void OnEnable() {
        if(highlight != null){
            highlight.SetActive(false);
        }
        if(normal != null){
            normal.SetActive(true);
        }
        if(click != null){
            click.SetActive(false);
        }
    }

    /// <summary>
    /// When the mouse enters the range of the button
    /// </summary>
    private void OnMouseEnter() {
        if(eventSystem.IsPointerOverGameObject()){return;}
        if(GM.I.gameplay.currentTime < GM.I.gameplay.travelLenght){
            onEnter.Invoke();
            if(highlight != null){
                highlight.SetActive(true);
            }
            if(normal != null){
                normal.SetActive(false);
            }
            if(click != null){
                click.SetActive(false);
            }
        }
    }

    /// <summary>
    /// When the mouse exits the range of the button
    /// </summary>
    private void OnMouseExit() {
        onExit.Invoke();
        if(highlight != null){
            highlight.SetActive(false);
        }
        if(normal != null){
            normal.SetActive(true);
        }
        if(click != null){
            click.SetActive(false);
        }
    }

    /// <summary>
    /// When the mouse is clicked on the button
    /// </summary>
    private void OnMouseDown() {
        if(eventSystem.IsPointerOverGameObject()){return;}
        if(GM.I.gameplay.currentTime < GM.I.gameplay.travelLenght){
            onClic.Invoke();
            if(highlight != null){
                highlight.SetActive(false);
            }
            if(normal != null){
                normal.SetActive(false);
            }
            if(click != null){
                click.SetActive(true);
            }
        }
    }

}
