using Entity_Systems;

namespace Interfaces.Animations {
    public interface IOnPrimaryClick {
        void OnPrimaryClick(Raycasting raycasting, Locomotion locomotion, Pathing pathing);
    }
}