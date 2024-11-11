using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] ShowcaseSpot[] _showcaseSpots;

    [SerializeField] float _rotationAmount;
    [SerializeField] float _rotationDuration;

    int _currentSpotIndex = 0;

    public event Action _OnFinishRotation;
    public bool Rotating { get; private set; }

    void Awake()
    {
        _OnFinishRotation += () => Rotating = false;
    }
    public ShowcaseSpot GetNextSpot()
    {
        _currentSpotIndex = (_currentSpotIndex + 1) % _showcaseSpots.Length;

        return _showcaseSpots[_currentSpotIndex];
    }
    public ShowcaseSpot GetPreviousSpot()
    {
        _currentSpotIndex = (_currentSpotIndex - 1 + _showcaseSpots.Length) % _showcaseSpots.Length;

        return _showcaseSpots[_currentSpotIndex];
    }

    public void RotateToNextSpot()
    {
        Rotating = true;
        StartCoroutine(TransformUtilities.RotateOverTime(transform, _rotationAmount, _rotationDuration, _OnFinishRotation));
    }
    public void RotateToPreviousSpot()
    {
        Rotating = true;
        StartCoroutine(TransformUtilities.RotateOverTime(transform, -_rotationAmount, _rotationDuration, _OnFinishRotation));
    }


}
