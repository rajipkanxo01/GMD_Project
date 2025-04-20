using System;
using UnityEngine;

namespace _Project.Levels.Level_3.Scripts.MovingGround
{
    public class TorchPuzzleSign : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Collider2D>().CompareTag("Player"))
            {
                UniversalTooltipUI.Instance.ShowTooltip("Light all torches to activate the platform (Press E to light)", transform.position);   
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            UniversalTooltipUI.Instance.HideTooltip();
        }
    }
}