using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public enum ObjectGroupType
    {
        RECTANGLE,
        TOWER,
        MIXED
    }
    
    public class ObjectGroup : MonoBehaviour
    {
        [HideInInspector] public bool isActive;
        public ObjectGroupType objectGroupType;

        private List<Collectable> _collectableBases;
        private List<Vector3> _positions;
        private Vector3 _groupPosition;
        
        public void Initialize()
        {
            _collectableBases = GetComponentsInChildren<Collectable>(true).ToList();
            _groupPosition = transform.position;
            _positions = new List<Vector3>(_collectableBases.Count);

            for (var i = 0; i < _collectableBases.Count; i++)
            {
                _positions.Add(_collectableBases[i].transform.localPosition);
            }

            isActive = false;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            isActive = true;
            
            transform.position = _groupPosition;

            foreach (var collectable in _collectableBases)
            {
                collectable.transform.localPosition = _positions[collectable.transform.GetSiblingIndex()];
                collectable.Activate();
            }

            PhysicsActivation(true);
        }

        public void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
            PhysicsActivation(false);
        }

        private void PhysicsActivation(bool state)
        {
            foreach (var collectableBase in _collectableBases)
            {
                if (collectableBase.TryGetComponent(out Rigidbody rb)) rb.isKinematic = !state;
            }
        }
    }
}
