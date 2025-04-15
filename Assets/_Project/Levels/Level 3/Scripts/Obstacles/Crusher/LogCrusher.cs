using System;
using System.Collections;
using _Project.Scripts;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Levels.Level_3.Scripts.Obstacles.Crusher
{
    public class LogCrusher : MonoBehaviour
    {
        [SerializeField] private Transform movingPart;
        [SerializeField] private float minScaleY = 0.4f;
        [SerializeField] private float maxScaleY = 1f;
        [SerializeField] private float scaleSpeed = 1.5f;
        [SerializeField] private float waitTimeAtEnds = 0.5f;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private float shakeDistance = 2f; 

        private bool scalingDown = true;
        private BoxCollider2D _collider;
        private Vector2 _originalSize;
        private Vector2 _originalOffset;

        private void Start()
        {
            _collider = movingPart.GetComponent<BoxCollider2D>();
            _originalSize = _collider.size;
            _originalOffset = _collider.offset;
            
            StartCoroutine(CrusherLoop());
        }

        private IEnumerator CrusherLoop()
        {
            while (true)
            {
                float targetY = scalingDown ? minScaleY : maxScaleY;

                while (Mathf.Abs(movingPart.localScale.y - targetY) > 0.01f)
                {
                    float newYScale = Mathf.MoveTowards(movingPart.localScale.y, targetY, scaleSpeed * Time.deltaTime);
                    movingPart.localScale = new Vector3(movingPart.localScale.x, newYScale, movingPart.localScale.z);
                    
                    if (scalingDown && Mathf.Approximately(targetY, minScaleY))
                    {
                        if (Vector2.Distance(transform.position, playerTransform.position) <= shakeDistance)
                        {
                            impulseSource?.GenerateImpulse();
                        }
                    }
                    
                    // Update collider size & offset based on current Y scale
                    _collider.size = new Vector2(_originalSize.x, _originalSize.y * newYScale);
                    
                    float heightDiff = (_originalSize.y - _originalSize.y * newYScale);
                    float newYOffset = _originalOffset.y + (heightDiff / 2f);
                    _collider.offset = new Vector2(_originalOffset.x, newYOffset);
                    
                    yield return null;
                }

                movingPart.localScale = new Vector3(movingPart.localScale.x, targetY, movingPart.localScale.z);
                _collider.size = new Vector2(_originalSize.x, _originalSize.y * targetY);
                _collider.offset = new Vector2(_originalOffset.x, _originalOffset.y * targetY);
                

                yield return new WaitForSeconds(waitTimeAtEnds);

                scalingDown = !scalingDown;
            }
        }
    }
}