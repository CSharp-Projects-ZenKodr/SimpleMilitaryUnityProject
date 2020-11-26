using System.Collections.Generic;
using Entities.Human;
using Entity_Systems.Finite_State_Machines.StateAction.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Wrappers {
    public class StateWrapperHuman : State<Human> {
        public override void Tick(Human agent) { }

        [SerializeField] protected List<StateActionWrapperHuman> StateActions;
    }
}