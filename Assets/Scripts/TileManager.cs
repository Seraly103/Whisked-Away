using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
   
   [SerializeField] private Tilemap interactableTilemap;
   [SerializeField] private Tile hiddenTile;
   [SerializeField] private Tile hoeTile;
   [SerializeField] private Tile waterTile;
   [SerializeField] private Tile seedTile;
   void Start()
   {
       foreach (var pos in interactableTilemap.cellBounds.allPositionsWithin)
       {
           if (interactableTilemap.HasTile(pos))
           {
               interactableTilemap.SetTile(pos, hiddenTile);
           }
       }
   }

   public bool IsTileInteractable(Vector3 worldPosition)
   {
       Vector3Int cellPosition = interactableTilemap.WorldToCell(worldPosition);
       return interactableTilemap.HasTile(cellPosition);
   }

   public void SetTilledTile (Vector3 worldPosition)
   {
       Vector3Int cellPosition =
        interactableTilemap.WorldToCell(worldPosition);

        if (interactableTilemap.HasTile(cellPosition))
        {
            interactableTilemap.SetTile(cellPosition, hoeTile);
        }
    }

   public void SetWaterTile (Vector3 worldPosition)
   {
        Vector3Int cellPosition =
        interactableTilemap.WorldToCell(worldPosition);

        if (interactableTilemap.HasTile(cellPosition))
        {
            interactableTilemap.SetTile(cellPosition, waterTile);
        }
   }

   
    
}
