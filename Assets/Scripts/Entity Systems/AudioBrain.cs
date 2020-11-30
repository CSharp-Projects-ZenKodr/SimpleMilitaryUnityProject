using Entities;
using UnityEngine;

namespace Entity_Systems {
    public class AudioBrain<T> where T : Agent {
        #region Private Readonly Fields

        protected readonly T _agent;
        protected readonly AudioSource _audioSource;

        #endregion

        #region Constructor

        public AudioBrain(T agent, AudioSource audioSource) {
            _agent = agent;
            _audioSource = audioSource;
        }

        #endregion

        #region Public Methods - Core

        public void PlayAudio(AudioClip clip) {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        #endregion
        
        #region Public Methods - Globa Query Audio Manager

        public void QueryAudioManagerToPlayClip(int clipIndex) {
            
        }

        #endregion
    }
}