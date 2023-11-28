using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Card Scriptable Object")]
public class CardSO : ScriptableObject
{
    // SET BY CODE = ONLY WHEN INSTATIATED
    public Participant Participant { get; set; }
    public Vector3 DeployDirection { get; set; }

    // SET BY DESIGNER
    public string CardName { get => cardName; set => cardName = value; }
    public GameObject CardObject { get => cardObject; set => cardObject = value; }
    public int ElixirCost { get => elixirCost; set => elixirCost = value; }
    public CardDeployLocation CardDeployLocation { get => cardDeployLocation; set => cardDeployLocation = value; }

    [SerializeField] private string cardName;
    [SerializeField] private GameObject cardObject;
    [SerializeField] private int elixirCost;
    [SerializeField] private CardDeployLocation cardDeployLocation;
}
