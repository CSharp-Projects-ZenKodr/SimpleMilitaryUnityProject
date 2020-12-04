using Entities;
using Entity_Systems.Finite_State_Machines.StateAction;

namespace Systems.Finite_State_Machines.StateAction.Wrappers {
    public class StateActionWrapperEnemy : StateAction<Enemy> {
        public override void Act(Enemy agent) { }
    }
}