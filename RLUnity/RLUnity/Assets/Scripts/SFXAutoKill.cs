using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Automatically destroy a particle effect once it has completed
/// </summary>
public class SFXAutoKill : MonoBehaviour
{
    /// <summary>
    /// The particle system
    /// </summary>
    private ParticleSystem _Particles = null;

    // Start is called before the first frame update
    void Start()
    {
        _Particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy this game object when the particle system is finished
        if (_Particles != null && !_Particles.IsAlive()) Destroy(this);
    }
}
