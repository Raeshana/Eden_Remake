

using UnityEngine;

public class InteractableThing : MonoBehaviour, IInteractable
{
    public string InteractionText => throw new System.NotImplementedException();

    public void Hover(Vector3 hitPoint)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Vector3 hitPoint)
    {
        throw new System.NotImplementedException();
    }
}