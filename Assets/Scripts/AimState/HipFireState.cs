using Unity.Cinemachine;
using UnityEngine;

namespace AimState
{
    public class HipFireState : AimBaseState
    {
        public override void EnterState(AimStateManager aimStateManager)
        {
            aimStateManager.animator.SetBool("Aiming", false);
            aimStateManager.currentFov=aimStateManager.hipFov;
        }
       
        public override void UpdateState(AimStateManager aimStateManager)
        {
            if (Input.GetMouseButton(0))
            {
                aimStateManager.aimcamera.Priority = 100;
                aimStateManager.SwitchState(aimStateManager.AimState);
            }
        }
    }
}
