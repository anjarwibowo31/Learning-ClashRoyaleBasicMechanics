using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public abstract CardType CardType { get; }

    public abstract string CardName { get;}
}
