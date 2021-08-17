namespace Assets.MapTool
{
    using UnityEngine;
    using UnityEditor;
    using Assets.MapObject.TouchHandling;
    using PathCreation;
    using Assets.MapObject.Rotation;
    using Assets.MapObject.Meteor;
    using Assets.MapObject.Motion;
    using UnityEditor.SceneManagement;
    using System.IO;
    using System.Collections.Generic;

    public class MeteorCreator : EditorWindow
    {
        private const int LAYER_SCENE_OBJECTS = 16;
        private const int LAYER_TRANSPARENT_OBJECTS = 17;

        private static MeteorCreatorOptions options;
        private PathCreator pathCreator = null;
        private GameObject huntZone = null;
        [MenuItem("Window/Meteor Creator %#m")]
        public static void ShowWindow()
        {
            GetWindow<MeteorCreator>("Meteor Creator");
        }

        private void OnEnable()
        {
            if (!options)
            {
                options = AssetDatabase.LoadAssetAtPath<MeteorCreatorOptions>("Assets/Editor/MapTool/MeteorCreatorOptions-default.asset");

                if (!options)
                {
                    options = CreateInstance<MeteorCreatorOptions>();

                    AssetDatabase.CreateAsset(options, "Assets/Editor/MapTool/MeteorCreatorOptions-default.asset");
                    AssetDatabase.SaveAssets();

                    EditorUtility.FocusProjectWindow();

                    Selection.activeObject = options;
                }
                
            }
        }

        private void OnGUI()
        {
            #region Texture

            if (GUILayout.Button("Choose Meteor"))
            {
                EditorGUIUtility.ShowObjectPicker<Sprite>(options.meteorSprite, false, "meteor_ t:Sprite", 1);
            }

            if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == 1)
                options.meteorSprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();

            if (options.meteorSprite)
                GUILayout.Label(options.meteorSprite.texture, GUILayout.MaxHeight(200));

            #endregion

            #region Touch

            options.touchMode = (MeteorCreatorOptions.TouchMode)EditorGUILayout.EnumPopup("Touch handling", options.touchMode);

            #endregion

            #region Motion

            EditorGUILayout.BeginHorizontal();

            options.isMobile = EditorGUILayout.Toggle("Motion", options.isMobile);

            if (options.isMobile)
            {
                if (!pathCreator)
                    if (!options.pathObject)
                    {
                        GameObject pathObject = new GameObject("Path: Meteor Creator");
                        pathObject.AddComponent(typeof(PathCreator));
                        pathCreator = pathObject.GetComponent<PathCreator>();
                        pathCreator.bezierPath.Space = PathSpace.xy;
                        pathCreator.EditorData.bezierHandleScale = 15;
                        pathCreator.EditorData.showTransformTool = false;
                        pathCreator.bezierPath.IsClosed = true;
                    }
                    else
                    {
                        pathCreator = Instantiate(options.pathObject).GetComponent<PathCreator>();
                        pathCreator.gameObject.name = "Path: Meteor Creator";
                    }
            }
            else
            {
                UnloadPath();
            }

            EditorGUILayout.EndHorizontal();

            if (options.isMobile)
            {
                options.fullCycleTime = EditorGUILayout.FloatField("Map time of full cycle", options.fullCycleTime);
                options.offsetTime = EditorGUILayout.FloatField("Starting point", options.offsetTime);
            }

            #endregion

            #region Rotation

            EditorGUILayout.BeginHorizontal();
            options.isRotating = EditorGUILayout.Toggle("Rotation", options.isRotating);

            if (options.isRotating)
            {
                options.rotationScript = (RotationScript)EditorGUILayout.ObjectField(options.rotationScript, typeof(RotationScript), false);
            }
            EditorGUILayout.EndHorizontal();

            #endregion

            #region Hunt

            EditorGUILayout.BeginHorizontal();

            options.isHunter = EditorGUILayout.Toggle("Hunter", options.isHunter);

            if (options.isHunter)
            {
                options.huntProperties = (HuntProperties)EditorGUILayout.ObjectField(options.huntProperties, typeof(HuntProperties), false);
            }

            EditorGUILayout.EndHorizontal();

            if (options.isHunter)
            {
                if (!huntZone)
                    if (options.huntZone)
                    {
                        huntZone = Instantiate(options.huntZone);
                        huntZone.name = "Hunting Zone: Meteor Creator";
                    }
                    else
                    {
                        huntZone = new GameObject("Hunting Zone: Meteor Creator");
                        huntZone.AddComponent<BoxCollider2D>();
                    }
            }
            else
            {
                UnloadHuntingZone();
            }

            #endregion

            if (ValidData())
            {
                if (GUILayout.Button("Place"))
                {
                    Place();
                    Debug.Log("Placed Meteor");
                }
            }
        }

        private void Place()
        {
            GameObject meteor = new GameObject(options.meteorSprite ? options.meteorSprite.name : "meteor");

            SpriteRenderer spriteRenderer = meteor.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = options.meteorSprite;

            meteor.AddComponent<PolygonCollider2D>();

            Rigidbody2D rigidbody = meteor.AddComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;

            MeteorBehaviour meteorBehaviour = meteor.AddComponent<MeteorBehaviour>();

            TouchHandler touchHandler;
            switch (options.touchMode)
            {
                case MeteorCreatorOptions.TouchMode.Nothing:
                    touchHandler = meteor.AddComponent<NothingOnTouch>();
                    meteor.layer = LAYER_TRANSPARENT_OBJECTS;
                    spriteRenderer.sortingLayerName = "Background";
                    spriteRenderer.sortingOrder = 50;
                    break;
                case MeteorCreatorOptions.TouchMode.Die:
                    touchHandler = meteor.AddComponent<DieOnTouch>();
                    meteor.layer = LAYER_SCENE_OBJECTS;
                    spriteRenderer.sortingLayerName = "MapObjects";
                    break;
                case MeteorCreatorOptions.TouchMode.Trigger:
                    touchHandler = meteor.AddComponent<TriggerOnTouch>();
                    meteor.layer = LAYER_TRANSPARENT_OBJECTS;
                    spriteRenderer.sortingLayerName = "Background";
                    spriteRenderer.sortingOrder = 50;
                    break;
            }

            if (options.isMobile)
            {
                GameObject path = Instantiate(pathCreator.gameObject);
                path.name = "Path: " + (options.meteorSprite ? options.meteorSprite.name : "unknown");
                PathFollower pathFollower = meteor.AddComponent<PathFollower>();
                pathFollower.pathCreator = path.GetComponent<PathCreator>();
                pathFollower.fullCycleTime = options.fullCycleTime;
                pathFollower.offset = options.offsetTime;
            }
            else
            {
                meteor.AddComponent<Stationary>();
                meteor.transform.position = new Vector3(0, 0, 0);
            }

            if (options.isRotating)
            {
                RotationFollower rotationFollower = meteor.AddComponent<RotationFollower>();
                rotationFollower.rotationScript = options.rotationScript;
                meteor.transform.rotation = Quaternion.Euler(0, 0, options.rotationScript.GetRotation(0f));
            }
            else
                meteor.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (options.isHunter)
            {
                Hunter hunter = meteor.AddComponent<Hunter>();
                hunter.properties = options.huntProperties;

                GameObject zone = Instantiate(huntZone);
                zone.name = "Hunting Zone: " + (options.meteorSprite ? options.meteorSprite.name : "unknown");
                zone.layer = 16;
                TriggerOnTouch triggerZone = zone.AddComponent<TriggerOnTouch>();
                triggerZone.triggerHandlers = new List<Component> { hunter };

                EditorUtility.FocusProjectWindow();
            }
        }

        private void UnloadPath()
        {
            if (pathCreator)
            {
                options.pathObject = PrefabUtility.SaveAsPrefabAsset(pathCreator.gameObject, options.defaultPathPath);
                DestroyImmediate(pathCreator.gameObject);
            }
        }

        private void UnloadHuntingZone()
        {
            if (huntZone)
            {
                options.huntZone = PrefabUtility.SaveAsPrefabAsset(huntZone.gameObject, options.defaultHuntZonePath);
                DestroyImmediate(huntZone.gameObject);
            }
        }

        private bool ValidData()
        {
            return
                options &&
                options.meteorSprite &&
                (!options.isMobile || options.pathObject && options.pathObject.GetComponent<PathCreator>() && options.fullCycleTime > 0) &&
                (!options.isRotating || options.rotationScript) &&
                (!options.isHunter || (options.huntProperties && options.huntZone));

        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(options.levelDataPath + EditorSceneManager.GetActiveScene().name + "_"))
            {
                Directory.CreateDirectory(options.levelDataPath + EditorSceneManager.GetActiveScene().name + "_");
            }
        }

        private void OnDisable()
        {
            UnloadPath();
            UnloadHuntingZone();
        }
    }
}