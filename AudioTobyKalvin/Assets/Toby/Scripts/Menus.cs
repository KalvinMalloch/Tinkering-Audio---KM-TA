// Copyright MIT License 2019 K&T Team 27
// Author: Toby Atkinson
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{

    // Initialising game object variables for switching UI on/off

    private GameObject title;
    private GameObject storeButton;
    private GameObject storeGroup;
    private GameObject optionsGroup;


    void Start()
    {
        // Assinging the game object variables to the correct UI groups in scene
        title = GameObject.Find("TitleGroup");
        storeButton = GameObject.Find("StoreButton");
        storeGroup = GameObject.Find("StoreGroup");
        optionsGroup = GameObject.Find("OptionsGroup");

        // Setting the inital options and store menus as inactive
        storeGroup.SetActive(false);
        optionsGroup.SetActive(false);
    }


    void Update()
    {
        
    }


    // Public function for when player presses the options button
    public void OptionsButtonPressed()
    {
        // Setting old UI as inactive
        storeGroup.SetActive(false);

        // Setting new UI as active 
        storeButton.SetActive(true);
        optionsGroup.SetActive(true);
    }

    // Public function for when player presses the store button
    public void StoreButtonPressed()
    {
        // Setting old UI as inactive
        title.SetActive(false);
        storeButton.SetActive(false);
        optionsGroup.SetActive(false);

        // Setting new UI as active 
        storeGroup.SetActive(true);
    }

}
