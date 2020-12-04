using System.Collections.Generic;

namespace Systems.Finite_State_Machines.Interfaces {
    public interface IStateWrapper<T> where T : class {
        List<T> States { get; }
    }
}