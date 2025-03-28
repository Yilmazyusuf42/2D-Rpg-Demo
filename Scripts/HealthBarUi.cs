using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    Entity entity;
    Slider slider;
    CharacterStats stats;
    RectTransform rect;


    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        rect = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        stats = GetComponentInParent<CharacterStats>();

        entity.OnFlipped += ReverseFlipped;
        stats.UpdateHealthBar += UpdateHealthUi;

        UpdateHealthUi();
    }


    void UpdateHealthUi()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = stats.currentHealth;
    }


    void ReverseFlipped() => rect.Rotate(0, 180, 0);

    void Unsubsribes()
    {
        entity.OnFlipped -= ReverseFlipped;
        stats.UpdateHealthBar -= UpdateHealthUi;
    }
}
