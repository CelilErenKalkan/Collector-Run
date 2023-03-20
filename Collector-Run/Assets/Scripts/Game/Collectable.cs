using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Collectable : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private void Awake()
        {
            if (TryGetComponent(out Rigidbody rb)) _rigidbody = rb;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        public void Push()
        {
            _rigidbody.DOMoveZ(transform.position.z + 8f, 1f);
        }
    }
}
