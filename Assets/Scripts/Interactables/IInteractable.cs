using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string InteractionText { get; }
    
    void Hover();

    // The method executed when the player triggers the interaction
    void Interact();
}
