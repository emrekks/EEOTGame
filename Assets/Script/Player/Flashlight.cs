using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public float flashLightDuration = 10f;
    private Light _light;
    bool flashlightEnable = false;

    public float timer;


    void Start()
    {
        _light = GetComponentInChildren<Light>();
    }


    void Update()
    {
        FlashlightTurn();
    }

    void FlashlightTurn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flashlightEnable = !flashlightEnable;
        }

        if (flashlightEnable && flashLightDuration > 0)
        {
            _light.enabled = true;
            flashLightDuration -= Time.deltaTime;
            timer = 0;
            if(flashLightDuration <= 0)
            {
                flashLightDuration = 0;
                flashlightEnable = false;
            }

        }

        if (!flashlightEnable || flashLightDuration <= 0)
        {

            _light.enabled = false;

            if(timer < 3)
            {
                timer += Time.deltaTime;
            }

            if (flashLightDuration < 3 && timer >= 3)
            {
                flashLightDuration += Time.deltaTime;
            }
        }
    }
}
