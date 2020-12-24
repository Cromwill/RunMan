using System;
using System.Collections;
using UnityEngine;

public class Fog : MonoBehaviour
{

    private float _lifeTime;
    private float _saveTime;
    private ITile _tile;
    private ParticleSystem _particle;
    private Collider _selfColider;

    public event Action<Fog> Destriction;

    public void Initialization(ITile tile, float lifeTime, float saveTime)
    {
        _tile = tile;
        Vector3 position = tile.GetPosition();
        transform.position = new Vector3(position.x, position.y + 1, position.z);
        _particle = GetComponent<ParticleSystem>();
        _selfColider = GetComponent<Collider>();
        _lifeTime = lifeTime;
        _saveTime = saveTime;
        _particle.Play();
        StartCoroutine(DestroyTile());
        StartCoroutine(DeadZoneActive());
    }

    public void Clear()
    {
        StopCoroutine(DestroyTile());
        StopCoroutine(DeadZoneActive());
        DestroyFog();
    }

    private IEnumerator DestroyTile()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyFog();
    }

    private IEnumerator DeadZoneActive()
    {
        yield return new WaitForSeconds(_saveTime);
        _selfColider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.GetComponent<IDeadable>() != null)
            {
                other.gameObject.GetComponent<IDeadable>().Dead();
            }
    }

    private void DestroyFog()
    {
        Destriction?.Invoke(this);
        Destroy(gameObject);
    }
}
