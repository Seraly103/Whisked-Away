using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
   
   [SerializeField] private Tilemap interactableTilemap;

   [SerializeField] private Tile hiddenInteractableTilemap;

   void Start()
    {
        foreach(var position in interactableTilemap.cellBounds.allPositionsWithin)
        {
            interactableTilemap.SetTile(position, hiddenInteractableTilemap);
        }
    }

    public bool IsInteractableTile(Vector3 worldPosition)
    {
        TileBase tile = interactableTilemap.GetTile(interactableTilemap.WorldToCell(worldPosition));
        if(tile != null)
        {
            if(tile.name =="InteractableTile")
            {
                return true;
            }
        }
        return false;
    }
}
