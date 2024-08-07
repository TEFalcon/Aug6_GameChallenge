using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    public event EventHandler<OnObjecttouchEventArgs> OnTouchPumpkin;
    public event EventHandler<OnObjecttouchEventArgs> OnTouchEnemy;

    public class OnObjecttouchEventArgs : EventArgs
    {
        public Collider2D collision;
    }
    [SerializeField] private float movementSpeed = 2f;

    [Tooltip("x - Left/y - Right point on X Axis")]
    [SerializeField] private Vector2  endPointsBarriers;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //DOTween.Init(false,false,LogBehaviour.Default);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //HoverTween();
    }

    //private void OnDestroy()
    //{
    //    DOTween.Clear();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PumpkinScript>() != null)
        {
            OnTouchPumpkin?.Invoke(this, new OnObjecttouchEventArgs
            {
                collision = collision
            });
            GameManager.Instance.AddOneToScore();
        }
        if (collision.gameObject.GetComponent<EnemyScript>() != null)
        {
            OnTouchEnemy?.Invoke(this, new OnObjecttouchEventArgs
            {
                collision = collision
            });
            GameManager.Instance.SetGameToOver();
        }
    }

    //private void HoverTween()
    //{
    //    transform.DOMoveY(transform.position.y + 0.1f, Time.deltaTime,false);
    //}
    private void HandleMovement()
    {
        if (GameManager.Instance.IsGamePlayin())
        {
            float moveTarget = GameInput.Instance.GetMovementInput();

            if (moveTarget != 0)
            {
                if (transform.position.x + (movementSpeed * Time.deltaTime * moveTarget) > endPointsBarriers.y || transform.position.x + (movementSpeed * Time.deltaTime * moveTarget) < endPointsBarriers.x)
                    return;

                transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime * moveTarget), transform.position.y, transform.position.z);


                //flip
                Vector3 tempVec = transform.localScale;
                if (moveTarget > 0 && tempVec.x < 0)
                {
                    transform.localScale = new Vector3(-tempVec.x, tempVec.y, tempVec.z);
                }
                else if (moveTarget < 0 && tempVec.x > 0)
                {
                    transform.localScale = new Vector3(-tempVec.x, tempVec.y, tempVec.z);
                }
            }
        }

    }

}
