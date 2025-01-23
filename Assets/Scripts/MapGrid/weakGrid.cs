using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakGrid : MonoBehaviour
{
    private Tilemap _tilemap;
    private readonly int[,] _directions = new int[,] {
        { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 },{ 1, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 } };
    public void DestroySingleTile(Vector3 playerPosition)
    {
        Vector3Int tilePosition = FindClosestTile(playerPosition);
        _tilemap.SetTile(tilePosition, null);
    }
    private void Awake()
    {
        _tilemap = gameObject.GetComponent<Tilemap>();
    }
    public Vector3Int FindClosestTile(Vector3 pos)
    {
        Vector3Int tilePosPlayer = _tilemap.WorldToCell(pos);
        float dist = Mathf.Infinity;
        Vector3Int closestTilePos = new Vector3Int();
        for (int i = 0; i < 8; i++)
        {
            Vector3Int tilePos = tilePosPlayer + new Vector3Int(_directions[i, 0], _directions[i, 1], 0);
            if (!_tilemap.HasTile(tilePos)) continue;
            if (dist > Vector3.Distance(pos, tilePos))
            {
                dist = Vector3.Distance(pos, tilePos);
                closestTilePos = tilePos;
            }
        }
        return closestTilePos;
    }
}
