using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Object;

public class EnemyScript : MonoBehaviour,Object
{

    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject spider;

    private Object.objectState objectState;
    private float runningTimer;

    [Tooltip("x/z - Left/Right points on X Axis, y point on Y Axis ")]
    [SerializeField] private Vector3 startPoint;
    public void ResetEnemy()
    {
        transform.position = RandomizeVector3Start();
        SetToWait();
        ChangeEnemyVisual();
        gameObject.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.Instance.OnTouchEnemy += PlayerScript_OnTouchEnemy;
        bat.gameObject.SetActive(false);
        SetToUninitialized();
    }
    private void Update()
    {
        if (objectState == Object.objectState.Uninitialized && GameManager.Instance.IsGamePlayin())
        {
            SetToWait();
        }
        else if (objectState == Object.objectState.Waiting)
        {
            runningTimer -= Time.deltaTime;
            if (runningTimer <= 0f)
            {
                SetToFall();
                runningTimer = 0f;
            }
        }
    }

    public void SetToUninitialized()
    {
        objectState = Object.objectState.Uninitialized;
        //this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        runningTimer = 0f;
    }
    public void SetToWait()
    {
        objectState = Object.objectState.Waiting;
        runningTimer = Random.Range(0f, 5f);
        //this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void SetToFall()
    {
        objectState = Object.objectState.Falling;
        //this.gameObject.SetActive(true);
        this.gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void PlayerScript_OnTouchEnemy(object sender, PlayerScript.OnObjecttouchEventArgs e)
    {
        if (e.collision.gameObject == this.gameObject)
        {
            ResetEnemy();
        }
    }


    private Vector3 RandomizeVector3Start()
    {
        Vector3 ret;
        ret = new Vector3(Random.Range(startPoint.x, startPoint.z), startPoint.y, 0);

        return ret;
    }
    private void ChangeEnemyVisual()
    {
        if (bat.activeSelf)
        {
            bat.gameObject.SetActive(false);
            spider.gameObject.SetActive(true);
        }
        else
        {
            bat.gameObject.SetActive(true);
            spider.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            ResetEnemy();
        }
    }
}
