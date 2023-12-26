using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;

    [SerializeField]
    private GameObject cellIndicator;

    [SerializeField]    
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
    }
    public void Test()
    {
        Debug.Log("Test");
    }

    public void StartPlacement(int objectIndex)
    {
        StopPlacement();
        selectedObjectIndex = database.objectData.FindIndex(data => data.Id == objectIndex);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"Object with index {objectIndex} not found in database");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
            return;
        
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        
        GameObject newObject = Instantiate(database.objectData[selectedObjectIndex].Prefab) ;
        Vector3 gridpos = grid.CellToWorld(gridPosition);
        newObject.transform.position = new Vector3(gridpos.x, mousePosition.y, gridpos.z);


    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if(selectedObjectIndex < 0)
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        
        
    }
}
