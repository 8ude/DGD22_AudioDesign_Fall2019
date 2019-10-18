using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
namespace Tinylytics{
public class Tinylytics_ConfigEditor : EditorWindow {

	public Tinylytics_Config analyticsConfig;

	[MenuItem("Window/Tinylytics - Configure")]
	static void Init() {
		EditorWindow.GetWindow(typeof(Tinylytics_ConfigEditor));
	}

	void OnEnable() {
		if (EditorPrefs.HasKey("AnalyticsConfigObjectPath")) {
			string objectPath = EditorPrefs.GetString("AnalyticsConfigObjectPath");
			analyticsConfig = AssetDatabase.LoadAssetAtPath(objectPath, typeof(Tinylytics_Config)) as Tinylytics_Config;
		}
	}

	void OnGUI() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Analytics Configuration Settings", EditorStyles.whiteLargeLabel);
		GUILayout.EndHorizontal();


		if (analyticsConfig == null) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			GUILayout.Label("No Config File Detected", EditorStyles.boldLabel);
			GUILayout.Space(20);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Create New Config File", GUILayout.ExpandWidth(false))) {
				CreateNewConfigFile();
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Set to Existing Config File", GUILayout.ExpandWidth(false))) {
				OpenConfigFile();
			}
			GUILayout.EndHorizontal();
		} else if (analyticsConfig != null) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			if (GUILayout.Button("Show Config File")) {
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = analyticsConfig;
				return;
			}
			GUILayout.Space(20);
			GUILayout.EndHorizontal();


			GUILayout.BeginHorizontal();
			GUILayout.Label("Unique URL:", EditorStyles.boldLabel);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();

			analyticsConfig.uniqueURL = EditorGUILayout.TextField("Unique URL", analyticsConfig.uniqueURL as string);


			GUILayout.Space(60);

			//if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) {
			//	//AddItem();
			//}
			//if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) {
			//	//DeleteItem(viewIndex - 1);
			//}

			GUILayout.EndHorizontal();

			//Show what's in config here =>
			//if (1 > 0) {
			//	GUILayout.BeginHorizontal();
			//	viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.itemList.Count);
			//	//Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
			//	EditorGUILayout.LabelField("of   " + inventoryItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
			//	GUILayout.EndHorizontal();

			//	inventoryItemList.itemList[viewIndex - 1].itemName = EditorGUILayout.TextField("Item Name", inventoryItemList.itemList[viewIndex - 1].itemName as string);
			//	inventoryItemList.itemList[viewIndex - 1].itemIcon = EditorGUILayout.ObjectField("Item Icon", inventoryItemList.itemList[viewIndex - 1].itemIcon, typeof(Texture2D), false) as Texture2D;
			//	inventoryItemList.itemList[viewIndex - 1].itemObject = EditorGUILayout.ObjectField("Item Object", inventoryItemList.itemList[viewIndex - 1].itemObject, typeof(Rigidbody), false) as Rigidbody;

			//	GUILayout.Space(10);

			//	GUILayout.BeginHorizontal();
			//	inventoryItemList.itemList[viewIndex - 1].isUnique = (bool)EditorGUILayout.Toggle("Unique", inventoryItemList.itemList[viewIndex - 1].isUnique, GUILayout.ExpandWidth(false));
			//	inventoryItemList.itemList[viewIndex - 1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", inventoryItemList.itemList[viewIndex - 1].isIndestructible, GUILayout.ExpandWidth(false));
			//	inventoryItemList.itemList[viewIndex - 1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", inventoryItemList.itemList[viewIndex - 1].isQuestItem, GUILayout.ExpandWidth(false));
			//	GUILayout.EndHorizontal();

			//	GUILayout.Space(10);

			//	GUILayout.BeginHorizontal();
			//	inventoryItemList.itemList[viewIndex - 1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", inventoryItemList.itemList[viewIndex - 1].isStackable, GUILayout.ExpandWidth(false));
			//	inventoryItemList.itemList[viewIndex - 1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", inventoryItemList.itemList[viewIndex - 1].destroyOnUse, GUILayout.ExpandWidth(false));
			//	inventoryItemList.itemList[viewIndex - 1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", inventoryItemList.itemList[viewIndex - 1].encumbranceValue, GUILayout.ExpandWidth(false));
			//	GUILayout.EndHorizontal();

			//	GUILayout.Space(10);

			//} else {
			//	GUILayout.Label("This Inventory List is Empty.");
			//}
		}

		if (GUI.changed) {
			EditorUtility.SetDirty(analyticsConfig);
		}
	}

	void CreateNewConfigFile() {
		// There is no overwrite protection here!
		// There is No "Are you sure you want to overwrite your existing object?" if it exists.
		// This should probably get a string from the user to create a new name and pass it ...

		analyticsConfig = CreateAnalyticsConfigFile();
		if (analyticsConfig) {
		//initialize it:
			//inventoryItemList.itemList = new List<InventoryItem>();
			string relPath = AssetDatabase.GetAssetPath(analyticsConfig);
			EditorPrefs.SetString("AnalyticsConfigObjectPath", relPath);
		}
	}

	Tinylytics_Config CreateAnalyticsConfigFile() {
		Tinylytics_Config asset = ScriptableObject.CreateInstance<Tinylytics_Config>();
		AssetDatabase.CreateAsset(asset, "Assets/Resources/AnalyticsConfiguration.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}


	void OpenConfigFile() {
		string absPath = EditorUtility.OpenFilePanel("Select Config File", Application.dataPath, "asset");
		if (absPath.StartsWith(Application.dataPath)) {
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			analyticsConfig = AssetDatabase.LoadAssetAtPath(relPath, typeof(Tinylytics_Config)) as Tinylytics_Config;
			//if (analyticsConfig.itemList == null)
			//	inventoryItemList.itemList = new List<InventoryItem>();
			if (analyticsConfig) {
				EditorPrefs.SetString("AnalyticsConfigObjectPath", relPath);
			}
		}
	}



}
}