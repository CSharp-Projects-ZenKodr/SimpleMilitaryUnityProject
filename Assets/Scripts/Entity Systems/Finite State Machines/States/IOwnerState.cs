using Entity_Systems.Finite_State_Machines.States;

namespace Entity_Systems.Finite_State_Machines {
    public interface IOwnerState<T> : IState<State> where T : State  {
        /// <summary>
        /// A state should implement this interface IF it needs access to the State Machine that
        /// handles it
        /// </summary>
        StateMachine<State> OwnerStateMachine { get; }
    }
}