using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
   
   [SerializeField] private Tilemap interactableTilemap;
   [SerializeField] private Tile hiddenTile;

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

}
