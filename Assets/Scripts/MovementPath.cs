using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{ 
     
    public Transform[] PathElements;

    public Transform ForwardPathStartPoint;
    public Transform BackwardPathStartPoint;

    public void OnDrawGizmos()        
    {
        if (PathElements == null || PathElements.Length < 2) return;

        for (int i = 1; i < PathElements.Length; i++)  
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); 
        }
    }
   
}



