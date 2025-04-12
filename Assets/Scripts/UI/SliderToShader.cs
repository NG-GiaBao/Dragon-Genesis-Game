using UnityEngine;
using UnityEngine.UI;

public class SliderToShader : MonoBehaviour
{
    public Slider slider;           // G�n Slider UI
    public Material material;       // G�n Material c� d�ng Shader Graph

    void Update()
    {
        // G?i gi� tr? slider.value (0 -> 1) v�o Shader
        material.SetFloat("_SliderValue", slider.value);
    }
}
