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
        cube.SetActive(false);
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
        cube.SetActive(true);
    }

    // Ref https://youtube.com/shorts/VPPRoNW6TQs?si=E6Ad0sVxuwwpvdZ6
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

                cube.SetActive(false);
                Instantiate(blockPrefab, cube.transform.position, Quaternion.identity);
                text = "Placement successful.";
                // transition to not place mode
                currState = state.notPlaceMode;
                Debug.Log("not in place mode anymore");
                //turn shop back on-- I don't like this
                // shopController.ShopOn();
                break;
            
            case state.notPlaceMode:
                text = "";
                break;
        }
    }
}
