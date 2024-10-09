using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{ 
     
    public Transform[] PathElements;

    public MovementPath ForwardConnectedPath;
    public MovementPath BackwardConnectedPath;

    public void OnDrawGizmos()        
    {
        if (PathElements == null || PathElements.Length < 2) return;

        for (int i = 1; i < PathElements.Length; i++)  
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); 
        }
    }
   
    public void ForceConnectionToSelf()
    {
        ForwardConnectedPath.BackwardConnectedPath = this;
        BackwardConnectedPath.ForwardConnectedPath = this;
    }

}



