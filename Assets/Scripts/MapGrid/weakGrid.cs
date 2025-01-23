using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakGrid : MonoBehaviour
{
    private Tilemap _tilemap;
    public void EraseTile(ContactPoint2D[] contactPoints)
    {
        foreach (var contact in contactPoints)
        {
            Vector2 contactPoint = contact.point;
            Vector3Int tilePosition = new Vector3Int(0, 0, 0);
            tilePosition.x = Mathf.RoundToInt(contactPoint.x);
            tilePosition.y = Mathf.RoundToInt(contactPoint.y);
            if (_tilemap.HasTile(tilePosition))
                _tilemap.SetTile(tilePosition, null);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        IPlayer currentPlayerForm = collision.gameObject.GetComponent<PlayerStateController>().GetPlayerStyle();
        if (currentPlayerForm == null || currentPlayerForm.GetPlayerState() != PlayerState.HARD) return;
        Rigidbody2D rigidbody2D = collision.gameObject.GetComponentInParent<Rigidbody2D>();
        if (rigidbody2D == null) return;
        ContactPoint2D[] contactPoints = collision.contacts;
        foreach (var contact in contactPoints)
        {
            Vector3Int tmp = FindClosestTile(contact.point);
            if (_tilemap.HasTile(tmp))
            {
                _tilemap.SetTile(tmp, null);
                _tilemap.RefreshAllTiles();
            }
            else
                Debug.LogWarning("How the fuck it even possible " + _tilemap.WorldToCell(contact.point) + " " + tmp.ToString());
        }
    }

    private Vector3Int FindClosestTile(Vector2 contactPoint)
    {
        Vector3Int tilePosition = _tilemap.WorldToCell(contactPoint);
        float minDistance = Mathf.Infinity;
        Vector3Int closestTilePosition = Vector3Int.zero;
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                if (xOffset == 0 && yOffset == 0) continue;

                Vector3Int neighborTilePosition = tilePosition + new Vector3Int(xOffset, yOffset, 0);
                if (!_tilemap.HasTile(neighborTilePosition)) continue;
                Vector3 neighborTileCenter = _tilemap.GetCellCenterWorld(neighborTilePosition);
                float distance = Vector2.Distance(contactPoint, neighborTileCenter);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTilePosition = neighborTilePosition;
                }
            }
        }
        return closestTilePosition;
    }


    private void Awake()
    {
        _tilemap = gameObject.GetComponent<Tilemap>();
    }
}
