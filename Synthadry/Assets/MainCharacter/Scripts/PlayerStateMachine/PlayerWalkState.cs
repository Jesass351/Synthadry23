using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory){}
    public override void EnterState()
    {
        Debug.Log("PState: enter Walk");
        _context.Animator.SetBool("IsMoving", true);
    }
    // public override void UpdateState()
    // {
    //     CheckSwitchStates();
    //     _context.animationInterpolation = Mathf.Lerp(_context.animationInterpolation, 1f, Time.deltaTime * 3);
    //     _context._anim.SetFloat("x", _context.horisontal * 0.25f);
    //     _context._anim.SetFloat("y", _context.vertical * 0.25f);
    //     _context.currentSpeed = Mathf.Lerp(_context.currentSpeed, _context.walkingSpeed, Time.deltaTime * 3);
    //     Vector3 camF = _context.mainCamera.forward;
    //     Vector3 camR = _context.mainCamera.right;
    //     camF.y = 0;
    //     camR.y = 0;
    //     // Vector3 moveDirection = cameraForward.normalized * stateMachine.InputReader.MoveComposite.y + cameraRight.normalized * stateMachine.InputReader.MoveComposite.x;
    //     Vector3 movingVector;
    //     movingVector = Vector3.ClampMagnitude(camF.normalized * _context.vertical * _context.currentSpeed + camR.normalized * _context.horisontal * _context.currentSpeed, _context.currentSpeed);

    //     // needs refactoring
    //     if (!_context.CharacterController.isGrounded)
    //     {
    //         movingVector.y -= _context.walkingSpeed * 2;
    //     }

    //     _context._anim.SetFloat("magnitude", movingVector.magnitude / _context.currentSpeed);
    //     _context.CharacterController.Move(movingVector * Time.fixedDeltaTime);
    // }
    public override void UpdateState()
    {
        CheckSwitchStates();
        _context.animationInterpolation = Mathf.Lerp(_context.animationInterpolation, 1f, Time.deltaTime * 3);
        _context.Animator.SetFloat("x", _context.Input.GetCurrentMovement().x * 0.25f);
        _context.Animator.SetFloat("y", _context.Input.GetCurrentMovement().y * 0.25f);

        _context._appliedMovement = new Vector2(_context.Input.GetCurrentMovement().x, _context.Input.GetCurrentMovement().y);
        
        // _context._currentVelocity = _context.rig.velocity;
        // Vector3 targetVelocity = new Vector3(_context.CurrentMovement.x, 0, _context.CurrentMovement.y);
        // targetVelocity *= _context.currentSpeed;
        // targetVelocity = _context.transform.TransformDirection(targetVelocity);

        // _context.currentSpeed = Mathf.Lerp(_context.currentSpeed, _context.walkingSpeed, Time.deltaTime * 3);

        // Vector3 velocityChange = (targetVelocity - _context._currentVelocity);
        // Vector3.ClampMagnitude(velocityChange, _context.currentSpeed);

        // _context.rig.AddForce(velocityChange, ForceMode.VelocityChange);
        
        // Vector3 camF = _context.mainCamera.forward;
        // Vector3 camR = _context.mainCamera.right;
        // camF.y = 0;
        // camR.y = 0;

        // Vector3 movingVector = new Vector3(camR.normalized * _context.CurrentMovement.x, 0, camF.normalized *_context.CurrentMovement.y);
        // Vector3 movingVector = camF.normalized * _context.CurrentMovement.y + camR.normalized * _context.CurrentMovement.x;
        // _context.CharacterController.Move(movingVector * Time.fixedDeltaTime * _context.currentSpeed);
    }
    public override void FixedUpdateState()
    {
        Vector3 movingVector = new Vector3(_context.Input.GetCurrentMovement().x, 0, _context.Input.GetCurrentMovement().y);
        _context._currentSpeed = Mathf.Lerp(_context._currentSpeed, _context.walkingSpeed, Time.deltaTime * 3);
        _context.CharacterController.Move(_context.transform.TransformDirection(movingVector) * _context._currentSpeed * Time.deltaTime);
        _context._currentVelocity.y += _context.gravity * Time.deltaTime;
        if (_context.CharacterController.isGrounded) {
            _context._currentVelocity.y = -2f;
        }
        _context.CharacterController.Move(_context._currentVelocity * Time.deltaTime);
        _context.Animator.SetFloat("magnitude", movingVector.magnitude / _context._currentSpeed);

        // Vector3 TargetVelocity = new Vector3(_context.CurrentMovement.x, 0, _context.CurrentMovement.y);
        // _context._currentSpeed = Mathf.Lerp(_context._currentSpeed, _context.walkingSpeed, Time.deltaTime * 3);
        // TargetVelocity *= _context._currentSpeed;
        // TargetVelocity = _context.transform.TransformDirection(TargetVelocity);
        // Vector3 VelocityChange = (TargetVelocity - _context._currentVelocity);
        // VelocityChange = new Vector3(VelocityChange.x, 0, VelocityChange.z);
        // Vector3.ClampMagnitude(VelocityChange, _context.maxForce);
        // _context.rig.AddForce(VelocityChange, ForceMode.VelocityChange);
    }
    public override void ExitState()
    {
        Debug.Log("PState: exit Walk");
        // _context._beforeMovement = _context.CharacterController.velocity;
        
        // _context._currentSpeed = 0f;
        // _context._currentVelocity = Vector3.zero;
    }
    public override void InitSubState() {
        
    }
    public override void CheckSwitchStates()
    {
        if (!_context.IsMoving) {
            SwitchState(_stateFactory.Idle());
        }
        if (_context.IsMoving && _context.IsRunning) {
            SwitchState(_stateFactory.Run());
        }
    }
}