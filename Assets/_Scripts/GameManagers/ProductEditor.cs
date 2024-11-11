using TMPro;
using UnityEngine;

public class ProductEditor : MonoSingleton<ProductEditor>
{
    [SerializeField] GameObject _editCanvas;

    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] TMP_InputField _priceInput;
    [SerializeField] TMP_InputField _descriptionInput;

    [SerializeField] GameObject _confirmationPanel;
    bool _inEditMode = false;
    Product _product;
    ShowcaseSpot _spot;


    void Start()
    {
        StopEditMode();
    }

    public void ToggleEditMode()
    {
        if (_inEditMode)
        {
            StopEditMode();
        }
        else
        {
            StartEditMode();
        }
    }

    public void StartEditMode()
    {
        _spot = PlatformManager.Instance.CurrentSpot;
        _spot.StartEditMode();
        _product = _spot.GetProduct();

        _nameInput.text = _product.name;
        _priceInput.text = _product.price.ToString();
        _descriptionInput.text = _product.description;

        _editCanvas.SetActive(true);
        _inEditMode = true;
    }

    public void StopEditMode()
    {
        PlatformManager.Instance.CurrentSpot?.StopEditMode();

        _editCanvas.SetActive(false);
        _confirmationPanel.SetActive(false);
        _inEditMode = false;
    }

    public void SaveChanges()
    {
        _product.name = _nameInput.text;
        _product.price = float.Parse(_priceInput.text);
        _product.description = _descriptionInput.text;

        _spot.UpdateTextFields(_product);
        _confirmationPanel.SetActive(true);
    }
}
