using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PlatformSystem
{
    public class CheckPointCounter : MonoBehaviour
    {
        private AssetManager _assetManager;
        private int _targetCounter;
        private int _counter;
        [SerializeField] private TextMeshPro textMesh;
        private MeshRenderer _meshRenderer;
        private Vector3 _firstPos;

        public void Initialize(int target)
        {
            _assetManager = AssetManager.Instance;
            _counter = 0;
            _targetCounter = target;
            textMesh.text = Mathf.RoundToInt(_counter) + "/" + _targetCounter;
            if (TryGetComponent(out MeshRenderer meshRenderer)) _meshRenderer = meshRenderer;
            _firstPos = new Vector3(transform.position.x, -3.43f, transform.position.z);
        }

        public void SuccessfulAction()
        {
            transform.DOMoveY(0, 1f);
            textMesh.enabled = false;
            _meshRenderer.material = _assetManager.groundMaterial;
        }

        public int GetCounter()
        {
            var temp = _counter;
            _counter = 0;
            return temp;
        }

        private void Reset()
        {
            transform.position = _firstPos;
            textMesh.enabled = true;
            textMesh.text = Mathf.RoundToInt(_counter) + "/" + _targetCounter;
            _meshRenderer.material = _assetManager.collectorMaterial;
        }

        private void OnEnable()
        {
            SUCCESS += Reset;
            FAIL += Reset;
        }

        private void OnDisable()
        {
            SUCCESS -= Reset;
            FAIL -= Reset;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Collectable picker)) return;
            
            picker.Deactivate();
            DOVirtual.Float(_counter, ++_counter, 1f,
                value => { textMesh.text = Mathf.RoundToInt(value) + "/" + _targetCounter; });
        }
    }
}