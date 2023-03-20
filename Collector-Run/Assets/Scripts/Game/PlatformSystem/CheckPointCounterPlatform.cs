using Bases;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PlatformSystem
{
    public class CheckPointCounterPlatform : MonoBehaviour
    {
        private int _targetCounter;
        private int _counter;
        private TextMeshPro _textMesh;
        private MeshRenderer _meshRenderer;
        private Vector3 _firstPos;

        public void Initialize(int target)
        {
            _counter = 0;
            _targetCounter = target;
            _textMesh = GetComponentInChildren<TextMeshPro>(true);
            _meshRenderer = GetComponent<MeshRenderer>();
            _textMesh.text = Mathf.RoundToInt(_counter) +"/" + _targetCounter;
            _firstPos = new Vector3(transform.position.x,-3.43f, transform.position.z);
        }

        public void SuccessfulAction()
        {
            transform.DOMoveY(0, 1f);
            _textMesh.enabled = false;
            _meshRenderer.material = AssetManager.Instance.groundMaterial;
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
            _textMesh.enabled = true;
            _textMesh.text = Mathf.RoundToInt(_counter) +"/" + _targetCounter;
            _meshRenderer.material = AssetManager.Instance.pickerMaterial;
        }

        private void OnEnable()
        {
            Success += Reset;
            Fail += Reset;
        }
        
        private void OnDisable()
        {
            Success -= Reset;
            Fail -= Reset;
        }

        private void OnCollisionEnter(Collision other)
        {
            var picker = other.gameObject.GetComponent<CollectableBase>();

            if (picker != null)
            {
                picker.Deactivate();
                DOVirtual.Float(_counter,++_counter,1f, value =>
                {
                    _textMesh.text = Mathf.RoundToInt(value) +"/" + _targetCounter;
                });
            }
        }
    }
}
