
using UnityEngine;
using UnityEngine.UIElements;

public class PumpkinScript : MonoBehaviour,Object
{
    [SerializeField] private PumpkinFaceSO[] pumpkinFaceSOList;
    [SerializeField] private GameObject faceImgObj;

    [SerializeField] private Transform pumpkinVisuals;

    private Object.objectState objectState;
    private float runningTimer;

    [Tooltip("x/z - Left/Right points on X Axis, y point on Y Axis ")]
    [SerializeField] private Vector3 startPoint;
    public void ResetPumpkin()
    {
        //pumpkinVisuals.gameObject.SetActive(false);
        transform.position = RandomizeVector3Start();
        SetToWait();
        ChangePumpkinFace();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript.Instance.OnTouchPumpkin += PlayerScript_OnTouchPumpkin;
        pumpkinVisuals.gameObject.SetActive(true);
        SetToUninitialized();
    }

    private void Update()
    {
        if(objectState == Object.objectState.Uninitialized && GameManager.Instance.IsGamePlayin())
        {
            SetToWait();
        }
        else if(objectState == Object.objectState.Waiting)
        {
            runningTimer-=Time.deltaTime;
            if(runningTimer <= 0f)
            {
                SetToFall();
                runningTimer = 0f;
            }
        }
    }
    private void PlayerScript_OnTouchPumpkin(object sender, PlayerScript.OnObjecttouchEventArgs e)
    {
        if(e.collision.gameObject == this.gameObject)
        {
            ResetPumpkin();
        }
    }

    private Vector3 RandomizeVector3Start()
    {
        Vector3 ret;
        ret = new Vector3(Random.Range(startPoint.x, startPoint.z),startPoint.y,0);

        return ret;
    }
    private void ChangePumpkinFace()
    {
        int index = Random.Range(0,pumpkinFaceSOList.Length);
        faceImgObj.GetComponent<SpriteRenderer>().sprite = pumpkinFaceSOList[index].sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Floor")
        {
            ResetPumpkin();
        }
    }
    public void SetToUninitialized()
    {
        objectState = Object.objectState.Uninitialized;
        //this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody2D>().simulated= false;
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
}
