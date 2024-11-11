using TMPro;
using UnityEngine;

public class ShowcaseSpot : MonoBehaviour
{
    [Header("Edit Mode Settings")]
    [SerializeField] Vector3 _editPosition;
    [SerializeField] float _moveDuration = 1;

    Vector3 _initialPosition;

    [Header("References")]
    [SerializeField] TextMeshPro _productNameText;
    [SerializeField] TextMeshPro _productPriceText;
    [SerializeField] TextMeshPro _productDescriptionText;
    [SerializeField] GameObject _itemSpawnPoint;
    [SerializeField] DragRotator _rotator;
    Product _product;
    GameObject _visual;

    void Awake()
    {
        _initialPosition = _itemSpawnPoint.transform.localPosition;
    }

    public void Initialize(Product product)
    {
        this._product = product;
        UpdateTextFields(product);

        _rotator.ResetRotator();
        _rotator.gameObject.SetActive(true);

        if (_visual != null) Destroy(_visual);
        _itemSpawnPoint.SetActive(true);
        GameObject visualModel = Resources.Load<GameObject>(product.visualName);
        _visual = Instantiate(visualModel, _itemSpawnPoint.transform, false);
    }

    public void UpdateTextFields(Product product)
    {
        _productNameText.text = product.name;
        _productPriceText.text = $"Price: {product.price}";
        _productDescriptionText.text = product.description;
    }

    public void Disable()
    {
        _itemSpawnPoint.SetActive(false);
        _rotator.gameObject.SetActive(false);
    }

    public void StartEditMode()
    {
        StartCoroutine(TransformUtilities.LocalMoveOverTime(_itemSpawnPoint.transform, _editPosition, _moveDuration));
    }

    public void StopEditMode()
    {
        StartCoroutine(TransformUtilities.LocalMoveOverTime(_itemSpawnPoint.transform, _initialPosition, _moveDuration));
    }

    public Product GetProduct()
    {
        return _product;
    }
}
