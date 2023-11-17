using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private SpaceShipModel _shipModel;

    [SerializeField] private Slider _sliderHealth;
    [SerializeField] private Slider _sliderEnergy;
    [SerializeField] private Slider _sliderShield;
    public Dictionary<Button, int> _skillsButtonsDict = new Dictionary<Button, int>();

    private void Awake()
    {
        _shipModel = GetComponent<SpaceShipModel>();
    }

    private void OnEnable()
    {
        _shipModel.OnTakeDamage.AddListener(SetHealthSlider);
        _shipModel.OnTakeEnergy.AddListener(SetEnergySlider);
        _shipModel.OnTakeShield.AddListener(SetShieldSlider);
        _shipModel.OnTakeCure.AddListener(SetHealthSlider);
    }

    private void OnDisable()
    {
        _shipModel.OnTakeDamage.RemoveListener(SetHealthSlider);
        _shipModel.OnTakeEnergy.RemoveListener(SetEnergySlider);
        _shipModel.OnTakeShield.RemoveListener(SetShieldSlider);
        _shipModel.OnTakeCure.RemoveListener(SetHealthSlider);
    }

    public void SetHealthSlider(float value)
    {
        _sliderHealth.value = value;
    }

    public void SetEnergySlider(float value)
    {
        _sliderEnergy.value = value;
        CheckIfActivatePower(value);
    }

    public void SetShieldSlider(float value)
    {
        Debug.Log(value);
        _sliderShield.value = value;
    }

    public void CheckIfActivatePower(float value)
    {
        foreach (KeyValuePair<Button, int> power in _skillsButtonsDict)
        {
            if(value  >= power.Value)
            {
                power.Key.interactable = true;
            }
            else
            {
                power.Key.interactable = false;
            }
        }
    }
}