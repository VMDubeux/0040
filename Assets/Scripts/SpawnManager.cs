using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Main GameObject:")]
    public GameObject ObjectPrefab;

    //*Private Variables*:
    
    //Player Controller script:
    private PlayerController _playerController;
    
    //Vector3 -> position Background:
    private Vector3 _startPos = new Vector3(37, 0.9f, 0);
    
    //InvokeRepeating times:
    [SerializeField] private float _startDelay = 2.0f;
    [SerializeField] private float _timeToRepeat = 2.0f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", _startDelay, _timeToRepeat);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (_playerController._theGameIsOver == false)
        {
            Vector3 _objectRot = new Vector3(6.2f, -180, 0);
            Instantiate(ObjectPrefab, _startPos, Quaternion.Euler(_objectRot));
        }
    }
}
