using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputData : MonoBehaviour
{
    public GameObject projectilePrefab;
    public InputDevice rightController;
    public InputDevice leftController;
    public InputDevice HMD;
    public InputDevice targetDevice;

    //cooldown attributes
    float _fireDelay = 0.5f;
    float _nextShot = 0.40f;

    // Update is called once per frame
    void Update()
    {
        if (!rightController.isValid || !leftController.isValid || !HMD.isValid)
        {
            InitializeInputDevices();
        }
        leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.5f && Time.time > _nextShot)
        {
            _nextShot = Time.time + _fireDelay;
            GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            spawnedProjectile.transform.forward = transform.forward;
            Debug.Log("TRIGGER PRESSED: " + triggerValue);
        }
    }

    private void InitializeInputDevices()
    {
        if (!rightController.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref rightController);
            Debug.Log("LEFT");
        }
        if (!leftController.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref leftController);
            Debug.Log("RIGHT");
        }
        if (!HMD.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref HMD);
            Debug.Log("HMD");
        }

    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputDeviceCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }
}
