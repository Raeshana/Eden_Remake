using UnityEngine;

public class GridInteractable : MonoBehaviour, IInteractable
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

    private ShopController shopController;
    
    void Awake()
    {
        currState = state.notPlaceMode;
        Debug.Log("Place mode off");
        shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
    }

    public string InteractionText
    {
        get { return text; }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnterPlaceMode()
    {
        currState = state.placeMode;
    }

    // interactable
    public void Hover(Vector3 hitPoint)
    {
        // Debug.Log("hover " + hitPoint + " state " + currState);
        switch (currState)
        {
            case state.placeMode:
                // Debug.Log("In place mode");
                // cube.GetComponent<MeshFilter>().mesh = blockPrefab.mesh;
                text = "Press E to place object";
                Vector3Int cellPosition = grid.WorldToCell(hitPoint);
                cube.transform.position = grid.GetCellCenterWorld(cellPosition);
                break;

            case state.notPlaceMode:
                // Debug.Log("Not in place mode");
                text = "";
                break;
        }
    }

    // interactable
    public void Interact(Vector3 hitPoint)
    {
        // Debug.Log("interqact " + hitPoint + " state " + currState);

        switch (currState)
        {
            case state.placeMode:
                
                Instantiate(blockPrefab, cube.transform.position, Quaternion.identity);
                text = "Object placed.";
                // transition to not place mode
                currState = state.notPlaceMode;
                Debug.Log("not in place mode anymore");
                //turn shop back on
                shopController.ShopOn();
                break;
            
            case state.notPlaceMode:
                text = "";
                break;
        }
    }
}
