using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PumpkinScript : MonoBehaviour
{
    [SerializeField] private PumpkinFaceSO[] pumpkinFaceSOList;
    [SerializeField] private GameObject faceImgObj;

    [SerializeField] private Transform pumpkinVisuals;

    [Tooltip("x/z - Left/Right points on X Axis, y point on Y Axis ")]
    [SerializeField] private Vector3 stratPoint;
    public void ResetPumpkin()
    {

        pumpkinVisuals.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.Instance.OnTouchPumpkin += PlayerScript_OnTouchPumpkin;
        pumpkinVisuals.gameObject.SetActive(true);
    }

    private void PlayerScript_OnTouchPumpkin(object sender, PlayerScript.OnObjecttouchEventArgs e)
    {
        if(e.collision.gameObject == this.gameObject)
        {
            ResetPumpkin();
        }
    }

    private void RandomizeXStart()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
