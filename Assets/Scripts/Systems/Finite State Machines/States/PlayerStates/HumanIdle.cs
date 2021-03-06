﻿using Systems.Animation.Helpers;
using Entities.Human;
using Entity_Systems.Finite_State_Machines.States.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.PlayerStates {
    [CreateAssetMenu(fileName = "HumanIdle", menuName = "Create State/Human/Idle")]
    public class HumanIdle : StateWrapperHuman {
        #region Public Overridden Methods

        public override void OnEnterState(Human agent) {
            base.OnEnterState(agent);

            agent.AnimationBrain.Animate(AnimationDictionaryKeys.Idle);
            ResetLocomotion(agent.Locomotion);
        }

        #endregion

        /// <summary>
        /// Resets locomotion parameters to be inline with what an Agent should be doing when idle
        /// </summary>
        /// <param name="locomotion">The agent's locomotion system</param>
        private static void ResetLocomotion(Locomotion locomotion) {
            locomotion.Speed = 0.0f;
            locomotion.CanSearch = false;
            locomotion.IsRunning = false;
        }
    }
}