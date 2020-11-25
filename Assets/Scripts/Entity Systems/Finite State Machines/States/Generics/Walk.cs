using Entities;
using Entity_Systems.SubSystems.Animation.Helpers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Generics {
    [CreateAssetMenu(order = 0, fileName = "GenericWalkState", menuName = "States/Generics/Generic Walk")]
    public class Walk : State {
        public override void OnEnterState(Agent agent) {
            base.OnEnterState(agent);
            
            agent.AnimationBrain.Animate(AnimId.Walk);
        }

        public override void OnExitState(Agent agent) {
            base.OnExitState(agent);
        }
    }
}