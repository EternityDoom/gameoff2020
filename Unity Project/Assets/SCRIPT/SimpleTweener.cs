using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTweener : MonoBehaviour
{
    //The gradient that will be applied to the object
    public Gradient gradient;

    //The image that the gradient will be applied to
    public Image image;
    
    /// <summary>
    /// Called once per frame, updates the image to be modified according to the gradient based on the current games time
    /// </summary>
    void Update()
    {
        image.color = gradient.Evaluate(Mathf.Sin(Time.time*10f));
    }
}
