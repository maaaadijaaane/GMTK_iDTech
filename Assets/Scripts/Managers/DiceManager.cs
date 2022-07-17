using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : MonoBehaviour
{
    public List<Dice> dice;
    public UnityEvent<int> onRollFinished;
    private bool rolling;

    public void RollDice()
    {
        dice.ForEach(die => die.roll());
        rolling = true;
    }

    void Update()
    {
        if (rolling)
        {
            bool allStopped = true;

            foreach (Dice die in dice)
            {
                if (die.rolling)
                {
                    allStopped = false;
                    break;
                }
            }

            if (allStopped)
            {
                AnnounceResult();
            }
        }
    }

    public void AnnounceResult()
    {
        rolling = false;
        int finalRoll = 0;
        dice.ForEach(die => finalRoll += die.rollResult);

        onRollFinished?.Invoke(finalRoll);
    }
}
