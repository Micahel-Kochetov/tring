using Assets.Scripts.States.ARRing.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FavouriteSelectSaver
{
    private const int ringsCount = 21;

    private bool[] isFavouriteRings;

    public FavouriteSelectSaver()
    {
        isFavouriteRings = new bool[ringsCount];
    }

    public void SetFavouriteValue(int index, bool isFavourite)
    {
        isFavouriteRings[index] = isFavourite;
    }

    public bool GetFavouriteValue(int index)
    {
        return isFavouriteRings[index];
    }
}