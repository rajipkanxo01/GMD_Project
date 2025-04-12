using System;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyDetection : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private float range = 1.5f;
        [SerializeField] private float colliderDistance = 0.5f;
        [SerializeField] private LayerMask playerLayer;
        
        public LayerMask PlayerLayer => playerLayer;
        
        public bool PlayerInRange()
        {
            Vector3 origin = boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
            Vector3 size = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
            Collider2D hit = Physics2D.OverlapBox(origin, size, 0, playerLayer);
            
            return hit != null;
        }

        private void OnDrawGizmos()
        {
            if (boxCollider == null) return;
            Gizmos.color = Color.red;

            Vector3 origin = boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
            Vector3 size = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
            Gizmos.DrawWireCube(origin, size);
        }
        
        public Vector3 GetBoxOrigin()
        {
            return boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        }

        public Vector3 GetBoxSize()
        {
            return new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        }
    }
}