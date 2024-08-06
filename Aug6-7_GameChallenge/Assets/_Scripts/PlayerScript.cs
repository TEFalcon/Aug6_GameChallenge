using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 2f;

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
            collision.gameObject.GetComponent<PumpkinScript>().ResetPumpkin();
            GameManager.Instance.AddOneToScore();
        }
        if (collision.gameObject.GetComponent<EnemyScript>() != null)
        {
            collision.gameObject.GetComponent<PumpkinScript>().ResetPumpkin();
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
