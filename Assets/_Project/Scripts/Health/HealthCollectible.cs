
using System;
using _Project.Scripts.Audio;
using UnityEngine;

namespace _Project.Scripts.Health {
    public class HealthCollectible : MonoBehaviour {

        public GameObject collectibleEffect;
        
        private PickupAudioController _pickupAudioController;

        private void Awake()
        {
            _pickupAudioController = GetComponent<PickupAudioController>();
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                _pickupAudioController.PlayPickupSound();
                PlayerHealthController.instance.AddPlayerHealth(10);
                Destroy(gameObject);
                Instantiate(collectibleEffect, transform.position, transform.rotation);
            }
        }
    }
}
