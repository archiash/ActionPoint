﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forgesmith : MonoBehaviour
{
    public static Forgesmith instance;
    public List<RecipeList> recipeLists = new List<RecipeList>();
    public int smithLevel;

    private void Awake()
    {
            instance = this;
    }

    [System.Serializable]
    public class RecipeList
    {
        public List<Recipe> recipes = new List<Recipe>();
    }
/*
    public void CraftList(int level)
    {
        RecipeManager.instance.recipes.Clear();
        RecipeManager.instance.recipes = recipeLists[level-1].recipes;
        RecipeManager.instance.UpdateRecipe();
    }*/
}
