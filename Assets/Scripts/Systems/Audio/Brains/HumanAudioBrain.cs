using Systems.Audio;
using Systems.SubSystems.Data_Containers;
using Entities.Human;
using UnityEngine;

namespace Entity_Systems.SubSystems.Audio.Brains {
    public class HumanAudioBrain : AudioBrain<Human> {
        #region Constructor - Inherited, Unmodified

        public HumanAudioBrain(Human agent, AudioDataContainer container, AudioSource audioSource) 
            : base(agent, container, audioSource) { }

        #endregion

        #region Audio Events

        public void RightFootStep() {
            //_audioSource.clip = _agent.AnimationDataContainer.
        }

        public void LeftFootStep() {
            
        }

        #endregion
    }
}