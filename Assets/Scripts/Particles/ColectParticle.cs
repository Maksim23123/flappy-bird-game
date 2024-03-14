using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectParticle : MonoBehaviour
{
    private ParticleSystem objectParticleSystem;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        objectParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        
        int triggeredParticles = objectParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.remainingLifetime = 0;
            particles[i] = particle;
        }

        objectParticleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }


}
