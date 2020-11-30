using Generics;
using UnityEngine;

namespace Managers {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager> {
        private AudioSource _managerSource;

        private void Awake() {
            _managerSource = GetComponent<AudioSource>();
        }

        public void PlayAudioSource(AudioSource source) {
            if (source != null) {
                source.Play();
            }
        }

        public void PlayAudioThroughManager(AudioClip audioClip) {
            if (audioClip is null) return;

            _managerSource.clip = audioClip;
            _managerSource.Play();
        }
    }
}