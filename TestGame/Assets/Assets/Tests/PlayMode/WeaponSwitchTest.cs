using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WeaponSwitchTests
{
    [Test]
    public void WeaponSwitch_SelectWeapon_SwitchToFirstWeapon()
    {
        // Arrange
        var gameObject = new GameObject();
        var weaponSwitch = gameObject.AddComponent<WeaponSwitch>();
        var sword1 = new GameObject().AddComponent<Sword>();
        sword1.gameObject.transform.SetParent(gameObject.transform);

        weaponSwitch.selectedWeapon = 1;
        weaponSwitch.swords = new Sword[] { sword1 };

        // Act
        weaponSwitch.SelectWeapon();

        // Assert
        Assert.IsFalse(sword1.gameObject.activeSelf);
    }

    [Test]
    public void WeaponSwitch_SelectWeapon_SwitchToSecondWeapon()
    {
        // Arrange
        var gameObject = new GameObject();
        var weaponSwitch = gameObject.AddComponent<WeaponSwitch>();
        var sword1 = new GameObject().AddComponent<Sword>();
        var sword2 = new GameObject().AddComponent<Sword>();

        sword1.gameObject.transform.SetParent(gameObject.transform);
        sword2.gameObject.transform.SetParent(gameObject.transform);

        weaponSwitch.selectedWeapon = 1;
        weaponSwitch.swords = new Sword[] { sword1, sword2 };

        // Act
        weaponSwitch.SelectWeapon();

        // Assert
        Assert.IsFalse(sword1.gameObject.activeSelf);
        Assert.IsTrue(sword2.gameObject.activeSelf);
    }

    [Test]
    public void WeaponSwitch_Update_SelectedWeaponChanged()
    {
        // Arrange
        var gameObject = new GameObject();
        var weaponSwitch = gameObject.AddComponent<WeaponSwitch>();
        var sword1 = new GameObject().AddComponent<Sword>();
        var sword2 = new GameObject().AddComponent<Sword>();

        sword1.gameObject.transform.SetParent(gameObject.transform);
        sword2.gameObject.transform.SetParent(gameObject.transform);

        weaponSwitch.selectedWeapon = 0;
        weaponSwitch.swords = new Sword[] { sword1, sword2 };

        // Act
        weaponSwitch.Update(); // Trigger update to change the selected weapon to 1

        // Assert
        Assert.IsTrue(sword1.gameObject.activeSelf);
        Assert.IsTrue(sword2.gameObject.activeSelf);
    }
}
