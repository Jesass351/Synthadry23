using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory) {
        _isRootState = true;
        InitSubState();
    }
    public override void EnterState()
    {
        _context._currentVelocity.y = Mathf.Sqrt(_context.jumpForce * -3.5f * _context.gravity);
        _context.Animator.SetTrigger("Jump");
        Debug.Log("PState: enter Jump");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        // _context.CharacterController.Move(Vector3.up * _context.jumpForce * Time.fixedDeltaTime);
    }
    public override void FixedUpdateState()
    {
        _context._currentVelocity.y += _context.gravity * Time.deltaTime;
        _context.CharacterController.Move(_context._currentVelocity * Time.deltaTime);
    }
    public override void ExitState()
    {
        _context.IsJumping = false;
        Debug.Log("PState: exit Jump");
    }
    public override void InitSubState() {
        if (!_context.IsMoving && !_context.IsRunning) {
            SetSubState(_stateFactory.Idle());
        }
        else if (_context.IsMoving && !_context.IsRunning) {
            SetSubState(_stateFactory.Walk());
        }
        else {
            SetSubState(_stateFactory.Run());
        }
    }
    public override void CheckSwitchStates()
    {
        if (_context.CharacterController.isGrounded)
        {
            SwitchState(_stateFactory.Grounded());
        }
    }
}