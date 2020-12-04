using Systems.SubSystems.Data_Containers;
using Entities;
using UnityEngine;

namespace Systems.Audio {
    public class AudioBrain<T> where T : Agent {
        #region Private Readonly Fields

        protected readonly T _agent;
        protected readonly AudioDataContainer _container;
        protected readonly AudioSource _audioSource;

        #endregion

        #region Constructor

        public AudioBrain(T agent, AudioDataContainer container, AudioSource audioSource) {
            _agent = agent;
            _container = container;
            _audioSource = audioSource;
        }

        #endregion

        #region Public Methods - Core

        public void PlayAudio(string audioName) {
            var audioClip = _container.Query(audioName);

            if (audioClip == null) {
                Debug.LogError("Please check the string constant or the audio data container dictionary.");
                
                return;
            }
            
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        #endregion
    }
}