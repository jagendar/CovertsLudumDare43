using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

public class PopulateWorld : MonoBehaviour {
    [SerializeField] private Tile waterTile, lavaTile, sandTile, dirtTile, grassTile;

    [SerializeField] private int worldSize;

    private static Tile[,] worldArray;
    private World world;

    private void Awake()
    {
        worldArray = new Tile[worldSize, worldSize];
        world = new World(worldArray);
    }

    void Start () {
        Tile thisTile = dirtTile;
        for(int x = 0; x < worldSize; x++)
        {
            for(int z = 0; z < worldSize; z++)
            {
                thisTile.Height = 1;
                thisTile.transform.position = new Vector3(x, thisTile.Height, z);
                world[x, z] = thisTile;
                Instantiate(world[x, z]);
            }
        }
	}
}
