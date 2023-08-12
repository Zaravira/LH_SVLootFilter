using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LH_SVLootFilter
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]

    public class LH_SVLootFilter : BaseUnityPlugin
    {
        public const string pluginGuid = "LH_SVLootFilter";
        public const string pluginName = "LH_SVLootFilter";
        public const string pluginVersion = "0.0.2";

        static GameObject WeirdThing;

        public void Awake()
        {
            string pluginfolder = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            string bundleName = "lh_svlootfilter";
            string prefabName = "Assets/Common/LH_SVLootFilter.prefab";
            Harmony.CreateAndPatchAll(typeof(LH_SVLootFilter));
            WeirdThing = AssetBundle.LoadFromFile($"{pluginfolder}\\{bundleName}").LoadAsset<GameObject>(prefabName);
        }

        [HarmonyPatch(typeof(Lootfilter), nameof(Lootfilter.FilterLoot))]
        [HarmonyPostfix]

        public static void RunFilter(Collectible col, ref int __result)
        {
            if (col.itemType == 3 && MiningFilter.Contains(col.itemID))
                __result = 2;
        }

        [HarmonyPatch(typeof(LootfilterControl), nameof(LootfilterControl.Open))]
        [HarmonyPostfix]

        public static void MakeButton(Dropdown ___tierDropdown1)
        {
            if (EnergyCellToggle == null)
            {
                GameObject LootfilterConfigMenu = ___tierDropdown1.gameObject.transform.parent.gameObject;
                EnergyCellToggle = GameObject.Instantiate(LootfilterConfigMenu).transform.Find("CollectJunk").GetComponent<Toggle>();
                EnergyCellToggle.transform.SetParent(LootfilterConfigMenu.transform.GetChild(15));
                EnergyCellToggle.transform.localPosition = new Vector3(7, 111, 0);
                EnergyCellToggle.transform.localScale = new Vector3(1, 1, 0);
                EnergyCellToggle.GetComponentInChildren<Text>().text = "Collect Energy Cells";
                EnergyCellToggle.isOn = true;
                EnergyCellToggle.gameObject.SetActive(true);
                LootfilterConfigMenu.transform.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    if (EnergyCellToggle.isOn == false && MiningFilter.Contains(18) == false)
                        MiningFilter.Add(18);
                    else if (EnergyCellToggle.isOn == true && MiningFilter.Contains(18) == true)
                        MiningFilter.Remove(18);
                });
            }
            if (MiningFilterBtn == null)
            {
                GameObject LootfilterConfigMenu = ___tierDropdown1.gameObject.transform.parent.gameObject;
                MiningFilterBtn = GameObject.Instantiate(LootfilterConfigMenu.transform.GetComponentInChildren<Button>());
                MiningFilterBtn.transform.SetParent(LootfilterConfigMenu.transform.GetChild(15));
                MiningFilterBtn.transform.localPosition = new Vector3(-103, 98, 0);
                MiningFilterBtn.transform.localScale = new Vector3(1, 1, 0);
                MiningFilterBtn.GetComponentInChildren<Text>().text = "Mining filter";
                MiningFilterBtn.gameObject.SetActive(true);
                MiningFilterBtn.onClick = new Button.ButtonClickedEvent();
                MiningFilterBtn.onClick.AddListener(() => {

                    GameObject mom = GameObject.Instantiate(WeirdThing.transform.Find("LootfilterMetals").gameObject);
                    mom.transform.SetParent(LootfilterConfigMenu.transform);
                    mom.transform.localPosition = new Vector3(0, 0, 0);
                    mom.transform.localScale = new Vector3(1, 1, 0);
                    mom.SetActive(true);

                    Toggle[] Toggles = mom.GetComponentsInChildren<Toggle>();
                    for (int i = 0; i < Toggles.Length; i++)
                    {
                        if (MiningFilter.Contains(MiningDict[Toggles[i].name]))
                            Toggles[i].isOn = false;
                        else if (MiningFilter.Count < 1)
                            Toggles[i].isOn = true;
                        else
                            Toggles[i].isOn = true;
                    }
                    Toggle btnGreyOre = mom.transform.Find("PanelGreyOre/Grey Ore").gameObject.GetComponent<Toggle>();
                    Toggle btnBlueCrystal = mom.transform.Find("PanelGreyOre/Blue Crystal").gameObject.GetComponent<Toggle>();
                    Toggle btnIron = mom.transform.Find("PanelGreyOre/Iron").gameObject.GetComponent<Toggle>();
                    Toggle btnNickel = mom.transform.Find("PanelGreyOre/Nickel").gameObject.GetComponent<Toggle>();
                    Toggle btnCobalt = mom.transform.Find("PanelGreyOre/Cobalt").gameObject.GetComponent<Toggle>();
                    Toggle btnLithium = mom.transform.Find("PanelGreyOre/Lithium").gameObject.GetComponent<Toggle>();
                    Toggle btnSilver = mom.transform.Find("PanelGreyOre/Silver").gameObject.GetComponent<Toggle>();

                    Toggle btnRedOre = mom.transform.Find("PanelRedOre/Red Ore").gameObject.GetComponent<Toggle>();
                    Toggle btnRedCrystal = mom.transform.Find("PanelRedOre/Red Crystal").gameObject.GetComponent<Toggle>();
                    Toggle btnAluminum = mom.transform.Find("PanelRedOre/Aluminum").gameObject.GetComponent<Toggle>();
                    Toggle btnCopper = mom.transform.Find("PanelRedOre/Copper").gameObject.GetComponent<Toggle>();
                    Toggle btnGallium = mom.transform.Find("PanelRedOre/Gallium").gameObject.GetComponent<Toggle>();
                    Toggle btnSilicon = mom.transform.Find("PanelRedOre/Silicon").gameObject.GetComponent<Toggle>();
                    Toggle btnGold = mom.transform.Find("PanelRedOre/Gold").gameObject.GetComponent<Toggle>();

                    Toggle btnGreenOre = mom.transform.Find("PanelGreenOre/Green Ore").gameObject.GetComponent<Toggle>();
                    Toggle btnGreenCrystal = mom.transform.Find("PanelGreenOre/Green Crystal").gameObject.GetComponent<Toggle>();
                    Toggle btnTitanium = mom.transform.Find("PanelGreenOre/Titanium").gameObject.GetComponent<Toggle>();
                    Toggle btnBismuth = mom.transform.Find("PanelGreenOre/Bismuth").gameObject.GetComponent<Toggle>();
                    Toggle btnSamarium = mom.transform.Find("PanelGreenOre/Samarium").gameObject.GetComponent<Toggle>();
                    Toggle btnIridium = mom.transform.Find("PanelGreenOre/Iridium").gameObject.GetComponent<Toggle>();
                    Toggle btnPlatinum = mom.transform.Find("PanelGreenOre/Platinum").gameObject.GetComponent<Toggle>();

                    mom.transform.Find("SelectAll").gameObject.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (!btnGreyOre.isOn)
                            btnGreyOre.isOn = true;
                        if (!btnBlueCrystal.isOn)
                            btnBlueCrystal.isOn = true;
                        if (!btnIron.isOn)
                            btnIron.isOn = true;
                        if (!btnNickel.isOn)
                            btnNickel.isOn = true;
                        if (!btnCobalt.isOn)
                            btnCobalt.isOn = true;
                        if (!btnLithium.isOn)
                            btnLithium.isOn = true;
                        if (!btnSilver.isOn)
                            btnSilver.isOn = true;

                        if (!btnRedOre.isOn)
                            btnRedOre.isOn = true;
                        if (!btnRedCrystal.isOn)
                            btnRedCrystal.isOn = true;
                        if (!btnAluminum.isOn)
                            btnAluminum.isOn = true;
                        if (!btnCopper.isOn)
                            btnCopper.isOn = true;
                        if (!btnGallium.isOn)
                            btnGallium.isOn = true;
                        if (!btnSilicon.isOn)
                            btnSilicon.isOn = true;
                        if (!btnGold.isOn)
                            btnGold.isOn = true;

                        if (!btnGreenOre.isOn)
                            btnGreenOre.isOn = true;
                        if (!btnGreenCrystal.isOn)
                            btnGreenCrystal.isOn = true;
                        if (!btnTitanium.isOn)
                            btnTitanium.isOn = true;
                        if (!btnBismuth.isOn)
                            btnBismuth.isOn = true;
                        if (!btnSamarium.isOn)
                            btnSamarium.isOn = true;
                        if (!btnIridium.isOn)
                            btnIridium.isOn = true;
                        if (!btnPlatinum.isOn)
                            btnPlatinum.isOn = true;
                    });
                    mom.transform.Find("DeselectAll").gameObject.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (btnGreyOre.isOn)
                            btnGreyOre.isOn = false;
                        if (btnBlueCrystal.isOn)
                            btnBlueCrystal.isOn = false;
                        if (btnIron.isOn)
                            btnIron.isOn = false;
                        if (btnNickel.isOn)
                            btnNickel.isOn = false;
                        if (btnCobalt.isOn)
                            btnCobalt.isOn = false;
                        if (btnLithium.isOn)
                            btnLithium.isOn = false;
                        if (btnSilver.isOn)
                            btnSilver.isOn = false;

                        if (btnRedOre.isOn)
                            btnRedOre.isOn = false;
                        if (btnRedCrystal.isOn)
                            btnRedCrystal.isOn = false;
                        if (btnAluminum.isOn)
                            btnAluminum.isOn = false;
                        if (btnCopper.isOn)
                            btnCopper.isOn = false;
                        if (btnGallium.isOn)
                            btnGallium.isOn = false;
                        if (btnSilicon.isOn)
                            btnSilicon.isOn = false;
                        if (btnGold.isOn)
                            btnGold.isOn = false;

                        if (btnGreenOre.isOn)
                            btnGreenOre.isOn = false;
                        if (btnGreenCrystal.isOn)
                            btnGreenCrystal.isOn = false;
                        if (btnTitanium.isOn)
                            btnTitanium.isOn = false;
                        if (btnBismuth.isOn)
                            btnBismuth.isOn = false;
                        if (btnSamarium.isOn)
                            btnSamarium.isOn = false;
                        if (btnIridium.isOn)
                            btnIridium.isOn = false;
                        if (btnPlatinum.isOn)
                            btnPlatinum.isOn = false;
                    });
                    mom.transform.Find("Close").gameObject.GetComponent<Button>().onClick.AddListener(() => {
                        MiningFilter.Clear();
                        for (int i = 0; i < Toggles.Length; i++)
                        {
                            if (Toggles[i].isOn == false)
                                MiningFilter.Add(MiningDict[Toggles[i].name]);
                        };
                        mom.SetActive(false);
                    });
                });
            }
        }

        [HarmonyPatch(typeof(BuffRefinery), "AddMetalToCargo")]
        [HarmonyPrefix]
        static bool FilterRefinery(int itemID)
        {
            if (MiningFilter.Contains(itemID))
                return false;
            else
                return true;
        }
        private static Dictionary<string, int> MiningDict = new Dictionary<string, int>()
        {
            { "Iron", 1 },
            { "Blue Crystal", 2 },
            { "Red Crystal", 3 },
            { "Green Crystal", 4 },
            { "Gold", 16 },
            { "Silver", 19 },
            { "Grey Ore", 27 },
            { "Nickel", 28 },
            { "Cobalt", 29 },
            { "Aluminum", 30 },
            { "Copper", 31 },
            { "Gallium", 32 },
            { "Titanium", 33 },
            { "Bismuth", 34 },
            { "Samarium", 35 },
            { "Lithium", 36 },
            { "Silicon", 37 },
            { "Iridium", 38 },
            { "Platinum", 39 },
            { "Red Ore", 40 },
            { "Green Ore", 41 },
        };
        public static List<int> MiningFilter = new List<int>();
        public static Button MiningFilterBtn;
        public static Toggle EnergyCellToggle;
    }
}