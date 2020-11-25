using Entities;
using Entity_Systems.SubSystems.Animation.Helpers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Generics {
    [CreateAssetMenu(order = 0, fileName = "GenericRunState", menuName = "States/Generics/Generic Run")]
    public class Run : State {
        public override void OnEnterState(Agent agent) {
            base.OnEnterState(agent);

            agent.AnimationBrain.Animate(AnimId.Run, 0.15f);
        }

        public override void OnExitState(Agent agent) {
            base.OnExitState(agent);
        }
    }
}