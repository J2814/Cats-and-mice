using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public Sprite OpenCage;
    public Sprite ClosedCage;
    private SpriteRenderer spriteRenderer;

    private bool currentlyClosed;

    private Coroutine closedRoutine;

    private float normalScale;

    void Start()
    {
        

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        normalScale = transform.transform.localScale.x;
    }


    public void Close()
    {
        Open();
        StartCoroutine(CloseDelay());
    }


    IEnumerator CloseDelay()
    {
        yield return new WaitForSeconds(0.05f);

        spriteRenderer.sprite = ClosedCage;
        transform.DOScale(new Vector3(normalScale, normalScale, normalScale), 0);
        transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.15f);

        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.CageClose);

        closedRoutine = StartCoroutine(ClosedWait());
    }

    IEnumerator ClosedWait()
    {
        yield return new WaitForSeconds(0.65f);

        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.CageOpen);
        Open();

    }

    private void Open()
    {
        if (closedRoutine != null) 
        {
            StopCoroutine(closedRoutine);
        }
        spriteRenderer.sprite = OpenCage;
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.15f);
    }
}
