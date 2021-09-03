using System;
using UnityEngine;

public class DungeonDescentHandler : MonoBehaviour
{
    public Dungeon dungeon;
    public event Action OnDungeonGenerated;

    [SerializeField] GameObject room;
    [SerializeField] GameObject player;

    [SerializeField] int levelsAmount;
    [SerializeField] int minIterations;
    [SerializeField] int maxIterations;

    [SerializeField] GameObject floorTileModel;
    [SerializeField] GameObject wallTileModel;
    [SerializeField] GameObject ceilingTileModel;

    [SerializeField] GameObject enterTileModel;
    [SerializeField] GameObject exitTileModel;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player.layer)
        {
            // ���������� ����� ����������.
            dungeon = new Dungeon(levelsAmount, minIterations, maxIterations, floorTileModel, wallTileModel, ceilingTileModel, enterTileModel, exitTileModel);
            dungeon.DungeonCompletedEvent += () =>
            {
                room.SetActive(true);
                PlayerSettings.TeleportPlayer(player, PlayerSettings.InitialPlayerRoomPosition);
            };
            OnDungeonGenerated?.Invoke();

            // �������� ������ ������.
            PlayerSettings.TeleportPlayer(player, PlayerSettings.InitialPlayerDungeonPosition);

            // ��������� �������.
            room.SetActive(false);
        }
    }
}