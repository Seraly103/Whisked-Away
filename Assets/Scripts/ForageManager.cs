using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ForageManager : MonoBehaviour
{
    [SerializeField] private Tilemap forageTilemap;
    [SerializeField] private Tile hiddenTile;
    [SerializeField] private GameObject[] foragePrefabs;

    [SerializeField] private int amountToSpawn = 10;

    private List<Vector3Int> availablePositions = new List<Vector3Int>();

     void Start()
    {
        FindForagePositions();
        SpawnForage();

        foreach (var pos in forageTilemap.cellBounds.allPositionsWithin)
       {
           if (forageTilemap.HasTile(pos))
           {
               forageTilemap.SetTile(pos, hiddenTile);
           }
       }
    }

    void FindForagePositions()
    {
        foreach (var pos in forageTilemap.cellBounds.allPositionsWithin)
        {
            if (forageTilemap.HasTile(pos))
            {
                availablePositions.Add(pos);
            }
        }
    }

    void SpawnForage()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            if (availablePositions.Count == 0)
                return;

            int randomIndex = Random.Range(0, availablePositions.Count);

            Vector3Int cellPosition = availablePositions[randomIndex];

            Vector3 worldPosition =
                forageTilemap.GetCellCenterWorld(cellPosition);

            GameObject randomForage =
                foragePrefabs[Random.Range(0, foragePrefabs.Length)];

            Instantiate(randomForage, worldPosition, Quaternion.identity);

            availablePositions.RemoveAt(randomIndex);
        }
    } 
    

}
