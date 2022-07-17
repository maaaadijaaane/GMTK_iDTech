using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

public class DiceManager
{
    public List<Dice> dice;
    public UnityEvent<int> onRollFinished;
    public void RollDice()
    {
        dice.ForEach(die => die.roll());
    }

    void Update()
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

    public void AnnounceResult()
    {
        int finalRoll = 0;
        dice.ForEach(die => finalRoll += die.rollResult);

        onRollFinished?.Invoke(finalRoll);
    }
}
