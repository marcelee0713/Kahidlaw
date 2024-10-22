using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContentSwitcher : MonoBehaviour
{
    public Sprite[] imagesOfContents;
    public Button[] contentButtons;
    public Image[] imagesOfButtons;

    public Sprite selectedIndexImage;
    public Sprite unSelectedIndexImage;

    public Image content;

    public TextMeshProUGUI tutorialHeader;
    public Button tutorialButton;

    public LevelLoader levelLoader;
    public MainMenu mainMenu;

    private int selectedIndex = 0;

    private void Start()
    {
        imagesOfButtons[0].sprite = selectedIndexImage;
        imagesOfButtons[1].sprite = unSelectedIndexImage;
        imagesOfButtons[2].sprite = unSelectedIndexImage;
        imagesOfButtons[3].sprite = unSelectedIndexImage;
        imagesOfButtons[4].sprite = unSelectedIndexImage;
        imagesOfButtons[5].sprite = unSelectedIndexImage;

        content.sprite = imagesOfContents[0];
        tutorialHeader.text = "Movement";
        selectedIndex = 0;
    }

    private void Awake()
    {

        tutorialButton.onClick.AddListener(() =>
        {
            if (!levelLoader.isTransitioning)
            {
                switch (selectedIndex)
                {
                    case 0:
                        levelLoader.LoadGame("MovementTutorial");
                        levelLoader.isTransitioning = true;
                        mainMenu.FadeOutMusic();
                        break;
                    case 1:
                        levelLoader.LoadGame("CombatTutorial");
                        levelLoader.isTransitioning = true;
                        mainMenu.FadeOutMusic();
                        break;
                    case 2:
                        levelLoader.LoadGame("DialogueTutorial");
                        levelLoader.isTransitioning = true;
                        mainMenu.FadeOutMusic();
                        break;
                    case 3:
                        levelLoader.LoadGame("MissionsTutorial");
                        levelLoader.isTransitioning = true;
                        mainMenu.FadeOutMusic();
                        break;
                    case 4:
                        levelLoader.LoadGame("StealthTutorial");
                        levelLoader.isTransitioning = true;
                        mainMenu.FadeOutMusic();
                        break;
                    case 5:
                        levelLoader.LoadGame("QTETutorial");
                        levelLoader.isTransitioning = true;
                        mainMenu.FadeOutMusic();
                        break;

                }
            }
        });

        contentButtons[0].onClick.AddListener(delegate
        {
            imagesOfButtons[0].sprite = selectedIndexImage;
            imagesOfButtons[1].sprite = unSelectedIndexImage;
            imagesOfButtons[2].sprite = unSelectedIndexImage;
            imagesOfButtons[3].sprite = unSelectedIndexImage;
            imagesOfButtons[4].sprite = unSelectedIndexImage;
            imagesOfButtons[5].sprite = unSelectedIndexImage;

            content.sprite = imagesOfContents[0];
            tutorialHeader.text = "Movement";
            selectedIndex = 0;
        });

        contentButtons[1].onClick.AddListener(delegate
        {
            imagesOfButtons[0].sprite = unSelectedIndexImage;
            imagesOfButtons[1].sprite = selectedIndexImage;
            imagesOfButtons[2].sprite = unSelectedIndexImage;
            imagesOfButtons[3].sprite = unSelectedIndexImage;
            imagesOfButtons[4].sprite = unSelectedIndexImage;
            imagesOfButtons[5].sprite = unSelectedIndexImage;

            content.sprite = imagesOfContents[1];
            tutorialHeader.text = "Combat";
            selectedIndex = 1;
        });

        contentButtons[2].onClick.AddListener(delegate
        {
            imagesOfButtons[0].sprite = unSelectedIndexImage;
            imagesOfButtons[1].sprite = unSelectedIndexImage;
            imagesOfButtons[2].sprite = selectedIndexImage;
            imagesOfButtons[3].sprite = unSelectedIndexImage;
            imagesOfButtons[4].sprite = unSelectedIndexImage;
            imagesOfButtons[5].sprite = unSelectedIndexImage;

            content.sprite = imagesOfContents[2];
            tutorialHeader.text = "Dialogue";
            selectedIndex = 2;
        });

        contentButtons[3].onClick.AddListener(delegate
        {
            imagesOfButtons[0].sprite = unSelectedIndexImage;
            imagesOfButtons[1].sprite = unSelectedIndexImage;
            imagesOfButtons[2].sprite = unSelectedIndexImage;
            imagesOfButtons[3].sprite = selectedIndexImage;
            imagesOfButtons[4].sprite = unSelectedIndexImage;
            imagesOfButtons[5].sprite = unSelectedIndexImage;

            content.sprite = imagesOfContents[3];
            tutorialHeader.text = "Missions";
            selectedIndex = 3;

        });

        contentButtons[4].onClick.AddListener(delegate
        {
            imagesOfButtons[0].sprite = unSelectedIndexImage;
            imagesOfButtons[1].sprite = unSelectedIndexImage;
            imagesOfButtons[2].sprite = unSelectedIndexImage;
            imagesOfButtons[3].sprite = unSelectedIndexImage;
            imagesOfButtons[4].sprite = selectedIndexImage;
            imagesOfButtons[5].sprite = unSelectedIndexImage;

            content.sprite = imagesOfContents[4];
            tutorialHeader.text = "Stealth";
            selectedIndex = 4;
        });

        contentButtons[5].onClick.AddListener(delegate
        {
            imagesOfButtons[0].sprite = unSelectedIndexImage;
            imagesOfButtons[1].sprite = unSelectedIndexImage;
            imagesOfButtons[2].sprite = unSelectedIndexImage;
            imagesOfButtons[3].sprite = unSelectedIndexImage;
            imagesOfButtons[4].sprite = unSelectedIndexImage;
            imagesOfButtons[5].sprite = selectedIndexImage;

            content.sprite = imagesOfContents[5];
            tutorialHeader.text = "Quick Time Events";
            selectedIndex = 5;
        });
    }


    void Update()
    {
        
    }
}
