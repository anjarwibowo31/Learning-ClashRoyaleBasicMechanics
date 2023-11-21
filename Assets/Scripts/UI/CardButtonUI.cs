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
    public Card Card { get => card; set => card = value; }

    [SerializeField] private TextMeshProUGUI cardNameTag;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Card card;
    [SerializeField] private Button cardButton;
    [SerializeField] private TextMeshProUGUI manaCostTag;

    public void SetCardButton(Card card)
    {
        cardNameTag.text = card.CardName;
        this.card = card;
        selectedImage.enabled = false;
        cardButton.onClick.AddListener(OnButtonClicked);
        manaCostTag.text = card.ManaCost.ToString();
    }

    private void OnButtonClicked()
    {
        if (GameplaySystem.Instance.GameState == GameState.Battle)
        {
            ActionSystem.Instance.SetSelectedCard(card);
            CardContainerUI.Instance.UpdateCardSelected(card);
        }
    }
}
