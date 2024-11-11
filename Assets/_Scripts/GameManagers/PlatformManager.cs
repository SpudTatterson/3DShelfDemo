using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoSingleton<PlatformManager>
{
    [SerializeField] Platform _platform;

    public ShowcaseSpot CurrentSpot {get; private set;}
    ShowcaseSpot _previousSpot;

    int _currentProductIndex = 0;
    void OnEnable()
    {
        ProductManager.Instance.OnFinishedProcessing += ShowInitialItem;

        _platform._OnFinishRotation += DisablePrevious;
    }
    void OnDisable()
    {
        ProductManager.Instance.OnFinishedProcessing -= ShowInitialItem;

        _platform._OnFinishRotation -= DisablePrevious;
    }

    public void ShowInitialItem()
    {
        if (_platform.Rotating) return;
        _previousSpot = CurrentSpot;

        Product product = ProductManager.Instance.ProductQueue[_currentProductIndex];
        CurrentSpot = _platform.GetNextSpot();
        CurrentSpot.Initialize(product);
        _platform.RotateToNextSpot();
    }
    public void ShowPreviousItem()
    {
        if (_platform.Rotating) return;
        _previousSpot = CurrentSpot;

        int productCount = ProductManager.Instance.ProductQueue.Count;
        _currentProductIndex = (_currentProductIndex - 1 + productCount) % productCount;

        Product product = ProductManager.Instance.ProductQueue[_currentProductIndex];
        CurrentSpot = _platform.GetPreviousSpot();
        CurrentSpot.Initialize(product);
        _platform.RotateToPreviousSpot();
    }
    public void ShowNextItem()
    {
        if (_platform.Rotating) return;
        _previousSpot = CurrentSpot;

        _currentProductIndex = (_currentProductIndex + 1) % ProductManager.Instance.ProductQueue.Count;

        Product product = ProductManager.Instance.ProductQueue[_currentProductIndex];
        CurrentSpot = _platform.GetNextSpot();
        CurrentSpot.Initialize(product);
        _platform.RotateToNextSpot();
    }

    void DisablePrevious()
    {
        _previousSpot?.Disable();
    }

    
}
