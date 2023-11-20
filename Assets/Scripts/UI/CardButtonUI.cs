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

    public void SetCardButton(Card card)
    {
        cardNameTag.text = card.CardName;
        this.card = card;
        selectedImage.enabled = false;
        cardButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        ActionSystem.Instance.SetSelectedCard(card);
        CardContainerUI.Instance.UpdateCardSelected(card);
    }
}
