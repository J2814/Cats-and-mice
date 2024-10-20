using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{ 
    public Transform[] PathElements;

    public enum ConnectionTypeEnum
    {
        EndToStart,
        StartToEnd,
        StartToStart,
        EndToEnd
    }
    [Serializable]
    public class Connection
    {
        public ConnectionTypeEnum ConnectionType;
        public MovementPath path;

    }
    public List<Connection> Connections = new List<Connection>();

    public void OnDrawGizmos()        
    {
        if (PathElements == null || PathElements.Length < 2) return;

        for (int i = 1; i < PathElements.Length; i++)  
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); 
        }
    }
}



