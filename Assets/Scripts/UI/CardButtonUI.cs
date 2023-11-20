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


    private void Start()
    {
        cardNameTag.text = card.CardName;
        selectedImage.enabled = false;
    }

    // CAN CALL SELECTION CARD
}
