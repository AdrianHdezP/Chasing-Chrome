using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graffiti : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject graffityOff;
    [SerializeField] private GameObject[] graffityOn;
    [SerializeField] private GameObject VFX;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void UseItem()
    {
        if (VFX.activeSelf == false)
            VFX.SetActive(true);

        Player player = gameManager.player;
        Vector3 playerDestination = new Vector3(transform.position.x, transform.position.y);

        if (graffityOff.activeSelf == true)
        {
            player.transform.position = playerDestination;

            if (player.stateMachine.currentState == player.graffitiState)
                return;

            player.stateMachine.ChangeState(gameManager.player.graffitiState);
        }

        StartCoroutine(ChangeSpriteAfterTime());

        if (VFX.activeSelf == true)
            Destroy(VFX, 2.1f);
    }

    private IEnumerator ChangeSpriteAfterTime()
    {
        yield return new WaitForSeconds(1);

        graffityOff.SetActive(false);

        int index = Random.Range(0, graffityOn.Length);
        graffityOn[index].SetActive(true);
    }

}
