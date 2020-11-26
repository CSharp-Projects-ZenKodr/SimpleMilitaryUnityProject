using System.Collections.Generic;
using Entities;
using Entity_Systems.Finite_State_Machines.StateAction.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States.Wrappers {
    public class StateWrapperEnemy : State<Enemy> {
        public override void Tick(Enemy agent) { }
        
        [SerializeField] protected List<StateActionWrapperEnemy> StateActions;
    }
}