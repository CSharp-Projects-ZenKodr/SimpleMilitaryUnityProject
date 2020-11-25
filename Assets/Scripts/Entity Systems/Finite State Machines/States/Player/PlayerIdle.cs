using Entities;
using Entity_Systems.Finite_State_Machines.States.Generics;

namespace Entity_Systems.Finite_State_Machines.States.Player {
    public class PlayerIdle : Idle {
        private Entities.Player.Player _player = null;
        public override void OnEnterState(Agent agent) {
            base.OnEnterState(agent);

            if (_player == null || _player != agent as Entities.Player.Player) {
                
            }
        }
    }
}