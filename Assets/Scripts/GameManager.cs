using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TileManager tileManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (tileManager == null)
        {
            tileManager = GetComponent<TileManager>();
        }

        if (tileManager == null)
        {
            tileManager = FindObjectOfType<TileManager>();
        }
    }
}
