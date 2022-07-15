using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace TempustScript.Commands
{
    [DataContract(Name="CommandGive")]
    public class CommandGive : Command
    {
        [DataMember] public string itemId { get; private set; }
        [DataMember] public int amount { get; private set; }
        [DataMember] public TSScript parent { get; private set; }

        public CommandGive(TSScript parent, string itemId, int amount)
        {
            this.parent = parent;
            this.itemId = itemId;
            this.amount = amount;
        }

        public override IEnumerator Execute()
        {
            Debug.Log("Giving " + amount + " of item with id " + itemId);
            yield return null;
        }
    }
}
