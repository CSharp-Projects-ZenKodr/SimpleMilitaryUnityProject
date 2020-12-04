using Entity_Systems;

namespace Systems.Animation.Interfaces {
    public interface IOnPrimaryClick {
        void OnPrimaryClick(Raycasting raycasting, Locomotion locomotion, Pathing pathing);
    }
}