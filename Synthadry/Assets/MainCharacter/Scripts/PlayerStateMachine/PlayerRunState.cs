using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory){}
    public override void EnterState()
    {
        _context.Animator.SetBool("IsRunning", true);
        Debug.Log("PState: enter Run");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        _context.animationInterpolation = Mathf.Lerp(_context.animationInterpolation, 1.5f, Time.deltaTime * 3);
        _context.Animator.SetFloat("x", _context.Input.GetCurrentMovement().x * _context.animationInterpolation);
        _context.Animator.SetFloat("y", _context.Input.GetCurrentMovement().y * _context.animationInterpolation);

        _context._appliedMovement = new Vector2(_context.Input.GetCurrentMovement().x * 1.5f, _context.Input.GetCurrentMovement().y * 1.5f);
    }
    public override void FixedUpdateState()
    {
        Vector3 movingVector = new Vector3(_context.Input.GetCurrentMovement().x, 0, _context.Input.GetCurrentMovement().y);
        _context._currentSpeed = Mathf.Lerp(_context._currentSpeed, _context.runningSpeed, Time.deltaTime * 3);
        _context.CharacterController.Move(_context.transform.TransformDirection(movingVector) * _context._currentSpeed * Time.deltaTime);
        _context._currentVelocity.y += _context.gravity * Time.deltaTime;
        if (_context.CharacterController.isGrounded) {
            _context._currentVelocity.y = -2f;
        }
        _context.CharacterController.Move(_context._currentVelocity * Time.deltaTime);
        _context.Animator.SetFloat("magnitude", movingVector.magnitude / _context._currentSpeed);
    }
    public override void ExitState()
    {
        _context.Animator.SetBool("RifleRunning", false);
        _context.Animator.SetBool("IsRunning", false);
        Debug.Log("PState: exit Run");
    }
    public override void InitSubState() {

    }
    public override void CheckSwitchStates()
    {
        if (!_context.IsMoving) {
            SwitchState(_stateFactory.Idle());
        }
        if (_context.IsMoving && !_context.IsRunning) {
            SwitchState(_stateFactory.Walk());
        }
    }
}