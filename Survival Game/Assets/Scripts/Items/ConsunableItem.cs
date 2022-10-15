using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "InventoryData/FoodItem", order = 1)]
public class ConsunableItem : BaseAbstractItem
{
    [SerializeField] int staminaBonus;
    [SerializeField] int hungerBonus;
    [SerializeField] int thirstBonus;

}
