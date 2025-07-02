using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour {
    [SerializeField] private Animator animator;

    private int _isRunningHash;
    private int _defeatedHash;
    private int _jumpedHash;
    private int _isSpinningHash;
    private int _startSpinHash;

    private bool _isSpinning;
    private bool _isRunning;
    private bool _isJumping;

    private int currentBuffAnimations = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _isRunningHash = Animator.StringToHash("isRunning");
        _defeatedHash = Animator.StringToHash("Defeated");
        _jumpedHash = Animator.StringToHash("isJumping");
        _isSpinningHash = Animator.StringToHash("isSpinning");
        _startSpinHash = Animator.StringToHash("StartSpin");
    }

    public void ActivateRunAnimation()
    {
        if (!_isRunning)
        {
            animator.SetBool(_isRunningHash, true);
            _isRunning = animator.GetBool(_isRunningHash);
        }

    }

    public void DeactivateRunAnimation()
    {
        if (_isRunning)
        {
            animator.SetBool(_isRunningHash, false);
            _isRunning = animator.GetBool(_isRunningHash);
        }
    }

    public void ActivateJUmpAnimation()
    {
        if (!_isRunning && !_isSpinning)
        {
            animator.SetBool(_jumpedHash, true);
            _isJumping = animator.GetBool(_jumpedHash);
        }
    }

    public void DeactivateJumpAnimation()
    {
        if (_isJumping)
        {
            animator.SetBool(_jumpedHash, false);
            _isJumping = animator.GetBool(_jumpedHash);
        }
    }


    public void ActivateSpinningAnimation(float timerTime)
    {

        StartCoroutine(HandleBuffAnimation(_isSpinningHash, timerTime, () =>
        {
            _isSpinning = false;
        }));
        _isSpinning = true;

    }

    public void DeactivateSpinningAnimation()
    {
        animator.SetBool(_isSpinningHash, false);
        _isSpinning = false;
    }

    private IEnumerator HandleBuffAnimation(int animationHash, float timerTime, Action OnAnimationStoped)
    {
        currentBuffAnimations++;
        animator.SetTrigger(_startSpinHash);
        animator.SetBool(animationHash, true);
        yield return new WaitForSeconds(timerTime);
        currentBuffAnimations--;
        if (currentBuffAnimations == 0)
        {
            animator.SetBool(animationHash, false);
            OnAnimationStoped?.Invoke();
        }

    }

    public void ActivateDefeatAnimation()
    {
        animator.SetTrigger(_defeatedHash);
    }
}
