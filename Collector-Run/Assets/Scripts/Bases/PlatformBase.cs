using UnityEngine;

namespace Bases
{
    public abstract class PlatformBase : MonoBehaviour
    {
        public abstract PlatformType PlatformType { get; }
        public bool isActive;
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

    public enum PlatformType
    {
        NORMAL,
        CHECKPOINT,
        FINAL
    }
}
