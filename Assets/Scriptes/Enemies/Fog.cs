using System;
using System.Collections;
using UnityEngine;

public class Fog : MonoBehaviour
{

    private float _lifeTime;
    private ITile _tile;
    private ParticleSystem _particle;

    public event Action<Fog> Destriction;

    public void Initialization(ITile tile, float lifeTime)
    {
        _tile = tile;
        Vector3 position = tile.GetPosition();
        transform.position = new Vector3(position.x, position.y + 1, position.z);
        _particle = GetComponent<ParticleSystem>();
        _lifeTime = lifeTime;

        _particle.Play();
        StartCoroutine(DestroyTile());
    }

    private IEnumerator DestroyTile()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destriction?.Invoke(this);

        Destroy(gameObject);
    }
}
