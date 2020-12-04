using Entities;

namespace Entity_Systems.Finite_State_Machines.States {
    public interface IState<T> where T : Agent {
    /// <summary>Determines whether this state can be entered.</summary>
    bool CanEnterState(State<T> previousState);

    /// <summary>Determines whether this state can be exited.</summary>
    bool CanExitState(State<T> nextState);
    }
}