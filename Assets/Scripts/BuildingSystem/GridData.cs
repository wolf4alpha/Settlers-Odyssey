using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridData
{
    Dictionary<Vector3, PlacementData> placedObjects = new();
    
    public void AddObjectAt(Vector3Int gridPosition, Vector2 objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionsToOccupy, ID, placedObjectIndex);
        foreach(var position in positionsToOccupy)
        {
            if(placedObjects.ContainsKey(position))
            { 
                Debug.LogError($"Position {position} is already occupied");
                return;
            }
            placedObjects[position] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2 objectSize)
    {
        List<Vector3Int> returnValues = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for(int y = 0; y < objectSize.y; y++)
            {
                returnValues.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
    return returnValues;
    }

    public bool CanPlaceObjectAT(Vector3Int gridPosition, Vector2 objectSize)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach(var position in positionsToOccupy)
        {
            if(placedObjects.ContainsKey(position))
            {
                return false;
            }
        }
        return true;
    }

}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int id { get; private set; }

    public int PlacedObjectIndex{ get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        this.id = id;
        PlacedObjectIndex = placedObjectIndex;
    }
}
