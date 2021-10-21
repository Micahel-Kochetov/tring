using Assets.Scripts.States.ARRing.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FavouriteSelectSaver
{
    private Dictionary<int, bool> isFavouriteRings = new Dictionary<int, bool>();

    public void SetFavouriteValue(int index, bool isFavourite)
    {
        isFavouriteRings[index] = isFavourite;
    }

    public bool GetFavouriteValue(int index)
    {
        if(isFavouriteRings.ContainsKey(index))
        {
            return isFavouriteRings[index];
        }

        return false;
    }
}