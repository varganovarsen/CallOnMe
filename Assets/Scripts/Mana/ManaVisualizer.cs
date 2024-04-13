using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Mana
{
    public class ManaVisualizer : MonoBehaviour
    {
        [SerializeField]
        TMP_Text manaCounterText;

        private void Start()
        {
            ManaBank.instance.OnManaChanged += UpdateManaCounter;
            UpdateManaCounter(0);
        }

        void UpdateManaCounter(int amountToAdd)
        {
            manaCounterText.text = ManaBank.ManaCount.ToString();
        }
    }
}