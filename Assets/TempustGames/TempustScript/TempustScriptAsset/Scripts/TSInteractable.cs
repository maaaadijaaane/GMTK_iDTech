using System.Collections;
using UnityEngine;

namespace TempustScript
{
    public interface TSInteractable
    {
        public void OnInteract(GameObject interactor);
    }
}