using UnityEngine;
namespace Photon.Pun.Demo.Asteroids
{
    public abstract class GhostTankBaseState
    {
        public abstract void EnterState(GhostTankStateManager ghostTank);
        public abstract void UpdateState(GhostTankStateManager ghostTank);
    }
}