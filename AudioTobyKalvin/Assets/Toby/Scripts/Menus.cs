// Copyright MIT License 2019 K&T Team 27
// Author: Toby Atkinson
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Menus Class.
/// Allows user to switch between Options and Store area using UI.
/// </summary>
public class Menus : MonoBehaviour
{
    // Initialising game object variables for switching UI on/off
    private GameObject title;
    private GameObject storeButton;
    private GameObject storeGroup;
    private GameObject optionsGroup;

    /// <summary>
    /// Assigns each GameObject variable to the correct objects in scene
    /// and sets the options and store UI as inactive to show title screen.
    /// </summary>
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

    /// <summary>
    /// Sets store UI as inactive and options UI as
    /// active when Options button pressed.
    /// </summary>
    public void OptionsButtonPressed()
    {
        // Setting store UI inactive
        storeGroup.SetActive(false);

        // Setting options UI active
        storeButton.SetActive(true);
        optionsGroup.SetActive(true);
    }

    /// <summary>
    /// Sets Options Ui as inactive and store UI
    /// as activce when Store button pressed.
    /// </summary> 
    public void StoreButtonPressed()
    {
        // Setting options UI inactive
        title.SetActive(false);
        storeButton.SetActive(false);
        optionsGroup.SetActive(false);

        // Setting store UI active
        storeGroup.SetActive(true);
    }

}
