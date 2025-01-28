using Unity.Cinemachine;
using UnityEngine;

namespace AimState
{
    public abstract class AimBaseState
    {
       
        
        public abstract void EnterState(AimStateManager aimStateManager);

        public abstract void UpdateState(AimStateManager aimStateManager);

       
    }
}