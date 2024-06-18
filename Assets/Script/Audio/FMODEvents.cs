using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience1 { get; private set; }
    [field: SerializeField] public EventReference ambience2 { get; private set; }

    [field: Header("Player")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference playerSlide { get; private set; }
    [field: SerializeField] public EventReference playerAttack1 { get; private set; }
    [field: SerializeField] public EventReference playerAttack2 { get; private set; }
    [field: SerializeField] public EventReference playerAttack3 { get; private set; }
    [field: SerializeField] public EventReference playerShoot { get; private set; }
    [field: SerializeField] public EventReference playerHit { get; private set; }

    [field: Header("Enemies")]
    [field: SerializeField] public EventReference privateAttack { get; private set; }
    [field: SerializeField] public EventReference corporalShoot { get; private set; }
    [field: SerializeField] public EventReference majorAttack { get; private set; }
    [field: SerializeField] public EventReference sergeantShoot { get; private set; }
    [field: SerializeField] public EventReference enemyHit { get; private set; }
    [field: SerializeField] public EventReference majorBusy { get; private set; }
    [field: SerializeField] public EventReference privateDeath { get; private set; }
    [field: SerializeField] public EventReference corporalDeath { get; private set; }
    [field: SerializeField] public EventReference majorDeath { get; private set; }
    [field: SerializeField] public EventReference sergeantDeath { get; private set; }


    [field: Header("Props")]
    [field: SerializeField] public EventReference checkpointSFX { get; private set; }
    [field: SerializeField] public EventReference respawnCheckpointSFX { get; private set; }
    [field: SerializeField] public EventReference cameraAlarm { get; private set; }
    [field: SerializeField] public EventReference barrier { get; private set; }

    [field: Header("Extras")]
    [field: SerializeField] public EventReference bulletImpact { get; private set; }

    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instnace in the scene");
        }

        instance = this;
    }
}
