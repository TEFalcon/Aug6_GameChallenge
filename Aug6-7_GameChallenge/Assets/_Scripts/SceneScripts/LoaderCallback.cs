using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Owned by EyalK
public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;


    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate= false;

            SceneLoader.LoaderCallback();
        }
        else
        { 
        
        }
    }
}
