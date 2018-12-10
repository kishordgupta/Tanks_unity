using UnityEngine;
using System.Collections;

public class HeroTankBaseRotation : MonoBehaviour {
    public float rotationSpeed = 10f;//degrees
    private float targetAngle;
    private Quaternion targetRotation;
    private Movement movementScript;
    private GameObject track;
    public GameObject trackPrefab;
    
    //    public static bool 
    void Awake()
    {
        this.movementScript = this.GetComponentInParent<Movement>();
        this.targetRotation = Quaternion.Euler(0, 0, this.targetAngle);
        //      Debug.Log ("hi: " + movementScript);
    }
    
    // Use this for initialization
    void Start()
    {
        
    }
    
    //    IEnumerator TrackCreate(float time)
    //    {
    //        yield return new WaitForSeconds(time);
    //       
    //    }
    // Update is called once per frame
    void Update()
    {
        if (movementScript == null)
        {
            return;
        }
        //        CFInput.
        if (Application.loadedLevel != 0)
        {
            var angle = Mathf.Atan2(movementScript.baseDirection.y, movementScript.baseDirection.x) * Mathf.Rad2Deg;
            if (Mathf.Abs(angle - this.targetAngle) > 0.01f)
            {
                this.targetAngle = angle;
                this.targetRotation = Quaternion.Euler(0, 0, angle);
            }
            
            this.transform.rotation = Quaternion.Slerp(
                this.transform.rotation,
                this.targetRotation,
                rotationSpeed * Time.deltaTime);
            
        }
        //        StartCoroutine(TrackDestroy( 6f, track));
        if (PlayerMovement.moving && Application.loadedLevel !=0)
        {
            track = Instantiate(trackPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y,1f), this.gameObject.transform.rotation) as GameObject;
            StartCoroutine(TrackDestroy(Random.Range(3.5f, 4f), track));
        }
    }
    
    IEnumerator TrackDestroy(float time, GameObject track)
    {
        yield return new WaitForSeconds(time);
        Destroy(track);
    }
}
