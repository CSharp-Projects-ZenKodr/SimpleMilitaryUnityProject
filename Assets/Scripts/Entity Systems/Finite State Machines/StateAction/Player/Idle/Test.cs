using Entities.Human;
using Entity_Systems.Finite_State_Machines.StateAction.Wrappers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.StateAction.Player.Idle {
    [CreateAssetMenu(fileName = "PlayerTestAction", menuName = "Create State Action/Player/Test")]
    public class Test : StateActionWrapperHuman {
        public override void Act(Human agent) {
            Debug.Log("Actions has been successfully demonstrated functional!");
        }
    }
}