using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagement.UI.EndScreen
{
    public class SimpleEndText : EndText
    {
        [SerializeField]
        private EquivalentTextSet win;
        [SerializeField]
        private EquivalentTextSet lose;

        protected override string GetLocalText(bool isWin)
        {
            if (isWin)
            {
                return win ? win.GetText() : "You Won";
            }
            else
            {
                return lose?lose.GetText() : "You Lost";
            }
        }
    }
}
