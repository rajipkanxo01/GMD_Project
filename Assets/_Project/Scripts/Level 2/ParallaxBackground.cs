using UnityEngine;

namespace _Project.Scripts.Level_2 {
public class ParallaxBackground : MonoBehaviour
{
    private Transform camTransform;
    private Vector3 lastCamPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        camTransform = Camera.main.transform;
        lastCamPosition = camTransform.position;
    }

    // Update is called once per frame
    void Update() {
        Vector3 newCamPosition = camTransform.position - lastCamPosition;
        transform.position += newCamPosition;
        lastCamPosition = camTransform.position;
    }
}
}
