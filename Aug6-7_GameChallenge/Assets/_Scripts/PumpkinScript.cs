
using UnityEngine;
using UnityEngine.UIElements;

public class PumpkinScript : MonoBehaviour
{
    [SerializeField] private PumpkinFaceSO[] pumpkinFaceSOList;
    [SerializeField] private GameObject faceImgObj;

    [SerializeField] private Transform pumpkinVisuals;

    [Tooltip("x/z - Left/Right points on X Axis, y point on Y Axis ")]
    [SerializeField] private Vector3 startPoint;
    public void ResetPumpkin()
    {
        pumpkinVisuals.gameObject.SetActive(false);
        transform.position = RandomizeVector3Start();
        ChangePumpkinFace();
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
