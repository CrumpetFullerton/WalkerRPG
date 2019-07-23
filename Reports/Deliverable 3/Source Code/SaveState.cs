/*SaveState.cs
 * Created By: Phillip Buckreis 10/4/17
 * 
 * This creates a savestate
 * 
 * Last modified: 11/27/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveState
{
    public int steps = 0;
    public int gold = 0;
    public float PowerLevel = 1.0f;
    public int currentZone = 0;
    public int completedLevel = 0;
    public int[] zoneSteps = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] zonePaths = new int[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    public struct item
    {
        public int ID;
        public string Title;
        public int ItemType;
        public int Rarity;
        public int Power;
        public int Strength;
        public int Critical;
        public int APCost;
        public int Element1;
        public int Element2;
        public int Status;
        public int StatProc;
        public string Description;
        public int Value;
        public string Slug;
        public int position;
    }

    public item[] savedItems = new item[30];

    public struct pet
    {
        public bool Unlocked;
        public float Level;
        public double StartSavedTime;
        public double NextPayout;
    }

    public item PlayerWeapon1 = new item();
    public item PlayerWeapon2 = new item();
    public item PlayerWeapon3 = new item();
    public item PlayerHead = new item();
    public item PlayerBody = new item();
    public item PlayerLegs = new item();

    public pet[] savedPets = new pet[8];
}