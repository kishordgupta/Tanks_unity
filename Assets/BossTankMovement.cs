using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossTankMovement : Movement
{
    public Transform targetFlag;
    private Transform lastTargetFlag;
    private GameObject tankHero;
    private bool targeting;//looking for the target
    float timer ;
    private List<Vector2> path;
    
    void Awake()
    {
        tankHero = GameObject.FindGameObjectWithTag(Tags.hero);
//        this.gameObject.rigidbody2D.mass = Random.Range(0,140);
        //        this.gameObject.transform.Find("TankBase/BaseSprite").GetComponent<SpriteRenderer>().sprite = tankSprite[(int)Random.Range(0,3)];
        targeting = true;
    }
    
    void Start()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log (other.tag);
        if (other.tag == Tags.edge)
        {
            this.targeting = true;
            print("triggering");
        }
    }
    //
    //    void OnCollisionStay2D(Collision2D coll)
    //    {
    //        if(coll.gameObject.tag == Tags.enemy )
    //        {
    //            timer = 0f;
    //            timer += Time.deltaTime;
    //            if(timer > 8f )
    //                this.targeting = true;
    //
    //            print("collision staying" + timer);
    //        }
    //    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (tankHero == null)
        {
            return;
        }
        if (Time.deltaTime == 0f)
        {
            return;
        }
        
        var targetPoint = this.tankHero.transform.position;
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
                path = NavMesh2D.GetSmoothedPath(this.transform.position, lastTargetFlag.position);
                
            }
            this.targeting = false;
        }
        
        if (path != null && path.Count != 0)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, path [0], 2f * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, path [0]) < 0.01f)
            {
                path.RemoveAt(0);
            }
        }
        
        base.fireTarget = targetPoint;
        base.baseDirection = (base.movingTarget - this.transform.position).normalized;
        //        this.transform.position += base.baseDirection * base.speed * Time.deltaTime;
        
        
        if ((this.transform.position - base.movingTarget).magnitude < 2f )
        {
            this.targeting = true;
        }
        
        //Debug.Log (string.Format ("{0} -> {1} | {2}", this.transform.position, this.targetPosition, this.targeting));
    }
    
    void OnDestroy()
    {
        if (this.lastTargetFlag != null)
        {
            Destroy(this.lastTargetFlag.gameObject);
        }
    }
}
