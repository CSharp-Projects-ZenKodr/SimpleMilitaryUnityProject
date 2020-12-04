using System.Collections.Generic;

namespace Systems.Finite_State_Machines.Interfaces {
    public interface IStateActionWrapper<T> where T : class {
        List<T> StateActions { get; }
    }
}