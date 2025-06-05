using System;
using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemies {
    public class EnemyCollisionDamage : MonoBehaviour {
        private Animator _animator;
        private EnemyAudioController _controller;
        private bool _isHit;
        private float _waitToDestroy = 0.3f;
        private void Awake()
        {
            
        }
        void Start() {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<EnemyAudioController>();
        }

        void Update() {
            if (_isHit) {
                _waitToDestroy -= Time.deltaTime;
                if (_waitToDestroy <= 0) {
                    Destroy(gameObject);
                }
            }
        }
        private void OnCollisionStay2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player") && !_isHit) {
                _controller.PlayAttackSound();
                PlayerHealthController.instance.TakeObstacleDamage(10);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                FindFirstObjectByType<PlayerController>().Bump();
                _animator.SetTrigger("hit");
                _controller.PlayDeathSound();
                _isHit = true;
            }
        }
    }
}
