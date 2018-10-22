﻿using UnityEngine;

/// <summary>   [Script information | Delete on Final Build]
/// 
/// Purpose:    Handle Animator related information.
/// Use:        Attach this component to a prefab to use as a player model.
///             
/// Status:     Has basic minimum viable product functions.
/// Bugs:       If you spot any issues or bugs, please list them below, I'll look into them asap:
///             
/// Notes:    
/// 
/// </summary>

[System.Serializable]
public class AnimationHelper
{
    #region Public Variables
    [Space()]
    [Header("Animator Prefabs")]
    public RuntimeAnimatorController controller;
    public Avatar avatar;

    [Space()]
    [Header("Animation Speed")]
    public float maxRunSpeed;
    public float maxWalkSpeed;
    public float maxCrouchSpeed;
    public float animationMultiplier = 1f;

    [Space()]
    [Header("Etc")]
    public float runAnimOffset = 0.2f;
    #endregion

    #region Private Variables
    private ThirdPersonController _playerController;
    private Animator _animator;
    #endregion


    #region Setup & Update Functions
    public void SetAnimator(ThirdPersonController pController, Animator anim)
    {
        if (!anim)
            throw new System.ArgumentException("Given Animator is invalid (NULL). Please check that given Model has an Animator", "anim");

        _animator = anim;

        _animator.avatar = avatar;
        _animator.runtimeAnimatorController = controller;
        _animator.Rebind();
        _animator.applyRootMotion = false;

        _playerController = pController;
    }

    /// <summary> Ensures the proper animations line up with player movement. </summary>
    public void HandleAnimator()
    {
        if (!_animator.gameObject.activeSelf) return;

        Vector3 velocity = _playerController.GetVelocity();
        float speed = (_playerController.State.IsCrouching) ? maxCrouchSpeed : ((_playerController.State.IsWalking) ? maxWalkSpeed : maxRunSpeed);

        _animator.SetFloat("Forward", (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z)) / speed, 0.1f, Time.fixedDeltaTime);
        _animator.SetBool("Crouch", _playerController.State.IsCrouching);
        _animator.SetBool("OnGround", _playerController.IsNearGround);
        if (_playerController.State.IsJumping)
            _animator.SetFloat("Jump", velocity.y);

        _animator.speed = (_playerController.IsNearGround && velocity.magnitude > 0) ? animationMultiplier : 1;
    }

    public void SetInCombat(bool value)
    {
        _animator.SetBool("InCombat", value);
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger("AttackTrigger");
    }

    //public void SetAttackIndex(int index)
    //{
    //    _animator.SetInteger("AttackIndex", index);
    //}

    //public bool AnimationComplete()
    //{
    //    //return (_animator.GetCurrentAnimatorStateInfo(1).IsName("Punch") && _animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 1 /*&& !_animator.IsInTransition(1)*/ );
    //    return _animator.GetCurrentAnimatorStateInfo(1).IsName("Default");
    //}

    //public void SetState(string stateName)
    //{
    //    _animator.Play(stateName, 1);
    //}

    //public bool IsPlayingAttackAnimation()
    //{
    //    return _animator.GetCurrentAnimatorStateInfo(1).IsName("Punch");
    //}
    #endregion
}