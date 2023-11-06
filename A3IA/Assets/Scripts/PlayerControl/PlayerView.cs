using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private SpaceShipModel _shipModel;

    [SerializeField] private Slider _sliderHealth;

    private void Awake()
    {
        _shipModel = GetComponent<SpaceShipModel>();
    }

    private void OnEnable()
    {
        _shipModel.OnTakeDamage.AddListener(SetHealthSlider);
        _shipModel.OnTakeCure.AddListener(SetHealthSlider);
    }

    private void OnDisable()
    {
        _shipModel.OnTakeDamage.RemoveListener(SetHealthSlider);
        _shipModel.OnTakeCure.RemoveListener(SetHealthSlider);
    }

    public void SetHealthSlider(float value)
    {
        _sliderHealth.value = value;
    }
}