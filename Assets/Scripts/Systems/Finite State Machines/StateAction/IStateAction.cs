using Entities;

namespace Entity_Systems.Finite_State_Machines.StateAction {
    public interface IStateAction<T> where T : Agent {
        void Act(T agent);
    }
}