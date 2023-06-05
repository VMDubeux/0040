using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float _speedMoveLeft = 10;
    private int _leftBound = -18;
    private PlayerController _playerController;
    private GameObject _obstacles;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        if (_playerController._theGameIsOver == false)
        {
            if (_playerController._doubleSpeed == true) transform.Translate(Vector3.left * (_speedMoveLeft * 2.0f) * Time.deltaTime, Space.World);
            else transform.Translate(Vector3.left * _speedMoveLeft * Time.deltaTime, Space.World);
        }
        if (gameObject.CompareTag("Obstacle") && transform.position.x < _leftBound) Destroy(gameObject);
    }
}
