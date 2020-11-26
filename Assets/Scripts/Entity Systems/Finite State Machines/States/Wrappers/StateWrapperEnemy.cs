using System.Collections.Generic;
using Entities;
using Entity_Systems.Finite_State_Machines.StateAction.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Wrappers {
    public class StateWrapperEnemy : State<Enemy> {
        [SerializeField] protected List<StateActionWrapperEnemy> StateActions;

        public override void Tick(Enemy agent) {
            if (StateActions == null) return;
            
            foreach (var action in StateActions) {
                action.Act(agent);
            }
        }
    }
}