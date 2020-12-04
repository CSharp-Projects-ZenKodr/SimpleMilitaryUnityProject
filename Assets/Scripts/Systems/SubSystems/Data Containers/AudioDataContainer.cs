using UnityEngine;

namespace Systems.SubSystems.Data_Containers {
    [CreateAssetMenu(menuName = "Data Containers/Audio", fileName = "AudioDataContainer")]
    public class AudioDataContainer : DataContainer<AudioClip> { }
}