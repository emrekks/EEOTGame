using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HideInObject : NetworkBehaviour
{
    private Camera fpsCamera;
    private PlayerController _player;
    [SerializeField] private Transform HideLocation;
    [SerializeField] private Image Image;
    private GameObject HideinObjects;
    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
        _player = GetComponent<PlayerController>();
        HideinObjects = GameObject.FindGameObjectWithTag("HideCanObject");
    }

    // Update is called once per frame
    void Update()
    {
        Hide();
    }


    void Hide()
    {
        Ray ray = fpsCamera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 2f))
        {
            if (hitInfo.collider.gameObject.tag == "PlayerCanHide" && _player.CanHideTrigger == true)
            {
                Image.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HideLocation = hitInfo.collider.transform;
                    _player.transform.position = HideLocation.position;
                    //CmdHide(HideLocation);
                }
            }
        }
        else
        {
            Image.gameObject.SetActive(false);
        }
    }


    //[Command]
    //void CmdHide(Transform loc)
    //{
    //    RpcHide(loc);
    //}

    //[ClientRpc]
    //void RpcHide(Transform hideLoc)
    //{
    //    Debug.Log("icerde");
    //    player.transform.position = hideLoc.position;
    //}
}
