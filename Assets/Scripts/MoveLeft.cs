using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float _speedMoveLeft;
    private int _leftBound = -18;
    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        if (_playerController._theGameIsOver == false)
        {
            transform.Translate(Vector3.left * _speedMoveLeft * Time.deltaTime);
        }

        if (gameObject.CompareTag("Obstacle") && transform.position.x < _leftBound) 
        {
            Destroy(gameObject);
        }
    }
}
