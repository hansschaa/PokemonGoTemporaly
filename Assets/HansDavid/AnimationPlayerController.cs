using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    IDLE, WALK
}

public class AnimationPlayerController : MonoBehaviour
{

    Animator _animator;
    Vector3 _currentPosition;
    Vector3 _lastPosition;
    float _distance;
    PlayerAction _playerAction;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentPosition = transform.position;
        _lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = transform.position;
        _distance = Vector2.Distance(_currentPosition, _lastPosition);
        //Correr
        if(_distance != 0 && _playerAction == PlayerAction.IDLE)
        {
            _playerAction = PlayerAction.WALK;
            _animator.SetInteger("Velocity", 1);
        }

        else if(_distance == 0 && _playerAction == PlayerAction.WALK)
        {
            _playerAction = PlayerAction.IDLE;
            _animator.SetInteger("Velocity", 0);
        }

        _lastPosition = _currentPosition;

    }
}
