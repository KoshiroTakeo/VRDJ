using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawedController;
    private GameObject spawedHandModel;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if(prefab)
            {
                spawedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("コントローラのモデルがありません");
                spawedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawedHandModel = Instantiate(handModelPrefab, transform);
        }
        
    }

    void Update()
    {
        if(showController)
        {
            spawedHandModel.SetActive(false);
            spawedController.SetActive(true);
        }
        else
        {
            spawedHandModel.SetActive(true);
            spawedController.SetActive(false);
        }
        //if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue)  && primaryButtonValue)
        //{
        //    Debug.Log("ボタンが押された");
        //}
        
        //if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        //{
        //    Debug.Log("トリガーが引かれてる" + triggerValue);
        //}

        //if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        //{
        //    Debug.Log("スティックが倒されてる" + primary2DAxisValue);
        //}
    }
}
