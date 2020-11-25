using Entities;

namespace Entity_Systems.Finite_State_Machines.States {
    public interface IState<T> where T : State  {
        /// <summary>Determines whether this state can be entered.</summary>
        bool CanEnterState(T previousState);

        /// <summary>Determines whether this state can be exited.</summary>
        bool CanExitState(T nextState);

        /// <summary>Called when this state is entered.</summary>
        void OnEnterState(Agent agent);

        /// <summary>Called when this state is exited.</summary>
        void OnExitState(Agent agent);
        
        /// <summary>
        /// Updates the current state machine loop
        /// </summary>
        void Tick(Agent agent);
    }
}