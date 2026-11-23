using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    public GameObject cube, blockPrefab;
    public Grid grid;
    public Interactable playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        // Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        // cube.transform.position = grid.GetCellCenterWorld(cellPosition);

        // if (gridInput.GetPlacementInput())
        // {
        //     Instantiate(blockPrefab, cube.transform.position, Quaternion.identity);
        // }
    }
}
