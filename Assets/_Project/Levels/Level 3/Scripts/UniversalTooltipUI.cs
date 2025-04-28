using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Levels.Level_3.Scripts
{
    public class UniversalTooltipUI : MonoBehaviour
    {
        public static UniversalTooltipUI Instance;

        [SerializeField] private RectTransform tooltipTransform;
        [SerializeField] private TMP_Text tooltipText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Camera mainCamera;
        // [SerializeField] private InputActionReference interactAction;

        private void Awake()
        {
            Instance = this;
            HideTooltip();
        }
        
        public void ShowTooltip(string displayText, Vector3 worldPosition)
        {
            tooltipText.text = $"{displayText}";

            // Move the tooltip above the world object
            Vector2 screenPos = mainCamera.WorldToScreenPoint(worldPosition);
            tooltipTransform.position = screenPos + new Vector2(0, 300f);

            // Reset alpha and scale
            canvasGroup.alpha = 0f;
            tooltipTransform.localScale = Vector3.one * 0.8f;

            StartCoroutine(FadeIn());
        }


        public void HideTooltip()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
        
        public IEnumerator FadeIn()
        {
            float t = 0f;
            Vector3 startScale = Vector3.one * 0.8f;
            Vector3 targetScale = Vector3.one;

            while (t < 1f)
            {
                t += Time.deltaTime * 5f;
                float eased = Mathf.SmoothStep(0f, 1f, t);
                canvasGroup.alpha = eased;
                tooltipTransform.localScale = Vector3.Lerp(startScale, targetScale, eased);
                yield return null;
            }

            canvasGroup.alpha = 1f;
            tooltipTransform.localScale = targetScale;
        }

        public IEnumerator FadeOut()
        {
            float t = 1f;
            while (t > 0f)
            {
                t -= Time.deltaTime * 5f;
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }
        }


        /*private string GetBindingDisplay()
        {
            var playerInput = FindObjectOfType<PlayerInput>();
            string controlScheme = playerInput.currentControlScheme;
            int bindingIndex = interactAction.action.GetBindingIndexForControlScheme(controlScheme);
            return interactAction.action.GetBindingDisplayString(bindingIndex);
        }*/
    }
}
