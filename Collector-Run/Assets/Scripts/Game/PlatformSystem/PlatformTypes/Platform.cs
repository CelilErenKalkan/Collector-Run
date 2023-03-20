using UnityEngine;

namespace Game.PlatformSystem.PlatformTypes
{
    public enum PlatformType
    {
        NORMAL,
        CHECKPOINT,
        FINAL
    }
    
    public abstract class Platform : MonoBehaviour
    {
        public abstract PlatformType PlatformType { get; }
        [HideInInspector] public bool isActive;
        
        public virtual void Initialize()
        {
            isActive = false;
        }
        
        public void Activate()
        {
            isActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
        }
        
    }
}
