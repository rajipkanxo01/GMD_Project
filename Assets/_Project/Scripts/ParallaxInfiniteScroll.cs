using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ParallaxInfiniteScroll : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;

        [Range(0f, 1f)] [SerializeField] private float _parallaxEffectX = 0.5f;
        [Range(0f, 1f)] [SerializeField] private float _parallaxEffectY = 0.5f;

        private Vector3 _startPos;
        private float _lengthX;
        private float _lengthY;

        private void Start()
        {
            _startPos = transform.position;
            _lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
            _lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        }

        private void FixedUpdate()
        {
            float distanceX = _camera.transform.position.x * _parallaxEffectX;
            float distanceY = _camera.transform.position.y * _parallaxEffectY;

            float movementX = _camera.transform.position.x * (1 - _parallaxEffectX);
            float movementY = _camera.transform.position.y * (1 - _parallaxEffectY);

            transform.position = new Vector3(_startPos.x + distanceX, _startPos.y + distanceY, transform.position.z);

            if (movementX > _startPos.x + _lengthX)
                _startPos.x += _lengthX;
            else if (movementX < _startPos.x - _lengthX)
                _startPos.x -= _lengthX;

            if (movementY > _startPos.y + _lengthY)
                _startPos.y += _lengthY;
            else if (movementY < _startPos.y - _lengthY)
                _startPos.y -= _lengthY;
        }
    }
}