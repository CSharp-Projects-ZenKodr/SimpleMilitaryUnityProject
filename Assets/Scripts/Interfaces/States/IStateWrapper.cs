using System.Collections.Generic;

namespace Interfaces.States {
    public interface IStateWrapper<T> where T : class {
        List<T> States { get; }
    }
}