using UnityEngine;

public class GridInteractable : MonoBehaviour
{
    private enum state
    {
        placeMode,
        notPlaceMode
    }

    private string text;
    public GameObject cube, blockPrefab;
    public Grid grid;
    private state currState;
    
    void Awake()
    {
        currState = state.placeMode;
        Debug.Log("Place mode on");
    }

    public string InteractionText
    {
        get { return text; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // interactable
    public void Hover(Vector3 hitPoint)
    {
        switch (currState)
        {
            case state.placeMode:
                Debug.Log("In place mode");
                text = "Press E to place object";
                Vector3Int cellPosition = grid.WorldToCell(hitPoint);
                cube.transform.position = grid.GetCellCenterWorld(cellPosition);
                break;

            case state.notPlaceMode:
                Debug.Log("Not in place mode");
                break;
        }
    }

    // interactable
    public void Interact(Vector3 hitPoint)
    {
        switch (currState)
        {
            case state.placeMode:
                
                Instantiate(blockPrefab, cube.transform.position, Quaternion.identity);
                text = "Object placed.";
                break;
            
            case state.notPlaceMode:
                break;
        }
    }
}
