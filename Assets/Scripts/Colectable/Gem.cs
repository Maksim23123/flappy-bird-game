using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class Gem : Colectable
{
    private readonly ColectableType colectableType = ColectableType.Gem;

    [SerializeField]
    private GameObject particleSys;

    

    private void OnTriggerEnter(Collider other)
    {
        PickUpThis(new PickUpColectableEventArgs(colectableType, value));

        if (Instantiate(particleSys, transform.position, Quaternion.identity)
                .TryGetComponent<ParticleSystem>(out ParticleSystem particles) && GameUIManager.particlesColector != null)
            particles.trigger.AddCollider(GameUIManager.particlesColector);

        Destroy(gameObject);
    }
}
