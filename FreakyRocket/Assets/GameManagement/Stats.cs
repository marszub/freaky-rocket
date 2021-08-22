using System.Collections;
using UnityEngine;

namespace Assets.GameManagement
{
    [System.Serializable]
    public class Stats
    {
        public int unlockedLevel;
        public int[] deathsEachLevel;
        public int[] passesEachLevel;

        public Stats()
        {
            unlockedLevel = 1;
            deathsEachLevel = new int[GlobalSettings.levels + 1]; // +1 is for simpler reference. Numeration from 1.
            passesEachLevel = new int[GlobalSettings.levels + 1];
        }
    }
}