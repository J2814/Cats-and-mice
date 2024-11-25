using System.Collections;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    public IntersectionPath[] availablePaths;
    [SerializeField]
    private int currentPathIndex = 0;
    public KeyCode switchKey;
    
    public MovementPath[] GetAvailablePaths()
    {
        return availablePaths;
    }
    private void Start()
    {
        StartCoroutine(EnableCurrentPathDelay());
        //EnableCurrentPath();
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

        if (currentPathIndex >= availablePaths.Length)
        {
            currentPathIndex = 0;
        }

        EnableCurrentPath();
    }

    private void EnableCurrentPath()
    {
        for (int i = 0; i < availablePaths.Length; i++)
        {
            if (i == currentPathIndex)
            {
                availablePaths[i].gameObject.SetActive(true);
                availablePaths[i].ForceConnectionToSelf();
            }
            else
            {
                availablePaths[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator EnableCurrentPathDelay()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        EnableCurrentPath();
    }
}
