using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Tasks;

namespace Assets.Scripts.Mana
{
    public class ManaVisualizer : MonoBehaviour
    {
        [SerializeField]
        TMP_Text manaCounterText;

        [SerializeField]
        TMP_Text manaCostText;


        private void OnEnable()
        {
            ManaBank.instance.OnManaChanged += UpdateManaCounter;
            TaskGiver.OnHoverTaskGiver += ShowManaCost;
            TaskGiver.OnExitHoverTaskGiver += HideManaCost;
        }


        private void Start()
        {
            UpdateManaCounter(0);
            HideManaCost();
        }

        void UpdateManaCounter(int amountToAdd)
        {
            manaCounterText.text = ManaBank.ManaCount.ToString();
        }

        private void OnDisable()
        {
            ManaBank.instance.OnManaChanged -= UpdateManaCounter;
            TaskGiver.OnHoverTaskGiver -= ShowManaCost;
            TaskGiver.OnExitHoverTaskGiver -= HideManaCost;
        }

        private void ShowManaCost(Task task)
        {
            manaCostText.text = "-" + task.manaCost.ToString();
        }

        private void HideManaCost()
        {
            manaCostText.text = "";
        }

    }
}