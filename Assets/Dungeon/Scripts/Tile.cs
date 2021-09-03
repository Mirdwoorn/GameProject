using UnityEngine;

// TilePool используется для создания подземелия, Tile - "уникальных" объектов (EnterTile, ExitTile...).
public class Tile
{
    private GameObject tile;
    public Tile(GameObject tileModel)
    {
        tile = Object.Instantiate(tileModel);
        tile.isStatic = true;
    }

    public GameObject ChangeTileParams(Vector3 position = default, Vector3 rotation = default)
    {
        tile.transform.position = position;
        tile.transform.eulerAngles = rotation;
        return tile;
    }
    public void DestroyTile() => Object.Destroy(tile);
}