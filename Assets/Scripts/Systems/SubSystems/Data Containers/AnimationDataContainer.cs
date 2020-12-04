using Animancer;
using UnityEngine;

namespace Systems.SubSystems.Data_Containers {
    [CreateAssetMenu(menuName = "Data Containers/Animation", fileName = "AnimationDataContainer")]
    public class AnimationDataContainer : DataContainer<ClipState.Transition> { }
}