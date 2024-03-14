using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class Coin : Colectable
{
    private readonly ColectableType colectableType = ColectableType.Coin;

    private void OnTriggerEnter(Collider other)
    {
        PickUpThis(new PickUpColectableEventArgs(colectableType, value));
        Destroy(gameObject);
    }
}
