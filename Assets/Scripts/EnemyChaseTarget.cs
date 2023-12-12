using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTarget : MonoBehaviour
{
    public bool PlayerInside => _playerInside;
    public float PlayerEnterTime => _playerEnterTime;
    public PlayerController Player => _player;
    public BasicToiletEnemyController Enemy => _enemy;


    private bool _playerInside;
    private float _playerEnterTime;
    private PlayerController _player;
    private BasicToiletEnemyController _enemy;

    public void Init(BasicToiletEnemyController enemy)
    {
        _enemy = enemy;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = true;
            _playerEnterTime = Time.time;
            _player = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = false;
            _player = null;
        }
    }
}
