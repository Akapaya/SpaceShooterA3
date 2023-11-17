using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerButton : MonoBehaviour
{
    [SerializeField] SpaceShipModel _spaceShipModel;
    [SerializeField] PlayerView _playerView;
    [SerializeField] int _powerValue;
    [SerializeField] powers power;
    PowerBase _shipPower = new Shield();

    public void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        _playerView._skillsButtonsDict.Add(button, _powerValue);
        button.onClick.AddListener(ActivatePower);
        _shipPower = PowersLibrary.ReturnPower(power);
        _shipPower.spaceShipModel = _spaceShipModel;
    }

    public void ActivatePower()
    {
        _shipPower.ActivatePower();
        ShipPlayerModel.CastPlayerEnergyHandler(_powerValue);
    }
}

public enum powers
{
    None = 0,
    Shield,
    SuperShoot,
    UltraShoot
}