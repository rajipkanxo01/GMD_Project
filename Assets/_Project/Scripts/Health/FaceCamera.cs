using UnityEngine;

namespace _Project.Scripts.Health {
    public class FaceCamera : MonoBehaviour {
        [SerializeField] private Transform camera;

        // Update is called once per frame
        void Update() {
            transform.LookAt(transform.position + camera.forward);
        }
    }
}
