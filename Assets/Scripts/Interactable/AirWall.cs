using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    public Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(ShowAirWall());
    }

    private IEnumerator ShowAirWall()
    {
        yield return new WaitForSeconds(0.5f);
        coll.enabled = true;
    }
}
