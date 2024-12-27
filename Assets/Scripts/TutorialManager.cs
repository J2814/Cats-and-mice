using DG.Tweening;
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
            AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);

            enterText.gameObject.transform.DOScale(new Vector3(3, 3, 3), 0);
            enterText.gameObject.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f);

        }
    }


    private void ShowNextStep()
    {
        if (tutorialTexts.Length > step)
        {
            tutorialTexts[step].gameObject.SetActive(false);
        }
        

        step++;

        if (step == 6)
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
        tutorTxt.PunchAnim();
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.IntersectionSwitch);
        yield return new WaitForSeconds(delay);  
        
        if(step == 6)
            StartCoroutine(DelayAndPunchAnim(1f));
    }

    private void ResumeGame()
    {
        Debug.Log("Игра началась!");
    }
}
