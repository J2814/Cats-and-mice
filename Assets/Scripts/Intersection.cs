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
            SwitchCurrentPath();
        }
    }

    private void SwitchCurrentPath()
    {
        currentPathIndex++;

        if (currentPathIndex >= availablePaths.Length - 1)
        {
            currentPathIndex = 0;
        }

        for (int i = 0; i < availablePaths.Length; i++) 
        {
            if (i == currentPathIndex)
            {
                availablePaths[i].gameObject.SetActive(true);
            }
            else
            {
                availablePaths[i].gameObject.SetActive(false);
            }
            
        }
    }


}
