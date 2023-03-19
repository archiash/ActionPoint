using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public EquipmentSelectPanel equipmentSelectPanel;
    public Forgesmith forgesmith;
    public RecipeManager recipeManager;
    public CraftDetail craftDetail;
    public EnermyDetail enermyDetail;
    public ResultReport resultReport;
    public HuntingManager huntingManager;
    public RaidManager raidManager;
    private void Start()
    {
        Instance = this;
    }
}
