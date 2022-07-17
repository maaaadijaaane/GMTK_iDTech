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
    }
}
public enum Ability{
    Stuck,
    Jump,
    Ladder,
    Sticky,
    None
}