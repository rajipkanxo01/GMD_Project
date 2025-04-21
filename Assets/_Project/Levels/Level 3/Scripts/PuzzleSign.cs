using UnityEngine;

namespace _Project.Levels.Level_3.Scripts
{
    public class PuzzleSign : MonoBehaviour
    {
        [SerializeField] private string displayText;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Collider2D>().CompareTag("Player"))
            {
                UniversalTooltipUI.Instance.ShowTooltip($"{displayText}", transform.position);   
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            UniversalTooltipUI.Instance.HideTooltip();
        }
    }
}