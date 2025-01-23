using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakGrid : MonoBehaviour
{
    private Tilemap _tilemap;
    private float _offset = 0.5f;
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

        ContactPoint2D[] contactPoints = collision.contacts;
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
    private void Awake()
    {
        _tilemap = gameObject.GetComponent<Tilemap>();
    }
}
