using System.Collections.Generic;
using Entities.Human;
using Entity_Systems.Finite_State_Machines.States;

namespace Entity_Systems.Finite_State_Machines.Machine {
    public class HumanStateMachine : StateMachine<Human> {
        private readonly Human _agent;

        public override void InvokeStateTick(Human agent) {
            CurrentState.Tick(agent);
        }

        #region Inherited Constructor

        public HumanStateMachine(Human agent, IReadOnlyCollection<State<Human>> states) : base(agent, states) { }

        #endregion
    }
}