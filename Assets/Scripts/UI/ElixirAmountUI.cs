using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElixirAmountUI : MonoBehaviour
{
    private Slider elixirSlider;

    private void Awake()
    {
        elixirSlider = GetComponent<Slider>();
        elixirSlider.maxValue = PlayerActionSystem.maxElixirAmount;
    }

    private void Update()
    {
        elixirSlider.value = PlayerActionSystem.Instance.ElixirAmount;
    }
}
