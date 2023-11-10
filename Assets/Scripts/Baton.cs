using System;
using UnityEngine;

[Serializable]
public class Baton {
    public Baton(float hitDamage, float secondsBetweenHits) {
        HitDamage = hitDamage;
        SecondsBetweenHits = secondsBetweenHits;
    }

    [field: SerializeField] public float HitDamage { get; private set; }
    [field: SerializeField] public float SecondsBetweenHits { get; private set; }
}