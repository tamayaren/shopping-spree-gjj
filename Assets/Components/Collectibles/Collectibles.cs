using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public int chips, chocoSticks, bellPepper, tomato, pancitBihon;

    private string _collectedItemName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectibles"))
        {
            _collectedItemName = other.gameObject.name;
            Destroy(gameObject);
        }

        switch (_collectedItemName)
        {
            case "AirChips":
                chips++;
                break;
            case "ChocStick":
                chocoSticks++;
                break;
            case "BellPepper":
                bellPepper++;
                break;
            case "PancitBihon":
                pancitBihon++;
                break;
            case "Tomato":
                tomato++;
                break;
            default:
                break;
        }
    }
}
