using UnityEngine;

public static class PlayerSettings
{
    public static Vector3 InitialPlayerRoomPosition { get; set; } = new Vector3(.5f, 3.0f, .5f);
    public static Vector3 InitialPlayerDungeonPosition { get; set; } = new Vector3(.5f, 1.0f, .5f);

    public static void TeleportPlayer(GameObject player, Vector3 newPosition)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = newPosition;
        controller.enabled = true;
    }
}