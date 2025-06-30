using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    public Transform swordHold;

    public Rigidbody swordPrefab;

    private Rigidbody currSword;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveSword()
    {
        if (currSword != null)
        {
            Debug.LogWarning("Has sword already");
        }

        currSword = Instantiate(swordPrefab, swordHold);

        //Align the possition
        currSword.transform.localPosition = Vector3.zero;
        currSword.transform.localRotation = Quaternion.identity;

        //Set Kinematic to true
        currSword.isKinematic = true;
    }

    void Awake()
    {
        swordHold = this.transform.Find("SwordPlacement");

        if (swordHold != null )
        {
            Debug.LogError("swordHold could not find");
        }
    }
}
