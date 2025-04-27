using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Levels.Level_3.Scripts.Lift
{
    public class LiftSceneController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera playerCam;
        [SerializeField] private CinemachineCamera liftCam;
        [SerializeField] private float cutsceneDuration = 2f;

        public void ShowLiftCutscene()
        {
            StartCoroutine(SwitchToLiftView());
        }

        private IEnumerator SwitchToLiftView()
        {
            liftCam.Priority = 20; // Take control
            playerCam.Priority = 5; // Step back

            yield return new WaitForSeconds(cutsceneDuration);

            // Return to player
            playerCam.Priority = 10;
            liftCam.Priority = 5;
        }
    }
}