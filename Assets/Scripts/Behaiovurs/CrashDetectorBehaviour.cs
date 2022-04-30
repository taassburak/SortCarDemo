using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Manager;
public class CrashDetectorBehaviour : MonoBehaviour
{

    private GameManager _gamemanager;

    public void Initialize(GameManager gamemanager)
    {
        _gamemanager = gamemanager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightCar"))
        {
            _gamemanager.EventManager.LevelFailed();
        }
    }
}
