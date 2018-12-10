using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class LevelSelect : MonoBehaviour
{
    public Button lv1, lv2, lv3, lv4, lv5, lv6, back, cross, level;
    public GameObject PlayPanel;
    public int count;
    public GameObject lockImage;

    // Use this for initialization


    public void PlayPanelShow(int i)
    {
        PlayPanel.GetComponent<CanvasGroup>().alpha = 1;
        PlayPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        PlayPanel.GetComponent<CanvasGroup>().interactable = true;

        level.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("level" + i);



        if (PlayerPrefs.GetInt("Level1") == 1)
        {
            
            print("level1");
            
            level.GetComponent<CanvasGroup>().blocksRaycasts = true;
            level.GetComponent<CanvasGroup>().interactable = true;
           
            
            lockImage.GetComponent<CanvasGroup>().alpha = 0;
            lockImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
            lockImage.GetComponent<CanvasGroup>().interactable = false;
        } else if (PlayerPrefs.GetInt("LevelUnlock") < i)
        {
            print(" else if level " );
            level.GetComponent<CanvasGroup>().blocksRaycasts = true;
            level.GetComponent<CanvasGroup>().interactable = true;
            
            lockImage.GetComponent<CanvasGroup>().alpha = 1;
            lockImage.GetComponent<CanvasGroup>().blocksRaycasts = true;
            lockImage.GetComponent<CanvasGroup>().interactable = true;
            

           
        } else
        {
          
            print(" else level " );
            
            level.GetComponent<CanvasGroup>().blocksRaycasts = true;
            level.GetComponent<CanvasGroup>().interactable = true;
            
            
            lockImage.GetComponent<CanvasGroup>().alpha = 0;
            lockImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
            lockImage.GetComponent<CanvasGroup>().interactable = false;
        }


        level.onClick.AddListener(() => {
            switch (i)
            {
                case 1:
                    Application.LoadLevel(SceneNames.Level1);
                    PlayerPrefs.SetInt("Level", 1);

                    PlayerPrefs.Save();
                    break;
                case 2:
                   
                    if (PlayerPrefs.GetInt("LevelUnlock") >= 2)
                    {
                        Application.LoadLevel(SceneNames.Level2);
                        PlayerPrefs.SetInt("Level", 2);
                        PlayerPrefs.Save();
                    } else 
                        print("");
                    break;
                case 3:
                    if (PlayerPrefs.GetInt("LevelUnlock") >= 3)
                    {
                        Application.LoadLevel(SceneNames.Level3);
                        PlayerPrefs.SetInt("Level", 3);
                        PlayerPrefs.Save();
                      
                    } else 
                        print("");
                    break;
                case 4:
                    if (PlayerPrefs.GetInt("LevelUnlock") >= 4)
                    {
                        Application.LoadLevel(SceneNames.Level4);
                        PlayerPrefs.SetInt("Level", 4);
                        PlayerPrefs.Save();

                    } else 
                        print("");
                    break;
                case 5:
                    if (PlayerPrefs.GetInt("LevelUnlock") >= 5)
                    {
                        Application.LoadLevel(SceneNames.Level5);
                        PlayerPrefs.SetInt("Level", 5);
                        PlayerPrefs.Save();
                    } else 
                        print("");
                    break;
                    
                case 6:
                    if (PlayerPrefs.GetInt("LevelUnlock") >= 6)
                    {
                        Application.LoadLevel(SceneNames.Level6);
                        PlayerPrefs.SetInt("Level", 6);
                        PlayerPrefs.Save();
                    } else 
                        print("");
                    break;
                    
            }
        });
    }

    public void PlayPanelHide()
    {
        PlayPanel.GetComponent<CanvasGroup>().alpha = 0;
        PlayPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        PlayPanel.GetComponent<CanvasGroup>().interactable = false;
    }

    void Start()
    {
//        PlayerPrefs.SetInt("LevelUnlock",0);
//        PlayerPrefs.Save();

        switch(PlayerPrefs.GetInt("LevelUnlock"))
        {

        }
        List<Button> levelArray = new List<Button>(6); 
        levelArray.Add(lv1);
        levelArray.Add(lv2);
        levelArray.Add(lv3);
        levelArray.Add(lv4);
        levelArray.Add(lv5);
        levelArray.Add(lv6);

        for(int i = 0 ; i < PlayerPrefs.GetInt("LevelUnlock")-1;i++)
        {
            levelArray[i].GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("complete");
        }

        lv1.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Level1",1);
            PlayerPrefs.Save();

            PlayPanelShow(1);

        });
        lv2.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Level1",2);
            PlayerPrefs.Save();
            PlayPanelShow(2);
        });
        lv3.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Level1",3);
            PlayerPrefs.Save();
            PlayPanelShow(3);
        });
        lv4.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Level1",4);
            PlayerPrefs.Save();
            PlayPanelShow(4);
        });
        lv5.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Level1",5);
            PlayerPrefs.Save();
            PlayPanelShow(5);
        });
        lv6.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Level1",6);
            PlayerPrefs.Save();
            PlayPanelShow(6);
        });
        cross.onClick.AddListener(() => {
            PlayPanelHide();
        });
        back.onClick.AddListener(() => {
            Application.LoadLevel(SceneNames.GameStart);
        });

    }

//    void LoadLevel(int j)
//    {
//        Application.LoadLevel(2);
//    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
