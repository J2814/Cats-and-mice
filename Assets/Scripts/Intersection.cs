using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Intersection : MonoBehaviour
{
    public IntersectionPath[] availablePaths;
    [SerializeField]
    private int currentPathIndex = 0;
    public KeyCode switchKey;

    private IntersectionText KeyText;

    public MovementPath[] GetAvailablePaths()
    {
        return availablePaths;
    }
    private void Start()
    {
        StartCoroutine(EnableCurrentPathDelay());

        KeyText = GetComponentInChildren<IntersectionText>();
        if (KeyText != null) 
        {
            KeyText.SetText(switchKey.ToString());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCurrentPath();

            if (KeyText != null)
            {
                KeyText.PunchAnim();
            }

            availablePaths[currentPathIndex].GetComponent<PathVisualisation>().PunchWidth();
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
                availablePaths[i].DisconnectFromSelf();
            }
        }
    }

    /// <summary>
    /// something was breaking before i did this. After waiting for a couple frames problem goes away.
    /// I don't know why and actually don't care to know
    /// </summary>
    /// <returns></returns>
    IEnumerator EnableCurrentPathDelay()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        EnableCurrentPath();
    }
}
