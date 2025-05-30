using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class ControlsUIManager : MonoBehaviour
    {
        public static ControlsUIManager Instance { get; private set; }

        [SerializeField] private GameObject controlsUI;
        [SerializeField] private GameObject goBackButton;
        [SerializeField] private GameObject mainMenuDefaultButton;

        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            controlsUI.SetActive(false);
        }

        public void ShowControls()
        {
            if (controlsUI != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(goBackButton);
                
                controlsUI.SetActive(true);
            }
        }

        public void HideControls()
        {
            if (controlsUI != null)
            {
                controlsUI.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(mainMenuDefaultButton);
            }
        }
    }
}