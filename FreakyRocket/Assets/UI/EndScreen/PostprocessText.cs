using System.Collections;
using UnityEngine;

namespace Assets.GameManagement.UI.EndScreen
{
    public static class PostprocessText
    {
        public static string Process(string text)
        {
            while (true)
            {
                int idx = text.IndexOf("`totalDeaths`");
                if (idx == -1)
                    break;
                text = text.Remove(idx, "`totalDeaths`".Length).Insert(idx, StatTracker.stats.totalDeaths.ToString());
            }
            return text;
        }
    }
}