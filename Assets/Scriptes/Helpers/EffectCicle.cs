using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EffectCicle : MonoBehaviour
{
    private ParticleSystem _particle;
    private void OnEnable()
    {
        _particle = GetComponent<ParticleSystem>();
        _particle.Play();
        StartCoroutine(DeleteEffect());
  
    }

    private IEnumerator DeleteEffect()
    {
        while(_particle.isPlaying)
        {
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
