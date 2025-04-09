using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ParallaxInfiniteScroll : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;

        [Range(0f, 1f)] [SerializeField] private float _parallaxEffect;

        private float _startPos;
        private float _length;

        private void Start()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x; 
        }

        private void FixedUpdate()
        {
            float distance = _camera.transform.position.x * _parallaxEffect;
            float movement = _camera.transform.position.x * (1 - _parallaxEffect);
            
            transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);
            
            if (movement > _startPos + _length)
            {
                _startPos += _length;
            }
            else if (movement < _startPos - _length)
            {
                _startPos -= _length;
            }
        }
    }
}