using Animancer.FSM;
using Entities;
using Entities.Player;
using UnityEngine;

namespace Finite_State_Machines {
    public abstract class AgentState : StateBehaviour<AgentState>, IOwnedState<AgentState> {
        #region Properties

        protected StateMachine<AgentState> _agentOwnerStateMachine;
        public StateMachine<AgentState> AgentOwnerStateMachine => _agentOwnerStateMachine;

        private Agent _agent;
        public Agent Agent {
            get => _agent;
            set {
                if (_agent != null && _agent.StateMachine.CurrentState == this) {
                    // Force to a default state here
                }

                _agent = value;
            }
        }

        #endregion

        #region Initialization

        private void Awake() {
            var agent = gameObject.GetComponent<Agent>();

            // Will NOT initialize if there is no Agent component on this Monobehaviour
            if (agent is null) {
                Debug.LogError($"There is no Agent component on the reference Monobehaviour");
                
                return;
            }

            if (agent is Player) {
                var playerAgent = agent as Player;

                Debug.Log($"Agent brain set to type 'Player' from {this.name} Monobehaviour");
            }
            // Add conditions for other various Agent Brains
            
            _agentOwnerStateMachine = new StateMachine<AgentState>();
        }

        #endregion

    }
} 