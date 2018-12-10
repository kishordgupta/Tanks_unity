using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;

public class HeroDamage : MonoBehaviour
{

		private Health healthScript;
		public GameObject Panel;
		public Button playAgain;
		public Sprite[] numbers;
//    public AudioSource deathSound;
		private string lastResponse = null;
		public InterstitialAd interstatial;
		AdRequest request ;

		void Awake ()
		{
				this.healthScript = this.GetComponentInChildren<Health> ();
		}

		private void CallFBLogin ()
		{
				FB.Login ("email,publish_actions", LoginCallback);
		}
    
		void LoginCallback (FBResult result)
		{
				if (result.Error != null)
						lastResponse = "Error Response:\n" + result.Error;
				else if (!FB.IsLoggedIn) {
						lastResponse = "Login cancelled by Player";
				} else {
						lastResponse = "Login was successful!";
						Debug.Log ("last response ");
						PlayerPrefs.SetString ("name", FB.UserId);
						PlayerPrefs.Save ();
				}


		}

		private void SetInit ()
		{
				enabled = true; 
				// "enabled" is a magic global; this lets us wait for FB before we start rendering
		}
    
		private void OnHideUnity (bool isGameShown)
		{
				if (!isGameShown) {
						// pause the game - we will need to hide
						Time.timeScale = 0;
				} else {
						// start the game back up - we're getting focus again
						Time.timeScale = 1;
				}
		}

		void Start ()
		{

				FB.Init (SetInit, OnHideUnity);
//				interstatial = new InterstitialAd ("ca-app-pub-2884314377778347/9751331916"); // android
				interstatial = new InterstitialAd ("ca-app-pub-2884314377778347/5181531513");  // ios
				// Create an empty ad request.
				request = new AdRequest.Builder ().Build ();
				interstatial.LoadAd (request);
		

        
				print ("name : " + FB.UserId);

				if (Application.loadedLevel != 0) {
						GameObject.Find ("Play").GetComponent<Button> ().onClick.AddListener (() => {
								if (interstatial.IsLoaded ()) {
										interstatial.Show ();
								}

								Application.LoadLevel (SceneNames.LevelSelect);


						});

						GameObject.Find ("Share").GetComponent<Button> ().onClick.AddListener (() => {
								if (!FB.IsLoggedIn)
										CallFBLogin ();
            
								FB.Feed (
						                link: "http://www.siliconorchard.com/app/tanks/index.html",
						                linkName: "Invasion of Tank Commander, 1943",
						                linkCaption: "Enjoy the game",
						                linkDescription: "My Score is " + PlayerPrefs.GetInt ("Point") + ", What is yours?",
						                picture: "http://www.siliconorchard.com/app/tanks/appicon.png",
						                callback: LogCallback
								);
						});
				}

		}

		void LogCallback (FBResult response)
		{
				print (" " + response.Text);
		}

		public  void ScoreShow (bool levelComplete)
		{

//        if (LevelController2.adShow)
//            GoogleAdController.ShowInterstital();

				// Load the interstitial with the request.


				GameObject.Find ("CPanel").GetComponent<CanvasGroup> ().alpha = 1;
				GameObject.Find ("CPanel").GetComponent<CanvasGroup> ().blocksRaycasts = true;
				GameObject.Find ("CPanel").GetComponent<CanvasGroup> ().interactable = true;
				Time.timeScale = 0f;
				int score = PlayerPrefs.GetInt ("Point");
				int reverse = 0;
				int index = 0;
				int[] scoreArray = new int[3];
				while (score != 0) {
            
						scoreArray [index] = score % 10;
						score = score / 10;
						index++;
				}
        
				GameObject.Find ("score1").GetComponent<Image> ().sprite = numbers [scoreArray [0]];
				GameObject.Find ("score2").GetComponent<Image> ().sprite = numbers [scoreArray [1]];
				GameObject.Find ("score3").GetComponent<Image> ().sprite = numbers [scoreArray [2]];

				if (levelComplete)
						GameObject.Find ("LevelCompleted").GetComponent<CanvasGroup> ().alpha = 1;
				else
						GameObject.Find ("LevelCompleted").GetComponent<CanvasGroup> ().alpha = 0;
//        HighScoreManager._instance.SaveHighScore(PlayerPrefs.GetString("name") == null ? "player" : PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("Point"));
				HighScoreManager._instance.SaveHighScore ("you", PlayerPrefs.GetInt ("Point"));

//        GoogleAdController.HideInterstital();

		}

		void Update ()
		{
        
				if (this.healthScript.HP >= 50) {
						this.transform.FindChild ("HalfLifeEffect").gameObject.SetActive (false);
						//            SpecialEffectsHelper.Instance.HalfLifeParticle(this.transform.position);
				}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
        
				//        print(other.transform.position);
				if (this.healthScript.HP <= 50) {
						this.transform.FindChild ("HalfLifeEffect").gameObject.SetActive (true);
						//            SpecialEffectsHelper.Instance.HalfLifeParticle(this.transform.position);
				}
				Instantiate (Resources.Load ("DamageAnimation") as GameObject, other.transform.position, other.transform.rotation);
				if (this.healthScript.HP <= 0) {


						if (PlayerPrefs.GetInt ("Sound") <= 0)
								Instantiate (Resources.Load ("DestroySound") as GameObject, this.transform.position, this.transform.rotation);
						Instantiate (Resources.Load ("FinalExplosion") as GameObject, this.transform.position, this.transform.rotation);


						this.gameObject.SetActive (false);
						ScoreShow (false);
           
						return;
				}
        
				var bulletFlyScript = other.GetComponent<BulletFly> ();
				if (bulletFlyScript == null) {
						return;
				}
				if (bulletFlyScript.shooterTag == this.tag) {
						return;
				}
				//        SpecialEffectsHelper.Instance.FullExplosionParticle(this.transform.position);
				var damage = bulletFlyScript.GetDamage (this.transform);
				this.healthScript.TakeDamage (damage);
		}
}
