using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    //How much the object will rotate
    public float speed;
    

    /// <summary>
    /// Called every frame, the object will rotate based on the speed and the games current delta time
    /// </summary>
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
