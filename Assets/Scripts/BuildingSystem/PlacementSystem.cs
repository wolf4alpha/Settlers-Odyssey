using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;

    [SerializeField]    
    private InputManager inputManager;

    [SerializeField] 
    private NavMeshSurface surface;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private GridData floorData;

    [SerializeField]
    private GridData furnitureData;

    private List<GameObject> placedGameObjects = new();
    
    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition =Vector3Int.zero;


    private void Start()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();

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
        preview.StartShowingPlacementPreview(database.objectData[selectedObjectIndex].Prefab, database.objectData[selectedObjectIndex].Size);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValid = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValid == false)
            return;
        GameObject newObject = Instantiate(database.objectData[selectedObjectIndex].Prefab);
        Vector3 gridpos = grid.CellToWorld(gridPosition);
        newObject.transform.position = gridpos;
        placedGameObjects.Add(newObject);

        GridData selectedData = database.objectData[selectedObjectIndex].Id == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.objectData[selectedObjectIndex].Size,
            database.objectData[selectedObjectIndex].Id,
            placedGameObjects.Count - 1);

        //change y position to be on top of the terrain, not inside the grid height
        newObject.transform.position = new Vector3(gridpos.x, mousePosition.y, gridpos.z);

        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
        UpdateNavMesh();
    }

    private void UpdateNavMesh()
    {
       // surface.BuildNavMesh();
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
       GridData selectedData = database.objectData[selectedObjectIndex].Id == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjectAT(gridPosition, database.objectData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPlacementPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        if(selectedObjectIndex < 0)
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            bool placementValid = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            mouseIndicator.transform.position = mousePosition;
            Vector3 realPosition = new Vector3(gridPosition.x, mousePosition.y, gridPosition.z);
            realPosition = grid.CellToWorld(gridPosition);
            realPosition.y = mousePosition.y;
            preview.UpdatePosition(realPosition, placementValid);
            lastDetectedPosition = gridPosition;
        }

    }
}
