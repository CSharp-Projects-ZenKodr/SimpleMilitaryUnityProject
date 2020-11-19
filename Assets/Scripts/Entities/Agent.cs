using Animation;
using Entity_Systems;
using Pathfinding;
using UnityEngine;

namespace Entities {
    #region Required Mono Components
    
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(RaycastModifier))]
    [RequireComponent(typeof(SimpleSmoothModifier))]

    #endregion
    
    public abstract class Agent : MonoBehaviour {
        #region Protected Fields & Propreties - Core Systems

        protected Pathing _pathing;
        protected Raycasting _raycasting;
        protected Locomotion _locomotion;
        protected Skeleton _skeleton;
        protected AnimationHandler _animationHandler;

        #endregion

        #region Protected Virtual Methods

        protected virtual void Awake() {
            var tr = GetComponent<Transform>();
            
            _pathing = new Pathing(
                GetComponent<Seeker>());
            _raycasting = new Raycasting(Camera.main);
            _locomotion = new Locomotion(tr, GetComponent<AIPath>());
            _skeleton = new Skeleton(gameObject);
            _animationHandler = new AnimationHandler(GetComponentInChildren<Animator>());
        }

        protected virtual void OnEnable() {
            
        }

        protected virtual void OnDisable() {
            
        }

        protected virtual void Start() {
            
        }

        protected abstract void CreateAgentSpecificDependencies();

        protected abstract void InitializeAgentSpecificDependencies();
        
        #endregion

        #region Private Methods

        private void OnAnimatorMove() {
            transform.position += _animationHandler.ReturnDeltaPositionVector();
        }

        #endregion
    }
}