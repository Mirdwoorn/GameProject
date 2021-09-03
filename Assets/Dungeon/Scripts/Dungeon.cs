using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Dungeon
{
    public event Action DungeonCompletedEvent;

    private int levelsAmount;
    private int minIterations;
    private int maxIterations;

    private int iterations;
    private int iterationsAddition;
    private int levelCounter;

    public string ExitTileLayer = "ExitTile";

    private Vector3[] floorCoords;

    // В целях оптимизации :)
    private TilePool floorTilePool;
    private TilePool wallTilePool;
    private TilePool ceilingTilePool;

    private Tile enterTile;
    private Tile exitTile;

    public Dungeon(int levelsAmount, int minIterations, int maxIterations, GameObject floorTileModel, GameObject wallTileModel, GameObject ceilingTileModel, GameObject enterTileModel, GameObject exitTileModel)
    {
        this.levelsAmount = levelsAmount;
        this.minIterations = minIterations;
        this.maxIterations = maxIterations;

        floorTilePool = new TilePool(maxIterations, floorTileModel);
        wallTilePool = new TilePool(maxIterations * 3, wallTileModel);
        ceilingTilePool = new TilePool(maxIterations, ceilingTileModel);

        enterTile = new Tile(enterTileModel);
        exitTile = new Tile(exitTileModel);

        iterations = minIterations;

        GenerateNewLevel();
    }

    public void GenerateNewLevel()
    {
        DisablePreviousLevel();

        if (levelCounter == levelsAmount)
        {
            // Полностью удаляем все созданные объекты для подземелья объекты.
            DungeonCompletedEvent += () =>
            {
                floorTilePool.DestroyAllTiles();
                wallTilePool.DestroyAllTiles();
                ceilingTilePool.DestroyAllTiles();

                enterTile.DestroyTile();
                exitTile.DestroyTile();
            };
            DungeonCompletedEvent.Invoke();
            return;
        }
        levelCounter++;

        iterations += iterationsAddition;

        if (iterationsAddition == 0)
            iterationsAddition = (maxIterations - minIterations) / (levelsAmount - 1);

        floorCoords = GenerateFloorCoords(iterations);

        EnableFloorTiles();
        EnableWallTiles();
        EnableCeilingTiles();
    }
    private void DisablePreviousLevel()
    {
        floorTilePool.DisableAllTiles();
        wallTilePool.DisableAllTiles();
        ceilingTilePool.DisableAllTiles();
    }

    private Vector3[] GenerateFloorCoords(int iterations)
    {
        List<Vector3> floorCoords = new List<Vector3>();

        Vector3 coords = new Vector3(.0f, 1.0f, .0f);
        for (int i = 0; i < iterations; i++)
        {
            floorCoords.Add(coords);

            int direction = UnityEngine.Random.Range(0, 4);
            switch (direction)
            {
                case 0:
                    coords.x++;
                    break;
                case 1:
                    coords.z++;
                    break;
                case 2:
                    coords.x--;
                    break;
                case 3:
                    coords.z--;
                    break;
            }
        }
        return floorCoords.Distinct().ToArray();
    }
    #region Objects Instantiation
    private void EnableFloorTiles()
    {
        GameObject ExitTile = exitTile.ChangeTileParams(floorCoords[floorCoords.Length - 1]);
        ExitTile.layer = LayerMask.NameToLayer(ExitTileLayer);

        for (int i = 0; i < floorCoords.Length - 1; i++)
            floorTilePool.EnableTile(floorCoords[i]);
    }
    private void EnableWallTiles()
    {
        List<Vector3> floorCoordsList = floorCoords.ToList();

        for (int i = 0; i < floorCoords.Length; i++)
        {
            if (floorCoordsList.Find(tile => (tile.x == floorCoords[i].x + 1.0f) && (tile.z == floorCoords[i].z)) == Vector3.zero)
                wallTilePool.EnableTile(new Vector3(floorCoords[i].x + 1.0f, floorCoords[i].y, floorCoords[i].z + 1.0f), new Vector3(.0f, 90.0f, .0f));

            if (floorCoordsList.Find(tile => (tile.x == floorCoords[i].x - 1.0f) && (tile.z == floorCoords[i].z)) == Vector3.zero)
                wallTilePool.EnableTile(new Vector3(floorCoords[i].x, floorCoords[i].y, floorCoords[i].z), new Vector3(.0f, -90.0f, .0f));

            if (floorCoordsList.Find(tile => (tile.x == floorCoords[i].x) && (tile.z == floorCoords[i].z + 1.0f)) == Vector3.zero)
                wallTilePool.EnableTile(new Vector3(floorCoords[i].x, floorCoords[i].y, floorCoords[i].z + 1.0f));

            if (floorCoordsList.Find(tile => (tile.x == floorCoords[i].x) && (tile.z == floorCoords[i].z - 1.0f)) == Vector3.zero)
                wallTilePool.EnableTile(new Vector3(floorCoords[i].x + 1.0f, floorCoords[i].y, floorCoords[i].z), new Vector3(.0f, 180.0f, .0f));
        }
    }
    private void EnableCeilingTiles()
    {
        enterTile.ChangeTileParams(floorCoords[0]);

        for (int i = 1; i < floorCoords.Length; i++)
            ceilingTilePool.EnableTile(floorCoords[i]);
    }
    #endregion
}