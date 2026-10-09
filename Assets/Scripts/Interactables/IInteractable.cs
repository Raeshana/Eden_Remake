using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Hover();

    // The method executed when the player triggers the interaction
    void Interact();
}
