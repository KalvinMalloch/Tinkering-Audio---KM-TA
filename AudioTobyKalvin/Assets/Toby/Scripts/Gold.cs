// Copyright MIT License 2019 K&T Team 27
// Author: Toby Atkinson
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gold Class.
/// For updating gold user has and playing sounds accordingly.
/// </summary>

public class Gold : MonoBehaviour
{
    // Integer that holds current gold
    private int goldAmount = 0;
    [Header("Change gold added on button press")]

    // Amount of gold added each time the Add Gold button is pressed
    [SerializeField]
    private int goldToAdd;

    // The Text component so we can update the UI showing gold amount
    private Text goldText;

    /// <summary>
    /// Gets the Text component and updates it.
    /// </summary>
    void Start()
    {
        // Finding the Text component in the "GoldAmount" gameobject
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>();
        // Updating the Text so it shows 0 gold
        UpdateText();
    }

    /// <summary>
    /// Adds gold and updates the UI.
    /// Runs when Add Gold button is pressed.
    /// </summary>
    public void AddGold()
    {
        // Adds the amount of gold
        goldAmount += goldToAdd;
        // Updates the Text
        UpdateText();
    }

    /// <summary>
    /// Checks if item can be bought and removes gold/plays sound accordingly.
    /// </summary>
    public void RemoveGold(int amount)
    {
        // If statement checks if item costs more than gold user has
        if(goldAmount >= amount)
        {
            // If the user can afford item minus the cost from gold amount
            goldAmount -= amount;
            // Update the Text to show the item has been bought
            UpdateText();

            //----------------posotive sound
        }
        else
        {
            // Else if user cannot afford item do not change gold

            //-----------------no money sound
        }
        
    }

    /// <summary>
    /// Updates gold text UI to show amount of gold.
    /// </summary>
    void UpdateText()
    {
        // Changes int of gold amount to string and displays it on screen
        goldText.text = goldAmount.ToString();
    }
}
