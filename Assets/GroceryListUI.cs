using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryListUI : MonoBehaviour
{
    public Text[] groceryItems;
    public InputField inputField;
    private int itemCount = 0;

    public void AddItemToList()
    {
        if (itemCount < groceryItems.Length)
        {
            groceryItems[itemCount].text = (itemCount + 1) + ". " + inputField.text;
            itemCount++;
            inputField.text = ""; // Clear the input field.
        }
    }
}
