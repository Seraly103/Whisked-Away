using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TileManager tileManager;

    private void Awake()
    {
        tileManager = GetComponent<TileManager>();
    }
}
