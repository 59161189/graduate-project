using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : IEquatable<Player>
{
    public string PlayerId;
    public string PlayerName;
    public int Hp = 1000;
    public int mana = 5;

    public bool Equals(Player other)
    {
        if (PlayerId.Equals(other.PlayerId))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
