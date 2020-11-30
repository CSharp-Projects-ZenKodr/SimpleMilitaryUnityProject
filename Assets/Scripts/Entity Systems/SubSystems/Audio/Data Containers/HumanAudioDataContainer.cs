using System.Collections.Generic;
using Entity_Systems.SubSystems.Data_Containers;
using UnityEngine;

namespace Entity_Systems.SubSystems.Audio.Data_Containers {
    [CreateAssetMenu(fileName = "HumanAudioDataContainer", menuName = "Create Data Container/Human Audio Container")]
    public class HumanAudioDataContainer : DataContainerAudioWrapper {
        [SerializeField] private List<AudioClip> _audioClips;
        public override List<AudioClip> DataList => _audioClips;
    }
}