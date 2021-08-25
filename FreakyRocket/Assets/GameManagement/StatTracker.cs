using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagement.UI
{
    public class StatTracker : MonoBehaviour
    {
        public static Stats stats;

        public static int unlockedLevel { get => stats.unlockedLevel; }

        private void OnEnable()
        {
            if (stats == null)
            {
                stats = new Stats();
            }

            GameplayController.LevelPassed += LevelPassed;
            GameplayController.Death += Death;
        }

        private void OnDisable()
        {
            GameplayController.LevelPassed -= LevelPassed;
            GameplayController.Death -= Death;
        }

        private void LevelPassed(int level)
        {
            if (level > stats.passesEachLevel.Length)
                throw new ArgumentException("Level argument: " + level.ToString() + "; Expected <= " + stats.passesEachLevel.Length);
            if(level < 1)
                throw new ArgumentException("Level argument: " + level.ToString() + "; Expected > 0");
            stats.passesEachLevel[level]++;
            stats.currentDeathsEachLevel[level] = 0;

            if (level >= stats.unlockedLevel)
                stats.unlockedLevel = level + 1;
        }

        private void Death(int level)
        {
            if (level > stats.deathsEachLevel.Length)
                throw new ArgumentException("Level argument: " + level.ToString() + "; Expected <= " + stats.deathsEachLevel.Length);
            if (level < 1)
                throw new ArgumentException("Level argument: " + level.ToString() + "; Expected > 0");
            stats.deathsEachLevel[level]++;
            stats.currentDeathsEachLevel[level]++;
            stats.totalDeaths++;
        }
    }
}
