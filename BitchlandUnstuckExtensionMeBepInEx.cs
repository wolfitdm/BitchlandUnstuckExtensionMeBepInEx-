using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using Den.Tools;
using HarmonyLib;
using HarmonyLib.Tools;
using SemanticVersioning;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BitchlandUnstuckExtensionMeBepInEx
{
    [BepInPlugin("com.wolfitdm.BitchlandUnstuckExtensionMeBepInEx", "BitchlandUnstuckExtensionMeBepInEx Plugin", "1.0.0.0")]
    public class BitchlandUnstuckExtensionMeBepInEx : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private ConfigEntry<bool> configEnableMe;

        public BitchlandUnstuckExtensionMeBepInEx()
        {
        }

        public static Type MyGetType(string originalClassName)
        {
            return Type.GetType(originalClassName + ",Assembly-CSharp");
        }

        private static string pluginKey = "General.Toggles";

        public static bool enableThisMod = false;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableThisMod",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");


            enableThisMod = configEnableMe.Value;

            Harmony.CreateAndPatchAll(typeof(BitchlandUnstuckExtensionMeBepInEx));

            Logger.LogInfo($"Plugin BitchlandUnstuckExtensionMeBepInEx BepInEx is loaded!");
        }

        [HarmonyPatch(typeof(Mis_Zea2), "DestIn6")]
        [HarmonyFinalizer]
        public static Exception Mis_Zea2_DestIn6(Exception __exception)
        {
            if (__exception != null)
            {
                Logger.LogInfo(__exception.Message);
                Logger.LogInfo(__exception.StackTrace);
            }
            return enableThisMod ? null : __exception;
        }


        [HarmonyPatch(typeof(int_ConstructionPlan), "ResourcesCheck")]
        [HarmonyFinalizer]
        public static Exception int_ConstructionPlan_ResourcesCheck_exception(Exception __exception)
        {
            if (__exception != null)
            {
                Logger.LogInfo(__exception.Message);
                Logger.LogInfo(__exception.StackTrace);
            }
            return enableThisMod ? null : __exception;
        }

        [HarmonyPatch(typeof(Mis_BackEntrance), "PlanPlacedCheck")]
        [HarmonyFinalizer]
        public static Exception Mis_BackEntrance_PlanPlacedCheck_exception(Exception __exception)
        {
            if (__exception != null)
            {
                Logger.LogInfo(__exception.Message);
                Logger.LogInfo(__exception.StackTrace);
            }
            return enableThisMod ? null : __exception;
        }

        [HarmonyPatch(typeof(Mis_BackEntrance), "BuildCheck")]
        [HarmonyFinalizer]
        public static Exception Mis_BackEntrance_BuildCheck_exception(Exception __exception)
        {
            if (__exception != null)
            {
                Logger.LogInfo(__exception.Message);
                Logger.LogInfo(__exception.StackTrace);
            }
            return enableThisMod ? null : __exception;
        }

        [HarmonyPatch(typeof(Mis_BackEntrance), "BuildCheck")]
        [HarmonyPrefix]
        public static bool Mis_BackEntrance_BuildCheck(object __instance)
        {
            try
            {
                Mis_BackEntrance _this = (Mis_BackEntrance) __instance;
                if (_this._ThePlan == null)
                    return false;
                Main.Instance.MainThreads.Remove(new Action(_this.BuildCheck));
                UI_Gameplay _gameplay = Main.Instance.GameplayMenu;
                Person _sia = Main.Instance.CityCharacters.Sia;
                Person _zea = Main.Instance.CityCharacters.Zea;
                _sia.AddMoveBlocker("siaback");
                _zea.AddMoveBlocker("siaback");
                _this.CompleteGoal(5);
                _gameplay.CanBuild = true;
                FieldInfo _pplField = _this.GetType().GetField("_ppl", BindingFlags.NonPublic | BindingFlags.Instance);
                List<GameObject> _ppl = null;
                object __ppl = null;
          
                if (_pplField == null)
                {
                    Main.Instance.GameplayMenu.ShowNotification("_pplField == null");
                    return false;
                } else
                {
                    Main.Instance.GameplayMenu.ShowNotification("_pplField != null");
                }

                __ppl = _pplField.GetValue(__instance);

                if ( __ppl == null) 
                {
                    Main.Instance.GameplayMenu.ShowNotification("__ppl == null");
                    return false;
                } else
                {
                    Main.Instance.GameplayMenu.ShowNotification("__ppl != null");
                }

                try
                {
                    _ppl = (List<GameObject>)__ppl;
                    Main.Instance.GameplayMenu.ShowNotification("_ppl = List<GameObject>");
                }
                catch (Exception ex)
                {
                    Main.Instance.GameplayMenu.ShowNotification("_ppl is not a List<GameObject>");
                    return false;
               }

                _gameplay.DisplaySubtitle("This is disguised as a shitty radio tower", _this.VoiceLines[12], (Action)(() => _gameplay.DisplaySubtitle("but this is actually a medium range scanner", _this.VoiceLines[13], (Action)(() => _gameplay.DisplaySubtitle("and it also pulsates \"anti air waves\"", _this.VoiceLines[14], (Action)(() => _gameplay.DisplaySubtitle("whatever that is", _this.VoiceLines[15], (Action)(() => _gameplay.DisplaySubtitle("it's another of Sephie's inventions", _this.VoiceLines[16 /*0x10*/], (Action)(() => _gameplay.DisplaySubtitle("I won't even try to understand how any of it works, as long as it works", _this.VoiceLines[17], (Action)(() => _gameplay.DisplaySubtitle("Anyway, Let's head back now", _this.VoiceLines[18], (Action)(() =>
                {
                    Main.Instance.GameplayMenu.ShowNotification("aaaa");
                    _sia.ThisPersonInt.EndTheChat();
                    Main.Instance.GameplayMenu.ShowNotification("aaaab");
                    Main.Instance.GameplayMenu.TheScreenFader.FadeOut(3f, (Action)(() =>
                    {
                        Main.Instance.GameplayMenu.ShowNotification("aaaac");
                        try
                        {
                            Main.Instance.GameplayMenu.ShowNotification("aaad");
                            Main.RunInNextFrame((Action)(() => Main.Instance.GameplayMenu.TheScreenFader.FadeIn(1f)));
                            _this.CompleteMission();
                            Main.Instance.GameplayMenu.ShowNotification("aaaaf");
                            Main.Instance.CanSaveFlags_remove("siaback");
                            // Main.Instance.SaveGame(true); throws a exception because autosave file is inaccessable
                            _sia.gameObject.SetActive(false);
                            _zea.gameObject.SetActive(false);
                            Main.Instance.GameplayMenu.ShowNotification("aaaaf");
                            _sia.PlaceAt(_this.Objs[0].transform);
                            _zea.PlaceAt(_this.Objs[0].transform);
                            Main.Instance.GameplayMenu.ShowNotification("aaaag");
                            Main.Instance.Player.PlaceAt(_this.Objs[8].transform);
                            Main.Instance.Player.SleepOnFloor();
                            Main.Instance.GameplayMenu.ShowNotification("The next day...");
                            Main.Instance.Player.UserControl.ResetSpot = Main.Instance.Player.UserControl.OriginalResetSpot;
                            _sia.RemoveMoveBlocker("siaback");
                            _zea.RemoveMoveBlocker("siaback");
                            Main.RunInNextFrame((Action)(() =>
                            {
                                try
                                {
                                    _sia.gameObject.SetActive(true);
                                    _sia.CurrentScheduleTask = (Person.ScheduleTask)null;
                                    _sia.WorkJob._StartWorkFor(_sia);
                                    for (int index = 0; index < _ppl.Count; ++index)
                                        _ppl[index].gameObject.SetActive(true);
                                    _pplField.SetValue(__instance, _ppl);
                                    Main.Instance.GameplayMenu.ShowNotification("set _pplfield");
                                }
                                catch (Exception ex)
                                {
                                    Main.Instance.GameplayMenu.ShowNotification("can not set _pplfield");
                                }
                            }), 4);
                            Main.RunInSeconds((Action)(() => Main.Instance.AllMissions[14].InitMission()), 10f);
                        } catch (Exception ex)
                        {
                            Logger.LogError(ex.ToString());
                        }
                    }));
                }), _sia)), _sia)), _sia)), _sia)), _sia)), _sia)), _sia);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            return false;
        }

        [HarmonyPatch(typeof(Mis_BackEntrance), "PlanPlacedCheck")]
        [HarmonyPrefix]
        public static bool Mis_BackEntrance_PlanPlacedCheck(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                Mis_BackEntrance _this = (Mis_BackEntrance)__instance;
                if (!((UnityEngine.Object)Main.Instance.GameplayMenu.LatestPlacedPlan != (UnityEngine.Object)null) || !(Main.Instance.GameplayMenu.LatestPlacedPlan.name == "PLAN_Struct_RadioTower"))
                    return false;
                Main.Instance.MainThreads.Remove(new Action(_this.PlanPlacedCheck));
                UI_Gameplay _gameplay = Main.Instance.GameplayMenu;
                Person _sia = Main.Instance.CityCharacters.Sia;
                _this.CompleteGoal(3);
                _gameplay.DisplaySubtitle("Next you'll need to put the resources it needs in it", _this.VoiceLines[6], (Action)(() =>
                {
                    _this.AddGoal(4, true);
                    _this.Objs[7].SetActive(true);
                    _gameplay.DisplaySubtitle("Lucky for you, we have them in these boxes right here", _this.VoiceLines[7], (Action)(() => _gameplay.DisplaySubtitle("just transfer them to it", _this.VoiceLines[8], (Action)(() =>
                    {
                        if ((UnityEngine.Object)Main.Instance.Player.CurrentBackpack == (UnityEngine.Object)null)
                            _gameplay.DisplaySubtitle("Since you don't have a backpack right now,", _this.VoiceLines[23], (Action)(() => _gameplay.DisplaySubtitle("you'll need to pick up one item at a time to your hand", _this.VoiceLines[24], (Action)(() => _sia.ThisPersonInt.EndTheChat()), _sia)), _sia);
                        else
                            _sia.ThisPersonInt.EndTheChat();
                        _this._ThePlan = Main.Instance.GameplayMenu.LatestPlacedPlan.GetComponentInChildren<int_ConstructionPlan>(true); // throws a exception, because if the player placed a plan and then cancel the placed plan, therefore we set includeInactive = true
                        Main.Instance.MainThreads.Add(new Action(_this.ResourceCheck));
                    }), _sia)), _sia);
                }), _sia);
            } catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            return false;
        }

        [HarmonyPatch(typeof(int_ConstructionPlan), "ResourcesCheck")]
        [HarmonyPrefix]
        public static bool int_ConstructionPlan_ResourcesCheck(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            try
            {
                int_ConstructionPlan _this = (int_ConstructionPlan) __instance;
                Dictionary<e_ResourceType, int> dictionary = new Dictionary<e_ResourceType, int>();
                foreach (GameObject storageItem in _this.StorageItems)
                {
                    int_ResourceItem componentInChildren = storageItem.GetComponentInChildren<int_ResourceItem>(true);
                    Logger.LogInfo(componentInChildren.name);
                    Logger.LogInfo(componentInChildren.ResourceType.ToString());
                    if (dictionary.ContainsKey(componentInChildren.ResourceType))
                        dictionary[componentInChildren.ResourceType]++;
                    else
                        dictionary[componentInChildren.ResourceType] = 1;

                    Logger.LogInfo(dictionary[componentInChildren.ResourceType].ToString());
                }
                foreach (bl_RecipesNeed ingredient in _this.OwnRecipe.Ingredients)
                {
                    Logger.LogInfo("ingredientType: " + ingredient.IngredientType.ToString());
                    if (!dictionary.ContainsKey(ingredient.IngredientType) || dictionary[ingredient.IngredientType] < ingredient.Amount)
                        return false;
                }
                _this.AllResourcesIn = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
            return false;
        }

        [HarmonyPatch(typeof(Mis_Zea2), "DestIn6")]
        [HarmonyPrefix]
        public static bool DestIn6_MisZea2(object __instance)
        {
            if (!enableThisMod)
            {
                return true;
            }

            Mis_Zea2 _this = (Mis_Zea2)__instance; 
            Person _zea = Main.Instance.CityCharacters.Zea;
            if ((double)Vector3.Distance(Main.Instance.Player.transform.position, _zea.transform.position) >= 2.0)
                return false;
            Main.Instance.MainThreads.Remove(new Action(_this.DestIn6));
            UI_Gameplay _gameplay = Main.Instance.GameplayMenu;
            Main.Instance.Player.UserControl.FirstPerson = true;
            _zea.LookAtPlayer.NonplayerTarget = _this.Objs[5].transform;
            _gameplay.DisplaySubtitle("Okay so, here is a keypad", _this.VoiceLines[32 /*0x20*/], (Action)(() => _gameplay.DisplaySubtitle("And uhm, how do I describe this to you?", _this.VoiceLines[33], (Action)(() =>
            {
                _zea.LookAtPlayer.NonplayerTarget = (Transform)null;
                _gameplay.DisplaySubtitle("Like, if you're around...", _this.VoiceLines[34], (Action)(() => _gameplay.DisplaySubtitle("...and there's serious problems going on", _this.VoiceLines[35], (Action)(() =>
                {
                    _zea.LookAtPlayer.NonplayerTarget = _this.Objs[5].transform;
                    _gameplay.DisplaySubtitle("come here and insert the numbers 4 2 7", _this.VoiceLines[36], (Action)(() => _gameplay.DisplaySubtitle("it's easy to remember", _this.VoiceLines[37], (Action)(() =>
                    {
                        _zea.LookAtPlayer.NonplayerTarget = (Transform)null;
                        _gameplay.DisplaySubtitle("it's 27 like your locker number", _this.VoiceLines[38], (Action)(() => _gameplay.DisplaySubtitle("and there's 4 lockers in there", _this.VoiceLines[39], (Action)(() => _gameplay.DisplaySubtitle("so, 4 and 27", _this.VoiceLines[40], (Action)(() => _gameplay.DisplaySubtitle("well then, we should head back", _this.VoiceLines[41], (Action)(() => _gameplay.DisplaySubtitle("I don't want my Sardinas to get burnt", _this.VoiceLines[42], (Action)(() =>
                        {
                            _this.SpawnSpots[24].gameObject.SetActive(false);
                            _gameplay.DisplaySubtitle("Or both burnt and cold", _this.VoiceLines[43], (Action)(() =>
                            {
                                _zea.ThisPersonInt.EndTheChat();
                                Main.Instance.GameplayMenu.TheScreenFader.FadeOut(4f, (Action)(() =>
                                {
                                    try
                                    {
                                        Main.RunInNextFrame((Action)(() => Main.Instance.GameplayMenu.TheScreenFader.FadeIn(1f)));
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.LogError(ex.ToString());
                                    }

                                    try
                                    {
                                        Main.Instance.Player.UserControl.ResetSpot = Main.Instance.Player.UserControl.OriginalResetSpot;
                                        Main.Instance.Player.PlaceAt(_this.SpawnSpots[30]);
                                        Main.Instance.Player.UserControl.FirstPerson = false;
                                        _this.SpawnSpots[33].gameObject.SetActive(false);
                                        _this.Army2.FakeTruck.SetActive(true);
                                        Main.Instance.Player.SleepOnFloor();
                                        Main.Instance.GameplayMenu.ShowNotification("The next day...");
                                        for (int index = 0; index < _this._ppl.Count; ++index)
                                            _this._ppl[index].SetActive(true);
                                        _zea.gameObject.SetActive(false);
                                        _this.CompleteMission();
                                        _this.MainWindZone.enabled = true;
                                        Main.RunInSeconds((Action)(() => Main.Instance.AllMissions[13].InitMission()), 10f);
                                        SceneManager.MoveGameObjectToScene(Main.Instance.Player.gameObject, Main.Instance.gameObject.scene);
                                        SceneManager.MoveGameObjectToScene(Main.Instance.CityCharacters.Zea.gameObject, Main.Instance.gameObject.scene);
                                        SceneManager.UnloadSceneAsync(_this.SceneToLoad);
                                        Main.Instance.CanSaveFlags_remove("Zea2Mission");
                                        /* Main.Instance.SaveGame(true); */ /* throws a exception because the auto save file is inaccessible; 
                                                                             * the file is being used by another process.
                                                                             * Suggestions/ideas: synchronize/thread-safe/serialize with System.Text.Json */
                                    } catch (Exception ex)
                                    {
                                        Logger.LogError(ex.ToString());
                                    }
                                }));
                            }), _zea);
                        }), _zea)), _zea)), _zea)), _zea)), _zea);
                    }), _zea)), _zea);
                }), _zea)), _zea);
            }), _zea)), _zea);
            return false;
        }
    }
}
