using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(CustomCharacterController context, PlayerStateFactory stateFactory) : base (context, stateFactory){}
    public override void EnterState()
    {
        Debug.Log("PState: enter Idle");
        _context.Animator.SetBool("IsMoving", false);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        
    }
    public override void ExitState()
    {
        Debug.Log("PState: exit Idle");
    }
    public override void InitSubState() {

    }
    public override void CheckSwitchStates()
    {
        if (_context.IsMoving && _context.IsRunning) {
            SwitchState(_stateFactory.Run());
        }
        else if (_context.IsMoving) {
            SwitchState(_stateFactory.Walk());
        }
    }
}