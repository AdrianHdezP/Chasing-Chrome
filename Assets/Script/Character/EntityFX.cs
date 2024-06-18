using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX Configs")]
    [SerializeField] private Material hitMat;
    [SerializeField] private Material invisibleMat;
    private Material originalMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        originalMat = sr.material;
    }

    public IEnumerator FlashFx()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originalMat;
    }

    public IEnumerator InvisibleFx(float duration)
    {
        sr.material = invisibleMat;
        yield return new WaitForSeconds(duration);
        sr.material = originalMat;
    }
}
