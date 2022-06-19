using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Startup : MonoBehaviour
{
    private void Awake()
    {
        GlobalController.Init();
        if(!Permission.HasUserAuthorizedPermission(Permission.Camera)) Permission.RequestUserPermission(Permission.Camera);
    }
}
