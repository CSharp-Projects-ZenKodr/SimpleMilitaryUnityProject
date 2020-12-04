using Entities.Human;
using Entity_Systems.Finite_State_Machines.StateAction;

namespace Systems.Finite_State_Machines.StateAction.Wrappers {
    public class StateActionWrapperHuman : StateAction<Human> {
        public override void Act(Human agent) { }
    }
}