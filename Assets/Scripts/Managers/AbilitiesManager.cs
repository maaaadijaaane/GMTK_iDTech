using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static void AddAbility(Ability newAbility)
    {
        if(newAbility == Ability.Jump)
        {
            PlayerController.doubleJump = true;
        }
        else
        {
            UpdatePlayer.activeAbility = newAbility;
        }
    }
}
public enum Ability{
    Static,
    Jump,
    Ladder,
    Sticky,
    None
}