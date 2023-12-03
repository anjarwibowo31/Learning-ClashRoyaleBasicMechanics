using UnityEngine;
using UnityEngine.UI;

public class ElixirAmountUI : MonoBehaviour
{
    private Slider elixirSlider;

    private void Awake()
    {
        elixirSlider = GetComponent<Slider>();
        elixirSlider.maxValue = ActionSystem.maxElixirAmount;
    }

    private void Update()
    {
        elixirSlider.value = ActionSystem.Instance.ElixirAmount;
    }
}
