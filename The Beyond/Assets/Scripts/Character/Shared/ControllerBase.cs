﻿using Beyond.States;
using UnityEngine;


public class ControllerBase : MonoBehaviour
{
    #region Public Variables
    [Header("Movement Speed")]
    public float runSpeed = 8;
    public float walkSpeed = 2;
    public float crouchSpeed = 1;
    public float jumpSpeed = 10;
    public float turnSpeed = 6;

    [Header("Multipliers")]
    public float gravityMultiplier = 2;
    public float crouchMultiplier = 0.55f;

    [Header("Collision Detection")]
    public LayerMask collisionLayers = -1;

    public Vector3 CurrentGravity { get { return Physics.gravity * gravityMultiplier * Time.deltaTime; } }
    public ActionState State { get; protected set; }

    public bool IsNearGround { get; protected set; }
    public bool IsCrouching { get { return State.IsCrouching; } }
    public bool IsJumping { get { return State.IsJumping; } }
    public bool IsWalking { get { return State.IsWalking; } }
    public bool IsIdle { get { return State.IsIdle; } }
    public bool InCombat { get; protected set; }
    //public bool IsMoving { get; protected set; }
    #endregion


    #region Protected Variables


    protected Character character;
    #endregion


    #region Character Setup
    public void Setup(Character newCharacter)
    {
        character = newCharacter;

        State = new ActionState();
        State.SetIdle();
    }
    #endregion


    public void CombatSwitch()
    {
        if (!InCombat)
            character.EnterCombat();

        else
            character.ExitCombat();

        InCombat = !InCombat;
    }

    public void AttackTrigger()
    {
        if (!InCombat)
            character.EnterCombat();

        character.TriggerAttack();
    }
}
