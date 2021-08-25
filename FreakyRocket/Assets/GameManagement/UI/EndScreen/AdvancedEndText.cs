using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagement.UI.EndScreen
{
    public class AdvancedEndText : EndText
    {
        [SerializeField] private float winByWinsWeight;
        [SerializeField] private List<EquivalentTextSet> winByWinsText;
        [SerializeField] private List<int> winByWinsThreshod;

        [SerializeField] private float winByDeathsWeight;
        [SerializeField] private List<EquivalentTextSet> winByDeathsText;
        [SerializeField] private List<int> winByDeathsThreshod;

        [SerializeField] private float loseByDeathsWeight;
        [SerializeField] private List<EquivalentTextSet> loseByDeathsText;
        [SerializeField] private List<int> loseByDeathsThreshod;

        protected override string GetLocalText(bool isWin)
        {
            if (isWin)
                return GetWinText();
            else
                return GetLoseText();
        }

        private string GetWinText()
        {
            if (Random.Range(0, winByDeathsWeight + winByWinsWeight) < winByWinsWeight)
                return GetWinByWins();
            else
                return GetWinByDeaths();
        }

        private string GetWinByWins()
        {
            for(int i = 0; i < winByWinsThreshod.Count; i++)
            {
                if (winByWinsThreshod[i] <= StatTracker.stats.passesEachLevel[GameManager.currentLevel])
                    return winByWinsText[i].GetText();
            }
            return defaultWinText;
        }

        private string GetWinByDeaths()
        {
            for (int i = 0; i < winByDeathsThreshod.Count; i++)
            {
                if (winByDeathsThreshod[i] <= StatTracker.stats.currentDeathsEachLevel[GameManager.currentLevel])
                    return winByDeathsText[i].GetText();
            }
            return defaultWinText;
        }

        private string GetLoseText()
        {
            for (int i = 0; i < loseByDeathsThreshod.Count; i++)
            {
                if (loseByDeathsThreshod[i] <= StatTracker.stats.currentDeathsEachLevel[GameManager.currentLevel])
                    return loseByDeathsText[i].GetText();
            }
            return defaultLoseText;
        }
    }
}