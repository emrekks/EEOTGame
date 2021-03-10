using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HideInObject : NetworkBehaviour
{
    private Camera fpsCamera;
    private PlayerController _player;
    [SyncVar] private Transform HideLocation;
    [SerializeField] private Image Image;
    private GameObject[] HideinObjects;
    [SyncVar] private bool hide = false;
    public bool hideNoSync;
    [SyncVar] private GameObject exitPos;
    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
        _player = GetComponent<PlayerController>();
        HideinObjects = GameObject.FindGameObjectsWithTag("PlayerCanHide");
        exitPos = GameObject.FindGameObjectWithTag("ExitPosition");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Hide();
    }


    void Hide()
    {
        Ray ray = fpsCamera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 2f))
        {
            if (hitInfo.collider.gameObject.tag == "PlayerCanHide" && _player.CanHideTrigger == true )
            {
                Image.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hide = !hide;
                    hideNoSync = hide;
                    
                    if(hide)
                    {
                        HideLocation = hitInfo.collider.transform;
                        CmdHide("hidetrue", HideLocation);
                    }
                   
                    if (!hide)
                    {
                        CmdHide("hidefalse" , HideLocation);
                    }
                }
            }
        }

        else
        {
            Image.gameObject.SetActive(false);
        }
    }


    [Command]
    void CmdHide(string name, Transform hideloc)
    {
        RpcHide(name, hideloc);
    }

    [ClientRpc]
    void RpcHide(string hidename, Transform hidelocation)
    {
        if(hidename == "hidetrue")
        {
            foreach (GameObject io in HideinObjects)
            {
                Physics.IgnoreCollision(io.GetComponent<Collider>(), _player.GetComponent<Collider>(), true);
                _player.transform.position = hidelocation.position;
            }
        }

        if(hidename == "hidefalse")
        {
            foreach (GameObject io in HideinObjects)
            {
                Physics.IgnoreCollision(io.GetComponent<Collider>(), _player.GetComponent<Collider>(), false);
                _player.transform.position = exitPos.transform.position;
            }
        }
    }
}
