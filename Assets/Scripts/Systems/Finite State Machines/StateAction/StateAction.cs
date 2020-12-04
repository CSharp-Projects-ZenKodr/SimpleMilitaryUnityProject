using Entities;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.StateAction {
    public abstract class StateAction<T> : ScriptableObject, IStateAction<T> where T : Agent {
       public virtual void Act(T agent) { }
    }
}