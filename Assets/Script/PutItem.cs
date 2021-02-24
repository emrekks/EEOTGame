using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PutItem : NetworkBehaviour
{
    private Camera fpsCamera;
    private PlayerController player;
    [SerializeField] public Image PickupImage;
    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        itemSelect();
    }

    void itemSelect()
    {
        Ray ray = fpsCamera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 2f))
        {
            if (hitInfo.collider.gameObject.tag == "PutItem1")
            {
                PickupImage.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F) && player.Item1Count > 0)
                {
                    CmdItemPut("Item1");
                }
            }
        }
        else
        {
            PickupImage.gameObject.SetActive(false);
        }
    }

    [Command]
    void CmdItemPut(string name)
    {
        if(name == "Item1")
        {
            player.Item1Count -= 1;
        }
    }

}
