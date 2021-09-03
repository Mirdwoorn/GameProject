using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] DungeonDescentHandler descentHandler;
    private Dungeon dungeon;

    private string exitTileLayer;

    void Awake()
    {
        image.enabled = false;

        descentHandler.OnDungeonGenerated += () =>
        {
            dungeon = descentHandler.dungeon;
            exitTileLayer = dungeon.ExitTileLayer;
        };
    }

    void OnTriggerEnter(Collider other)
    {
        // Когда игрок прыгает в яму:
        if (other.gameObject.layer == LayerMask.NameToLayer(exitTileLayer))
        {
            image.enabled = true;

            PlayerSettings.TeleportPlayer(gameObject, PlayerSettings.InitialPlayerDungeonPosition);

            dungeon.GenerateNewLevel();

            StartCoroutine(Delay(1));
        }
    }

    IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        image.enabled = false;
    }
}