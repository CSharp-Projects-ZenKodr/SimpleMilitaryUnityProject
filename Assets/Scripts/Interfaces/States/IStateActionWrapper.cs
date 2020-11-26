using System.Collections.Generic;

namespace Interfaces.States {
    public interface IStateActionWrapper<T> where T : class {
        List<T> StateActions { get; }
    }
}