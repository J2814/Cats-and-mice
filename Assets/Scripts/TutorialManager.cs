using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public IntersectionText tutorTxt;

    public Text[] tutorialTexts; 
    public Text enterText;      
    private int step = 0;


    void Start()
    {
        for (int i = 1; i < tutorialTexts.Length; i++)
        {
            tutorialTexts[i].gameObject.SetActive(false);
        }

        tutorialTexts[0].gameObject.SetActive(true);
        enterText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            ShowNextStep();
        }
    }


    private void ShowNextStep()
    {
        tutorialTexts[step].gameObject.SetActive(false);

        step++;

        if (step == 4)
        {
            tutorialTexts[step].gameObject.SetActive(true);
            StartCoroutine(DelayAndPunchAnim(1f));  
        }
        else if(step < tutorialTexts.Length)
        {
            tutorialTexts[step].gameObject.SetActive(true);
        }
        else
        {
            enterText.gameObject.SetActive(false);
            ResumeGame();
        }
    }

    private IEnumerator DelayAndPunchAnim(float delay)
    {
        yield return new WaitForSeconds(delay);  
        tutorTxt.PunchAnim();  
    }

    private void ResumeGame()
    {
        Debug.Log("Игра началась!");
    }
}
