using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceAnimator : MyAnimator
{
    public List<string> DeathAnims;
    public List<string> IdleAnims;

    private bool MovingRight;
    [SerializeField]
    private float MovingFaceOffset = 0.1f;

    private float previousX = 0;

    private void Awake()
    {
        SetUpAnimator();
        
    }

    private void Update()
    {
        DirectionSwitcher();
    }

    private void OnDestroy()
    {
        //DOTween.Clear(this);

        DOTween.Kill(transform.parent.transform);
        DOTween.Kill(transform);
        
    }
    public override void SetUpAnimator()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];

            AnimationEvent animationEndEvent = new AnimationEvent();
            animationEndEvent.time = clip.length;
            animationEndEvent.functionName = "AnimationExit";

            clip.AddEvent(animationEndEvent);
        }
    }

    private void DirectionSwitcher()
    {
        if (previousX < transform.position.x)
        {
            SwitchDirection(true);
            previousX = transform.position.x;
        }
        if (previousX > transform.position.x)
        {
            SwitchDirection(false);
            previousX = transform.position.x;
        }
    }

    private void SwitchDirection(bool right)
    {
        if (right)
        {
            transform.DOLocalMoveX(MovingFaceOffset, 0.3f);
        }
        else
        {
            transform.DOLocalMoveX(-MovingFaceOffset, 0.3f);
            
        }
    }

    int RandElement(List<string> list)
    {
        if (list.Count > 0)
        {
            return Random.Range(0, list.Count - 1);
        }
        else
        {
            return -1;
        }
    }
    public void PlayDeathAnim()
    {
        if (RandElement(DeathAnims) >= 0)
        {
            ChangeAnimationState(DeathAnims[RandElement(DeathAnims)]);
        }

        Vector3 scalePunch = new Vector3(transform.localScale.x * 1.5f, transform.localScale.x * 1.5f, transform.localScale.x * 1.5f);

        transform.parent.transform.DOScale(scalePunch, 0.15f);
    }

    public void PlayIdleAnim()
    {
        if (RandElement(IdleAnims) >= 0)
        {
            ChangeAnimationState(IdleAnims[RandElement(IdleAnims)]);
        }
    }

    public void AnimationExit()
    {
        PlayIdleAnim();
    }
}
