using UnityEngine;
using System.Collections;

public class JeepMovement : Movement
{


    public Transform targetFlag;
    private Transform lastTargetFlag;
    private bool targeting;//looking for the target
    float timer ;
    private Vector3 lastPos ;
    private CarController carController;
    private float normalSpeed = 1f;
    // Use this for initialization
    void Start()
    {
    
        var carControllerGameObject = GameObject.FindGameObjectWithTag(Tags.carController);
        carController = carControllerGameObject.GetComponent<CarController>();
        targeting = true;

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0f)
        {
            return;
        }
        
        var targetPoint =  new Vector3(100,100);//carController.nodePoints1 [0].position;
        targetPoint.z = this.transform.position.z;
        
        if (this.targeting)
        {
            var direction = (targetPoint - this.transform.position).normalized;
            base.movingTarget = targetPoint + direction * 0.25f;
            if (this.targetFlag != null)
            {
                if (this.lastTargetFlag != null)
                {
                    Destroy(this.lastTargetFlag.gameObject);
                }
                lastTargetFlag = Instantiate(targetFlag, base.movingTarget, this.transform.rotation) as Transform;
               
                
            }
            this.targeting = false;
        }

        base.fireTarget = targetPoint;
        base.baseDirection = (base.movingTarget - this.transform.position).normalized;
        this.transform.position += base.baseDirection * base.speed * Time.deltaTime;
            if ((this.transform.position - base.movingTarget).magnitude < 0.5f)// || this.rigidbody2D.velocity.magnitude<0f)
            {
                this.targeting = true;
            }
        
    }
    
    void OnDestroy()
    {
        if (this.lastTargetFlag != null)
        {
            Destroy(this.lastTargetFlag.gameObject);
        }
    }
}
