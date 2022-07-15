using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TempustScript
{
    public class WaitForEvent : CustomYieldInstruction
    {
        bool shouldContinue = false;

        public override bool keepWaiting
        {
            get
            {
                return !shouldContinue;
            }
        }

        private void onEvent()
        {
            shouldContinue = true;
        }

        public WaitForEvent(UnityAction waitEvent)
        {
            waitEvent += onEvent;
        }
    }
}
