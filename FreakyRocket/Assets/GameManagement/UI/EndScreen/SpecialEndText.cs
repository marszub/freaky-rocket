using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagement.UI.EndScreen
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Special end text")]
    public class SpecialEndText : ScriptableObject
    {
        [SerializeField]
        private List<EquivalentTextSet> totalDeathsTexts;
        [SerializeField]
        private List<int> totalDeaths;

        public string GetText(bool isWin)
        {
            if (isWin)
                return null;

            int deathsSum = 0;
            foreach (int deaths in StatTracker.stats.deathsEachLevel)
                deathsSum += deaths;

            for(int i = 0; i< totalDeaths.Count; i++)
            {
                if (StatTracker.stats.totalDeaths == totalDeaths[i])
                    return totalDeathsTexts[i].GetText();
            }

            return null;
        }
    }
}