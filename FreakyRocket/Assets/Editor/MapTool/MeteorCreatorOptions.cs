using Assets.MapObject.Meteor;
using Assets.MapObject.Rotation;
using Assets.MapObject.TouchHandling;
using Assets.MapObject.TriggerHandling;
using PathCreation;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.MapTool
{
    public class MeteorCreatorOptions : ScriptableObject
    {
        public Sprite meteorSprite = null;
        public enum TouchMode { Nothing, Die, Trigger};
        public TouchMode touchMode = TouchMode.Nothing;
        public bool isMobile = false;
        public GameObject pathObject = null;
        public string defaultPathPath = "Assets/Editor/MapTool/DefaultPath.prefab";
        public float fullCycleTime = 1;
        [Range(0, 1)]
        public float offsetTime = 0;
        public bool isRotating = false;
        public RotationScript rotationScript = null;
        public bool isHunter = false;
        public HuntProperties huntProperties = null;
        public GameObject huntZone = null;
        public string defaultHuntZonePath = "Assets/Editor/MapTool/DefaultHuntingZone.prefab";
        public bool isAppearing = false;
        public Appearing.InitState appearInitialState = Appearing.InitState.Invisible;
        public GameObject appearZone = null;
        public string defaultAppearZonePath = "Assets/Editor/MapTool/DefaultAppearZone.prefab";
        public string levelDataPath = "Assets/Levels/";
    }
}