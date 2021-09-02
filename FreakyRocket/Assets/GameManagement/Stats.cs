using System.Collections;
using UnityEngine;

namespace Assets.GameManagement
{
    [System.Serializable]
    public class Stats
    {
        public int totalDeaths;
        public int[] deathsEachLevel;
        public int[] currentDeathsEachLevel;
        public int[] passesEachLevel;

        public Stats()
        {
            totalDeaths = 0;
            deathsEachLevel = new int[GlobalSettings.levels + 1]; // +1 is for simpler reference. Numeration from 1.
            currentDeathsEachLevel = new int[GlobalSettings.levels + 1];
            passesEachLevel = new int[GlobalSettings.levels + 1];
        }
    }
}