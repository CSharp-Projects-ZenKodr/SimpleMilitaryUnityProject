using Systems.Animation.Helpers;
using Entities.Human;
using Entity_Systems.Finite_State_Machines.States.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.PlayerStates {
    [CreateAssetMenu(fileName = "HumanRun", menuName = "Create State/Human/Run")]
    public class HumanRun : StateWrapperHuman {
        public override void OnEnterState(Human agent) {
            base.OnEnterState(agent);
            
            SetLocomotion(agent.Locomotion);
            agent.AnimationBrain.Animate(AnimationDictionaryKeys.Run);
        }

        public override void OnExitState(Human agent) {
            base.OnExitState(agent);
        }

        public override void Tick(Human agent) {
            base.Tick(agent);
        }

        /// <summary>
        /// Sets the agent's locomotion parameter to be inline with what is expected from a 'run' state
        /// </summary>
        /// <param name="locomotion"></param>
        private void SetLocomotion(Locomotion locomotion) {
            locomotion.Speed = 4.0f;
            locomotion.IsRunning = !locomotion.IsRunning;
        }
    }
}