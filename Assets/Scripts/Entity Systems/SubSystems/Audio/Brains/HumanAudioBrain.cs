using Entities.Human;
using UnityEngine;

namespace Entity_Systems.SubSystems.Audio.Brains {
    public class HumanAudioBrain : AudioBrain<Human> {
        #region Constructor - Inherited, Unmodified

        public HumanAudioBrain(Human agent, AudioSource audioSource) : base(agent, audioSource) { }

        #endregion

        #region Audio Events

        public void RightFootStep() {
            //_audioSource.clip = _agent.AnimDataContainer.
        }

        public void LeftFootStep() {
            
        }

        #endregion
    }
}