using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform _startingPoint;
    //Private Variables:
    private PlayerController _playerController;
    private float _lerpSpeed = 5.0f;
    private int _score = 0;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerController._theGameIsOver = true;
        StartCoroutine(PlayIntro());
    }

    void Update()
    {
        if (_playerController._theGameIsOver == false)
        {
            if (_playerController._doubleSpeed) _score += 2;
            else _score++;

            Debug.Log($"Score: {_score}");
        }
    }

    IEnumerator PlayIntro() 
    {
        Vector3 startPos = _playerController.transform.position;
        Vector3 endPos = _startingPoint.position;
        float jorneyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        
        float distanceCovered = (Time.time - startTime) *_lerpSpeed;
        float fractionOfJourney = distanceCovered / jorneyLength;
        
        _playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);
        
        while (fractionOfJourney < 1) 
        {
            distanceCovered = (Time.time - startTime) * _lerpSpeed;
            fractionOfJourney = distanceCovered / jorneyLength;
            _playerController.transform.position = Vector3.Lerp(startPos,endPos, fractionOfJourney);
            yield return null;
        }

        _playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        _playerController._theGameIsOver = false;
    }
}
