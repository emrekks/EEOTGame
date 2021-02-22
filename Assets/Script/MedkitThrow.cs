using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitThrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * 3, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
