using Systems.Animation.Helpers;
using Entities.Human;
using Entity_Systems.Finite_State_Machines.States.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.PlayerStates {
    [CreateAssetMenu(fileName = "HumanWalk", menuName = "Create State/Human/Walk")]
    public class HumanWalk : StateWrapperHuman {
        public override void OnEnterState(Human agent) {
            base.OnEnterState(agent);

            SetLocomotion(agent.Locomotion);
            agent.AnimationBrain.Animate(out var state, AnimationDictionaryKeys.Walk);

            //state.Events.Add(AnimationEventStatics.Player.WalkLeftStepTime, agent.AudioBrain.LeftFootStep);
            //state.Events.Add(AnimationEventStatics.Player.WalkRightStepTime, agent.AudioBrain.RightFootStep);    
        }

        public override bool CanExitState(State<Human> nextState) {
            return base.CanExitState(nextState);
        }

        public override void Tick(Human agent) {
            base.Tick(agent);
        }

        /// <summary>
        /// Sets the agent's locomotion parameters to be in line with a walking state
        /// </summary>
        /// <param name="locomotion"></param>
        private void SetLocomotion(Locomotion locomotion) {
            locomotion.Speed = 2.0f;
        }

    }
}