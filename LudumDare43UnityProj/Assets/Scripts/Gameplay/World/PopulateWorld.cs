using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

public class PopulateWorld : MonoBehaviour {
    [SerializeField] private Tile waterTile;
    [SerializeField] private Tile lavaTile;
    [SerializeField] private Tile sandTile;
    [SerializeField] private Tile dirtTile;
    [SerializeField] private Tile grassTile;

    [SerializeField] private int waterWidth;
    [SerializeField] private int sandWidth;
    [SerializeField] private int worldSize;
    [SerializeField] private int maxHeight;

    [SerializeField] private float volcanoFalloffChance;

    private int halfSize;

    private GameObject[,] tiles;

    public World World { get; private set; }

    private void Awake()
    {
        World = new World(worldSize, worldSize);
    }

    void Start () {
        halfSize = worldSize / 2;

        for (int x = 0; x < worldSize; x++)
        {
            for (int z = 0; z < worldSize; z++)
            {
                Tile thisTile = SetTileType(x, z);

                World[x, z] = thisTile;

                Tile tile = Instantiate(World[x, z], new Vector3(x - halfSize, thisTile.Height, z - halfSize), Quaternion.identity, transform);
                tile.Position = new Vector2Int(x, z);
                World[x, z] = tile;
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
        float volcanoEdgeMin = halfSize - maxHeight * 1.7f;
        float volcanoEdgeMax = halfSize + maxHeight * 1.7f;
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
        else if ((x > volcanoEdgeMin && x < volcanoEdgeMax) && (z > volcanoEdgeMin && z < volcanoEdgeMax))
        {
            int randomChance = Random.Range(1, 6);
            if (randomChance > 2)
            {
                tile = dirtTile;
            }
            else
            {
                tile = lavaTile;
            }
        }
        else if((x == volcanoEdgeMin && z >= volcanoEdgeMin && z <= volcanoEdgeMax) || (z == volcanoEdgeMin && x >= volcanoEdgeMin && x <= volcanoEdgeMax) ||
                (x == volcanoEdgeMax && z >= volcanoEdgeMin && z <= volcanoEdgeMax) || (z == volcanoEdgeMax && x >= volcanoEdgeMin && x <= volcanoEdgeMax))
        {
            int randomChance = Random.Range(1, 3);
            if (randomChance == 2)
            {
                tile = dirtTile;
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
        Tile center = World[halfSize, halfSize];
        tilesToProcess.Enqueue(new TileToProcess { height = maxHeight, tile = center });
        tilesQueuedForProcess.Add(center);
        System.Random rand = new System.Random();

        while (tilesToProcess.Count > 0)
        {
            ProcessVolcanoTile(tilesToProcess, tilesQueuedForProcess, rand);
        }
    }

    private void ProcessVolcanoTile(Queue<TileToProcess> tilesToProcess, HashSet<Tile> previouslyQueuedTiles, System.Random rand)
    {
        TileToProcess t = tilesToProcess.Dequeue();
        double randomChance = rand.NextDouble();
        if (randomChance < volcanoFalloffChance)
        {
            t.tile.Height = t.height - 0.5f;
        }
        else if (randomChance > volcanoFalloffChance && randomChance < 0.9f)
        {
            t.tile.Height = t.height - 1f;
        }
        else
        {
            t.tile.Height = t.height;
        }
        Transform child = t.tile.transform.GetChild(0);
        child.localScale = new Vector3(child.localScale.x, t.height * 2, child.localScale.z);
        if (t.tile.Height < 0.9)
        {
            return;//no more to do
        }

        for (int i = Mathf.Max(0, t.tile.Position.x - 1); i < Mathf.Min(t.tile.Position.x + 2, worldSize); i++)
        {
            for (int j = Mathf.Max(0, t.tile.Position.y - 1); j < Mathf.Min(t.tile.Position.y + 2, worldSize); j++)
            {
                if (!previouslyQueuedTiles.Contains(World[i, j]))
                {
                    tilesToProcess.Enqueue(new TileToProcess { height = t.tile.Height, tile = World[i, j] });
                    previouslyQueuedTiles.Add(World[i, j]);
                }
            }
        }

    }
}
