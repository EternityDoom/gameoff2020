using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform target;
    public LineRenderer line;
    public float radius;

    /// <summary>
    /// Called once per frame. Draws a line from one point to a target point
    /// </summary>
    void Update()
    {
        if(target != null){
            Vector3 offset = target.InverseTransformVector(line.GetPosition(1)-target.position + Vector3.down * 1000 + Vector3.right * 1000).normalized;
            offset = new Vector3(offset.x, 0, offset.z) ;
            Vector3 targetPos = target.position + target.TransformVector(offset).normalized* radius;
            line.SetPosition(2,targetPos);
        }else{
            line.SetPosition(2,line.GetPosition(1));
        }
    }
}
