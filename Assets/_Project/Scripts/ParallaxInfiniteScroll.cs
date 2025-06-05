using UnityEngine;

namespace _Project.Scripts
{
    public class ParallaxInfiniteScroll : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [Range(0f, 1f)] [SerializeField] private float _parallaxEffectX = 0.5f;

        private Vector3 _startPos;
        private float _spriteWidth;

        private void Start()
        {
            _startPos = transform.position;
            _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float temp = (_camera.transform.position.x * (1 - _parallaxEffectX));
            float dist = (_camera.transform.position.x * _parallaxEffectX);

            transform.position = new Vector3(_startPos.x + dist, _startPos.y, transform.position.z);

            // Now, wrap the background!
            if (temp > _startPos.x + _spriteWidth)
            {
                _startPos.x += _spriteWidth;
            }
            else if (temp < _startPos.x - _spriteWidth)
            {
                _startPos.x -= _spriteWidth;
            }
        }
    }
}