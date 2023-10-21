using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefaults", menuName = "Defaults/Player")]
public class PlayerProperties : ScriptableObject
{
    public float health = 5f;

    public float jumpPower = 60f;
    public float jumpCooldown = .3f;

    public float speed = 5f;

    public float drag = 3f;
    //
}
