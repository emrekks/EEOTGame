using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public int item1;
    [SerializeField] public int item2;
    [SerializeField] public float pickupTime = 2f;
    [SerializeField] public RectTransform pickupImageRoot;
    [SerializeField] public Image pickupProgressImage;
    [SerializeField] public Text itemNameText;

    private Item itemBeingPickUp;
    private float currentPickupTimerElapsed;
    private Camera fpsCamera;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ItemSelect();

        if (HasItemTargetted())
        {
            pickupImageRoot.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {
                PickupProgressComplete();
            }
            else
            {
                currentPickupTimerElapsed = 0f;
            }

            UpdatePickupProgressImage();
        }
        else
        {
            pickupImageRoot.gameObject.SetActive(false);
            currentPickupTimerElapsed = 0f;
        }
    }

    private bool HasItemTargetted()
    {
        return itemBeingPickUp != null;
    }

    private void PickupProgressComplete()
    {
        currentPickupTimerElapsed += Time.deltaTime;
        if (currentPickupTimerElapsed >= pickupTime)
        {
            MoveItemInventory();
        }
    }
    
    private void UpdatePickupProgressImage()
    {
        float pct = currentPickupTimerElapsed / pickupTime;
        pickupProgressImage.fillAmount = pct;
    }

    private void ItemSelect()
    {
        Ray ray = fpsCamera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 2f, layerMask))
        {
            var hitItem = hitInfo.collider.GetComponent<Item>();

            if (hitItem == null)
            {
                itemBeingPickUp = null;
            }

            else if (hitItem != null && hitItem != itemBeingPickUp)
            {
                itemBeingPickUp = hitItem;
                //itemNameText.text = "Pickup" + itemBeingPickUp.gameObject.name;
            }
        }
        else
        {
            itemBeingPickUp = null;
        }
    }

    private void MoveItemInventory()
    {
        itemBeingPickUp.gameObject.SetActive(false);
        itemBeingPickUp = null;
    }

}
