using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


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
            //collision.gameObject.GetComponent<EnemyScript>().ResetPumpkin();
        }
    }


    private void HandleMovement()
    {
        if (GameManager.Instance.IsGamePlayin())
        {
            float moveTarget = GameInput.Instance.GetMovementInput();

            if (moveTarget != 0)
            {


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
