using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonUI : MonoBehaviour
{
    public TextMeshProUGUI CardNameTag { get => cardNameTag; set => cardNameTag = value; }
    public Image SelectedImage { get => selectedImage; set => selectedImage = value; }
    public CardSO Card { get; set; }

    [SerializeField] private TextMeshProUGUI cardNameTag;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Button cardButton;
    [SerializeField] private TextMeshProUGUI manaCostTag;

    public void SetCardButton(CardSO card)
    {
        this.Card = card;
        cardNameTag.text = card.CardName;
        selectedImage.enabled = false;
        cardButton.onClick.AddListener(OnButtonClicked);
        manaCostTag.text = card.ElixirCost.ToString();
    }

    private void OnButtonClicked()
    {
        if (GameplaySystem.Instance.GameState == GameState.Battle)
        {
            ActionSystem.Instance.SetSelectedCard(Card);
            CardContainerUI.Instance.UpdateCardSelected(Card);
        }
    }
}
