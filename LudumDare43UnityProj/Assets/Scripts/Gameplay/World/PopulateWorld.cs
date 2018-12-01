using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

public class PopulateWorld : MonoBehaviour {
    [SerializeField] private Tile waterTile, lavaTile, sandTile, dirtTile, grassTile;
    [SerializeField] private int waterWidth, sandWidth;
    [SerializeField] private int worldSize;
    [SerializeField] private int maxHeight;

    [SerializeField] private float volcanoFalloffChance;

    private int halfSize;

    private static Tile[,] worldArray;
    private World world;

    private GameObject[,] tiles;

    private void Awake()
    {
        worldArray = new Tile[worldSize, worldSize];
        world = new World(worldArray);
    }

    void Start () {
        halfSize = worldSize / 2;

        for (int x = 0; x < worldSize; x++)
        {
            for (int z = 0; z < worldSize; z++)
            {
                Tile thisTile = SetTileType(x, z);

                world[x, z] = thisTile;

                Tile tile = Instantiate(world[x, z], new Vector3(x - halfSize, thisTile.Height, z - halfSize), Quaternion.identity, transform);
                tile.Position = new Vector2Int(x, z);
                worldArray[x, z] = tile;
            }
        }

        SetVolcanoHeights();
    }

    private class TileToProcess
    {
        public Tile tile;
        public float height;
    }

    private Tile SetTileType(int x, int z)
    {
        Tile tile;
        int sandEdge = waterWidth + sandWidth;
        if (worldSize - z < waterWidth || worldSize - x < waterWidth || x < waterWidth || z < waterWidth)
        {
            tile = waterTile;
        }
        else if ((worldSize - z < sandEdge && worldSize - z > waterWidth) || (worldSize - x < sandEdge && worldSize - x > waterWidth) ||
                (x < sandEdge && x > waterWidth) || (z < sandEdge && z > waterWidth))
        {
            tile = sandTile;
        }
        else if (worldSize - z == waterWidth || worldSize - x == waterWidth || x == waterWidth || z == waterWidth)
        {
            int randomChance = Random.Range(1, 3);
            if (randomChance == 2)
            {
                tile = sandTile;
            }
            else
            {
                tile = waterTile;
            }
        }
        else if (worldSize - z == sandEdge || worldSize - x == sandEdge || x == sandEdge || z == sandEdge)
        {
            int randomChance = Random.Range(1, 3);
            if (randomChance == 2)
            {
                tile = sandTile;
            }
            else
            {
                tile = grassTile;
            }
        }
        else
        {
            tile = grassTile;
        }

        return tile;
    }

    private void SetVolcanoHeights()
    {
        HashSet<Tile> tilesQueuedForProcess = new HashSet<Tile>();
        Queue<TileToProcess> tilesToProcess = new Queue<TileToProcess>();
        Tile center = worldArray[halfSize, halfSize];
        tilesToProcess.Enqueue(new TileToProcess { height = maxHeight, tile = center } );
        tilesQueuedForProcess.Add(center);
        System.Random rand = new System.Random();

        while(tilesToProcess.Count > 0)
        {
            ProcessVolcanoTile(tilesToProcess, tilesQueuedForProcess, rand);
        }
    }

    private void ProcessVolcanoTile(Queue<TileToProcess> tilesToProcess, HashSet<Tile> previouslyQueuedTiles, System.Random rand)
    {
        TileToProcess t = tilesToProcess.Dequeue();
        if(rand.NextDouble() < volcanoFalloffChance)
        {
            t.tile.Height = t.height - .5f;
        }
        else
        {
            t.tile.Height = t.height;
        }
        if(t.tile.Height <= .4f)
        {
            return;//no more to do
        }

        for(int i = Mathf.Max(0, t.tile.Position.x - 1); i < Mathf.Min(t.tile.Position.x + 2, worldSize); i++)
        {
            for (int j = Mathf.Max(0, t.tile.Position.y - 1); j < Mathf.Min(t.tile.Position.y + 2, worldSize); j++)
            {
                if(!previouslyQueuedTiles.Contains(worldArray[i,j]))
                {
                    tilesToProcess.Enqueue(new TileToProcess { height = t.tile.Height, tile = worldArray[i,j] });
                    previouslyQueuedTiles.Add(worldArray[i, j]);
                }
            }
        }

    }
}
