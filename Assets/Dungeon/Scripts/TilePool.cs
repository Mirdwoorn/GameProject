using UnityEngine;

public class TilePool
{
    private GameObject[] tiles;
    private int currentTileIndex;

    public TilePool(int tilesAmount, GameObject tileModel)
    {
        tiles = new GameObject[tilesAmount];
        InstantiateAndDisable(tiles, tileModel);
    }
    private void InstantiateAndDisable(GameObject[] tiles, GameObject tileModel)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = Object.Instantiate(tileModel);
            tiles[i].SetActive(false);
        }
    }

    private GameObject HandleTile(bool active, GameObject[] tiles, ref int currentTileIndex, Vector3 position = default, Vector3 rotation = default)
    {
        GameObject tile = tiles[currentTileIndex];
        tile.SetActive(active);

        if (active)
        {
            tile.transform.position = position;
            tile.transform.eulerAngles = rotation;
            tile.isStatic = true;

            currentTileIndex++;
        }
        else
        {
            if (currentTileIndex > 0)
                currentTileIndex--;
        }

        return tile;
    }

    public GameObject EnableTile(Vector3 position = default, Vector3 rotation = default) => HandleTile(true, tiles, ref currentTileIndex, position, rotation);
    private void DisableTile() => HandleTile(false, tiles, ref currentTileIndex);
    public void DisableAllTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
            DisableTile();
    }
    public void DestroyAllTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
            Object.Destroy(tiles[i].gameObject);
    }
}