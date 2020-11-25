using Entities;
using Entity_Systems.SubSystems.Animation.Helpers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Generics {
    [CreateAssetMenu(order = 0, fileName = "GenericIdleState", menuName = "States/Generics/Generic Idle")]
    public class Idle : State{
        public override void OnEnterState(Agent agent) {
            base.OnEnterState(agent);
            agent.AnimationBrain.Animate(agent.Container.Query(AnimId.Idle));
        }
        
        public override void OnExitState(Agent agent) {
            base.OnEnterState(agent);
        }

        public override void Tick(Agent agent) {
            
        }
    }
}