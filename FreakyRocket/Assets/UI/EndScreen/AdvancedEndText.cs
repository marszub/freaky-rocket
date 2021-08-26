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
            for (int i = 0; i < winByWinsThreshod.Count - 1; i++)
            {
                if (winByWinsThreshod[i + 1] > StatTracker.stats.passesEachLevel[GameManager.currentLevel])
                    return winByWinsText[i].GetText();
            }
            return winByWinsText[winByWinsThreshod.Count - 1].GetText();
        }

        private string GetWinByDeaths()
        {
            for (int i = 0; i < winByDeathsThreshod.Count - 1; i++)
            {
                if (winByDeathsThreshod[i + 1] > StatTracker.stats.currentDeathsEachLevel[GameManager.currentLevel])
                    return winByDeathsText[i].GetText();
            }
            return winByDeathsText[winByDeathsThreshod.Count - 1].GetText();
        }

        private string GetLoseText()
        {
            for (int i = 0; i < loseByDeathsThreshod.Count - 1; i++)
            {
                if (loseByDeathsThreshod[i + 1] > StatTracker.stats.currentDeathsEachLevel[GameManager.currentLevel])
                    return loseByDeathsText[i].GetText();
            }
            return loseByDeathsText[loseByDeathsThreshod.Count - 1].GetText();
        }
    }
}