using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ProductManager : MonoSingleton<ProductManager>
{
    private const string apiUrl = "https://homework.mocart.io/api/products";

    public List<Product> ProductQueue { get; private set;} = new List<Product>();
    [SerializeField] SerializableDictionary<string, Product> _products;

    [SerializeField] string[] _ProductVisuals;

    public event Action OnFinishedProcessing;

    void Start()
    {
        //TODO load products from local disc

        StartCoroutine(GetProducts());
    }

    private IEnumerator GetProducts()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                ProcessProducts(jsonResponse);
            }
            else
            {
                Debug.LogError($"Request failed: {request.error}");
            }
        }
    }

    private void ProcessProducts(string json)
    {
        ProductResponse productResponse = JsonUtility.FromJson<ProductResponse>(json);

        foreach (var product in productResponse.products)
        {
            Debug.Log($"Name: {product.name}, Description: {product.description}, Price: {product.price}");
            string productID = product.name;
            if (_products.ContainsKey(productID))
            {
                AddProductToQueue(_products[productID]);
            }
            else
            {
                int modelIndex = UnityEngine.Random.Range(0, _ProductVisuals.Length);
                product.visualName = _ProductVisuals[modelIndex];
                _products.Add(productID, product);
                AddProductToQueue(product);
            }

        }
        OnFinishedProcessing?.Invoke();
    }

    public void AddProductToQueue(Product product)
    {
        if (!ProductQueue.Contains(product))
        {
            ProductQueue.Add(product);
        }
    }
}

[Serializable]
internal class ProductResponse
{
    public Product[] products;
}
