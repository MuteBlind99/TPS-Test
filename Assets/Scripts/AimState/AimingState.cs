using Unity.Cinemachine;
using UnityEngine;

namespace AimState
{
    public class AimingState : AimBaseState
    {
        
        public override void EnterState(AimStateManager aimStateManager)
        {
            aimStateManager.animator.SetBool("Aiming", true);
            aimStateManager.currentFov=aimStateManager.adsFov;
        }
       
        public override void UpdateState(AimStateManager aimStateManager)
        {
            if (Input.GetMouseButtonUp(0))
            {
                aimStateManager.aimcamera.Priority = 0;
                aimStateManager.SwitchState(aimStateManager.HipFireState);
            }
        }
    }
}
