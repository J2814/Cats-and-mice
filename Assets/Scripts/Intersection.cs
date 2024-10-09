using UnityEngine;

public class Intersection : MonoBehaviour
{
    public MovementPath[] availablePaths; 
    private int currentPathIndex;
    public KeyCode switchKey;
    
    public MovementPath[] GetAvailablePaths()
    {
        return availablePaths;
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            currentPathIndex++;

            if (currentPathIndex >= availablePaths.Length - 1) 
            {
                currentPathIndex = 0;
            }
        }
    }

    public Transform CurrentPath()
    {
        return availablePaths[currentPathIndex].transform;
    }

    public void OnUnitEnter(Unit unit, MovementPath incomingPath, Transform currentPoint)
    {
       
    }
}
