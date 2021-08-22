using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.GameManagement.UI.EndScreen
{
    [CreateAssetMenu(menuName = "Freaky Rocket/End text set")]
    public class EquivalentTextSet : ScriptableObject
    {
        public List<string> texts;
        public List<float> weights;

        private void OnEnable()
        {
            if (texts == null)
                texts = new List<string>();
            if (weights == null)
                weights = new List<float>();
        }

        public string GetText()
        {
            float weightSum = 0;
            foreach (float weight in weights)
                weightSum += weight;
            float rand = Random.Range(0, weightSum);

            int textIdx = 0;
            while (rand > 0)
            {
                rand -= weights[textIdx];
                textIdx++;
            }

            return texts[textIdx];
        }
    }
}