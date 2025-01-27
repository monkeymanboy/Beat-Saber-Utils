﻿using System;
using Harmony;

namespace BS_Utils.Gameplay.HarmonyPatches
{
    [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO))]
    [HarmonyPatch("Init", MethodType.Normal)]
    class BlahBlahGrabTheLevelData
    {
        static void Prefix(StandardLevelScenesTransitionSetupDataSO __instance, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, PracticeSettings practiceSettings, string backButtonText, bool useTestNoteCutSoundEffects)
        {
            Plugin.LevelData.GameplayCoreSceneSetupData = new GameplayCoreSceneSetupData(difficultyBeatmap, gameplayModifiers, playerSpecificSettings, practiceSettings, useTestNoteCutSoundEffects);
            Plugin.LevelData.IsSet = true;
            __instance.didFinishEvent -= __instance_didFinishEvent;
            __instance.didFinishEvent += __instance_didFinishEvent;

        }

        private static void __instance_didFinishEvent(StandardLevelScenesTransitionSetupDataSO levelScenesTransitionSetupDataSO, LevelCompletionResults levelCompletionResults)
        {
            Plugin.TriggerLevelFinishEvent(levelScenesTransitionSetupDataSO, levelCompletionResults);
        }
    }
    
    [HarmonyPatch(typeof(MissionLevelScenesTransitionSetupDataSO))]
    [HarmonyPatch("Init", MethodType.Normal)]
    class BlahBlahGrabTheMissionLevelData
    {
        static void Prefix(MissionLevelScenesTransitionSetupDataSO __instance, IDifficultyBeatmap difficultyBeatmap, MissionObjective[] missionObjectives, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings)
        {
            Plugin.LevelData.GameplayCoreSceneSetupData = new GameplayCoreSceneSetupData(difficultyBeatmap, gameplayModifiers, playerSpecificSettings, PracticeSettings.defaultPracticeSettings, false);
            Plugin.LevelData.IsSet = true;
            __instance.didFinishEvent -= __instance_didFinishEvent;
            __instance.didFinishEvent += __instance_didFinishEvent;

        }

        private static void __instance_didFinishEvent(MissionLevelScenesTransitionSetupDataSO missionLevelScenesTransitionSetupDataSO, MissionCompletionResults missionCompletionResults)
        {
            Plugin.TriggerMissionFinishEvent(missionLevelScenesTransitionSetupDataSO, missionCompletionResults);
        }
    }
}
