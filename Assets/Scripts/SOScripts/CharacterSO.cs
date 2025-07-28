using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Characters")]
public class CharacterSO : ScriptableObject
{
    public string characterName;

    public int index;

    public int priceInMoney;

    public GameObject characterForMenu;

    public GameObject characterForGame;
}
