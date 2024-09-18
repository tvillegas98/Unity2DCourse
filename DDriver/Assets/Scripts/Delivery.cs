using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField]
    Color32 hasPackageColor = new Color32(236, 0, 229, 255);

    [SerializeField]
    Color32 noPackageColor = new Color32(255, 255, 255, 255);

    [SerializeField]
    float packageDestroyDelay = 1f;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    bool hasPackage = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Package":
                pickUpPackage(other);
                break;
            case "Customer":
                deliverPackageToCustomer(other);
                break;
        }
    }

    private void pickUpPackage(Collider2D package)
    {
        if (!hasPackage)
        {
            Debug.Log($"Picked up {package.gameObject.name}");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(package.gameObject, packageDestroyDelay);
        }
        else
        {
            Debug.Log($"Already carrying a package");
        }
    }
    private void deliverPackageToCustomer(Collider2D customer){
        if (hasPackage)
        {
            Debug.Log($"Delivered to {customer.gameObject.name}");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
        else
        {
            Debug.Log($"No package to deliver to {customer.gameObject.name}");
        }
    }
}
