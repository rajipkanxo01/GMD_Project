using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class ControlsUIManager : MonoBehaviour
    {
        public static ControlsUIManager Instance { get; private set; }

        [SerializeField] private GameObject controlsUI;
        [SerializeField] private GameObject goBackButton;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.Log("[ControlsUIManager] Duplicate detected, destroying new instance.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            controlsUI.SetActive(false);
            Debug.Log("[ControlsUIManager] Initialized and set controlsUI inactive.");
        }

        public void ShowControls()
        {
            if (controlsUI != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(goBackButton);
                
                controlsUI.SetActive(true);

                
                Debug.Log("[ControlsUIManager] ShowControls() called - controlsUI activated.");
            }
            else
            {
                Debug.LogWarning("[ControlsUIManager] ShowControls() failed - controlsUI reference is null.");
            }
        }

        public void HideControls()
        {
            if (controlsUI != null)
            {
                controlsUI.SetActive(false);
                Debug.Log("[ControlsUIManager] HideControls() called - controlsUI deactivated.");
            }
            else
            {
                Debug.LogWarning("[ControlsUIManager] HideControls() failed - controlsUI reference is null.");
            }
        }
    }
}