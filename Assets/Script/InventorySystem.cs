using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InventorySystem : NetworkBehaviour
{
    
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public float pickupTime = 2f;
    [SerializeField] public RectTransform pickupImageRoot;
    [SerializeField] public Image pickupProgressImage;
    [SerializeField] public Text itemNameText;

    private bool itemtaking = false;

    [SyncVar]
    private GameObject NetworkItemRemove;
    
    private GameObject itemBeingPickUp;
    
    private float currentPickupTimerElapsed;
    private Camera fpsCamera;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        itemBeingPickUp = GameObject.FindGameObjectWithTag("Item");
        fpsCamera = GetComponentInChildren<Camera>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

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
            NetworkItemRemove = itemBeingPickUp;
            CmdMoveItemInventory(NetworkItemRemove);
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
                itemBeingPickUp = hitItem.gameObject;
                //itemNameText.text = "Pickup" + itemBeingPickUp.gameObject.name;
            }
        }
        else
        {
            itemBeingPickUp = null;
        }
    }

    [Command]
    private void CmdMoveItemInventory(GameObject _Item)
    {
        if(_Item != null)
        {
            player.itemCount += 1;
            return;
        }
        NetworkServer.Destroy(_Item); 
    }

}
