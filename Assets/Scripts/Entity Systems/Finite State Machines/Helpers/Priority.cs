using System;

namespace Entity_Systems.Finite_State_Machines.Helpers {
    [Serializable] public enum Priority {
        Idle,
        Crouch,
        Walk, 
        Run,
        Jump,
        ProjectileAttack
    }
}
