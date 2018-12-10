using UnityEngine;
using System.Collections;

public class DamageAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DestroyObject()
    {
//        print("Animation completed");
        if(CanonBulletFly.play == true)
        {
            CanonBulletFly.play = false;
            Application.LoadLevel(SceneNames.LevelSelect);
        }

        if(CanonBulletFly.setting == true)
        {
            btnstartGameStart.LoadSetting();
            Time.timeScale = 0;
        }
        if(CanonBulletFly.highScore ==true)
        {
            btnstartGameStart.LoadHighScore();
            Time.timeScale = 0;

        }
        Destroy(this.gameObject);
    }
}
