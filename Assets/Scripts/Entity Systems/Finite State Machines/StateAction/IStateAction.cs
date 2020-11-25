using Entities;
using Entities.Player;

namespace Entity_Systems.Finite_State_Machines.StateAction {
    public interface IStateAction<T> where T : StateAction {
        void Call(Agent genericAgent);
        void Call(Player playerAgent);
    }
}