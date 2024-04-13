using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Mana
{
    public class ManaVisualizer : MonoBehaviour
    {
        [SerializeField]
        TMP_Text manaCounterText;


        private void OnEnable()
        {
            ManaBank.instance.OnManaChanged += UpdateManaCounter;
        }


        private void Start()
        {
            UpdateManaCounter(0);
        }

        void UpdateManaCounter(int amountToAdd)
        {
            manaCounterText.text = ManaBank.ManaCount.ToString();
        }

        private void OnDisable()
        {
            ManaBank.instance.OnManaChanged -= UpdateManaCounter;
        }

    }
}