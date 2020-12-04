using System.Collections.Generic;
using Systems.Finite_State_Machines.StateAction.Wrappers;
using Entities.Human;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Wrappers {
    public class StateWrapperHuman : State<Human> {
        [SerializeField] protected List<StateActionWrapperHuman> StateActions;

        public override void Tick(Human agent) {
            if (StateActions == null) return;
            
            foreach (var action in StateActions) {
                action.Act(agent);
            }
        }
    }
}