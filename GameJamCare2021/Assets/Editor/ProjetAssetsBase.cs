using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
public class ProjetAssetsBase : EditorWindow {
    #region Declaration
    private bool firstTime = true;
    private int maxField = 25;
    private int toolbarInt = 0;
    private string[] toolbarStrings;
    private string errorMessage = "";
    private string assetPath = "Assets";
    private bool saveSystemGood = true;

    private string noNameFolder = "Folder have no name"; private string noNameScene = "Scene have no name"; private string noNameScript = "Script have no name";

    private int numberOfFolders = 0; private string sFolders = "Folders"; private List<string> foldersNames = new List<string>();
    private int numberOfScenes = 0; private string sScenes = "Scenes"; private List<string> scenesNames = new List<string>();
    private int numberOfScripts = 0; private string sScripts = "Scripts"; private List<string> scriptsNames = new List<string>();

    private List<(bool want, string name)> basicsFolders = new List<(bool want, string name)>() {
        (false, "Arts"), (false,"Animations"),
        (false,"Place Holder"), (false, "Prefabs"),
        (false,"Resources"), (false,"StreamingAssets")
    };

    private List<(bool want, string name, bool saveData, string nameAsVar)> basicsScripts = new List<(bool want, string name, bool saveData, string nameAsVar)>() {
        (false,"Managers",false, "managers"), (false,"GameManager",false, "gameManager"),
        (false,"UIManager",false,"uIManager"), (false,"SoundManager",false, "soundManager"),
        (false,"ScenesManager",false,"scenesManager"), (false,"DialogueManager",false,"dialogueManager")
    };
    private bool wantSaveSystem = false;
    private string sSaveSystem = "SaveSystem";

    #endregion

    [MenuItem("Window/ProjectAssetsBase")]
    public static void ShowWindow() { EditorWindow.GetWindow<ProjetAssetsBase>("ProjectAssetsBase"); }

    private void OnGUI() {
        if (firstTime) {
            for (int i = 0; i < maxField; i++) {
                foldersNames.Add("");
                scenesNames.Add("");
                scriptsNames.Add("");
            }
            firstTime = !firstTime;
        }
        GUILayout.Label("Edit the Project Assets Base.", EditorStyles.whiteLargeLabel);
        GUILayout.Space(10);
        toolbarStrings = new string[] { sFolders, sScenes, sScripts };
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
        switch (toolbarInt) {
            case 0:
                DisplayFoldersSettings();
                break;
            case 1:
                DisplayScenesSettings();
                break;
            case 2:
                DisplayScriptsSettings();
                break;
        }
        if (!saveSystemGood){
            GUI.contentColor = Color.red;
        }
        if (GUILayout.Button($"Create only {toolbarStrings[toolbarInt]}")) {
            if (!saveSystemGood) return;
            bool created = false;
            switch (toolbarInt) {
                case 0:
                    created = CreateFolders();
                    break;
                case 1:
                    created = CreateScenes();
                    break;
                case 2:
                    created = CreateScripts();
                    break;
            }
            if (created)
                Debug.Log($"<color=green><size=20>{toolbarStrings[toolbarInt]}</size> have been corretly created. </color>");
            else {
                Debug.Log($"<color=red><size=20>Error : </size> {toolbarStrings[toolbarInt]} haven't beeen created corretly. {errorMessage} </color>");
                errorMessage = "";
            }
        }
        if (GUILayout.Button($"Create all {sFolders}, {sScenes} and {sScripts}")) {
            if (!saveSystemGood) return;
            if (CreateFolders() && CreateScenes() && CreateScripts())
                Debug.Log("<color=green> Assets have been corretly created. </color>");
            else {
                Debug.Log($"<color=red><size=20>Error : </size> Assets haven't beeen created corretly. {errorMessage} </color>");
                errorMessage = "";
            }
        }
    }

    #region DisplaySettings
    private void DisplayFoldersSettings() {
        GUILayout.Label($"Basics {sFolders}", EditorStyles.centeredGreyMiniLabel);
        for (int i = 0; i < basicsFolders.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            basicsFolders[i] = (GUILayout.Toggle(basicsFolders[i].want, basicsFolders[i].name), basicsFolders[i].name);
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Label($"Can create {maxField} {sFolders} maximum at once.", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label($"How much {sFolders} do you want?");
        GUILayout.Label("                            ");
        GUILayout.Label("                            ");
        numberOfFolders = (int)EditorGUILayout.Slider(numberOfFolders, 0, maxField);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginFoldoutHeaderGroup(numberOfFolders > 0, $"Names of the {sFolders}");
        if (numberOfFolders > 0) {
            for (int i = 0; i < numberOfFolders; i++) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Name of the Folder you wanna create : ");
                GUILayout.Label("                            ");
                foldersNames[i] = EditorGUILayout.TextField(foldersNames[i]);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    private void DisplayScenesSettings() {
        GUILayout.Label($"Can create {maxField} {sScenes} maximum at once.", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label($"How much {sScenes} do you want?");
        GUILayout.Label("                            ");
        GUILayout.Label("                            ");
        numberOfScenes = (int)EditorGUILayout.Slider(numberOfScenes, 0, maxField);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginFoldoutHeaderGroup(numberOfScenes > 0, $"Names of the {sScenes}");
        if (numberOfScenes > 0) {
            for (int i = 0; i < numberOfScenes; i++) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Name of the Scene you wanna create : ");
                GUILayout.Label("                            ");
                scenesNames[i] = EditorGUILayout.TextField(scenesNames[i]);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    private void DisplayScriptsSettings() {
        GUILayout.Label($"SaveSytem for any basics {sScripts}", EditorStyles.centeredGreyMiniLabel);
        if (wantSaveSystem) {
            for (int i = 0; i < basicsScripts.Count; i++) {
                if (basicsScripts[i].want && basicsScripts[i].saveData) {
                    saveSystemGood = true;
                    break;
                } else {
                    saveSystemGood = false;
                }
            }
            if (!saveSystemGood) {
                GUI.contentColor = Color.red;
                GUILayout.Label($"You have to want a SaveData for any basics {sScripts}", EditorStyles.centeredGreyMiniLabel);
            }
        } else {
            saveSystemGood = true;
        }
        wantSaveSystem = GUILayout.Toggle(wantSaveSystem, sSaveSystem);
        GUI.contentColor = Color.white;

        GUILayout.Label($"Basics {sScripts}", EditorStyles.centeredGreyMiniLabel);
        for (int i = 0; i < basicsScripts.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            basicsScripts[i] = (GUILayout.Toggle(basicsScripts[i].want, basicsScripts[i].name), basicsScripts[i].name, basicsScripts[i].saveData, basicsScripts[i].nameAsVar);
            EditorGUILayout.EndHorizontal();
            if (wantSaveSystem && basicsScripts[i].want && i != 0) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Do you want a SaveData for this script?");
                basicsScripts[i] = (basicsScripts[i].want, basicsScripts[i].name, EditorGUILayout.Toggle(basicsScripts[i].saveData), basicsScripts[i].nameAsVar);
                EditorGUILayout.EndHorizontal();
            }
        }

        GUILayout.Label($"Can create {maxField} {sScripts} maximum at once.", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label($"How much {sScripts} do you want?");
        GUILayout.Label("                            ");
        GUILayout.Label("                            ");
        numberOfScripts = (int)EditorGUILayout.Slider(numberOfScripts, 0, maxField);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginFoldoutHeaderGroup(numberOfScripts > 0, $"Names of the {sScripts}");
        if (numberOfScripts > 0) {
            for (int i = 0; i < numberOfScripts; i++) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Name of the Script you wanna create : ");
                GUILayout.Label("                            ");
                scriptsNames[i] = EditorGUILayout.TextField(scriptsNames[i]);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    #endregion

    #region CreateAssets
    private bool CreateFolders() {
        if (numberOfFolders > 0) {
            for (int i = 0; i < numberOfFolders; i++) {
                if (foldersNames[i] == "") {
                    errorMessage = $"N°{i}" + noNameFolder;
                    return false;
                }
                CreatePath(foldersNames[i], 0);
            }
        }
        for (int index = 0; index < basicsFolders.Count; index++) {
            if (basicsFolders[index].want) {
                CreatePath(basicsFolders[index].name, 0);
            }
        }
        return true;
    }

    private bool CreateScenes() {
        if (numberOfScenes > 0) {
            for (int i = 0; i < numberOfScenes; i++) {
                if (scenesNames[i] == "") {
                    errorMessage = $"N°{i}" + noNameScene;
                    return false;
                }
                CreatePath(scenesNames[i], 1);
            }
        }
        return true;
    }

    private bool CreateScripts() {
        if (numberOfScripts > 0) {
            for (int i = 0; i < numberOfScripts; i++) {
                if (scriptsNames[i] == "") {
                    errorMessage = $"N°{i}" + noNameScript;
                    return false;
                }
                CreatePath(scriptsNames[i], 2);
            }
        }
        for (int index = 0; index < basicsScripts.Count; index++) {
            if (basicsScripts[index].want) {
                CreatePath(basicsScripts[index].name, 2, basicsScripts[index].saveData, index);
            }
        }
        if (wantSaveSystem && (File.Exists($"{assetPath}/{sScripts}/{sSaveSystem}") == false))
            WriteScript(sSaveSystem + ".cs", -1);
        AssetDatabase.Refresh();
        return true;
    }
    /// <summary>
    /// Check if a path already Exists. Else create the path
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param> 0 = Folder, 1 = Scene, 2 = Script
    private void CreatePath(string name, int type) {
        switch (type) {
            default:
                if (!Directory.Exists($"{assetPath}/{name}")) {
                    AssetDatabase.CreateFolder(assetPath, name);
                }
                break;
            case 1:
                name += ".unity";
                if (!Directory.Exists($"{assetPath}/{sScenes}")) {
                    AssetDatabase.CreateFolder(assetPath, sScenes);
                }
                if (!File.Exists($"{assetPath}/{sScenes}/{name}")) {
                    Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                    EditorSceneManager.SaveScene(newScene, $"{assetPath}/{sScenes}/{name}");
                }
                break;
            case 2:
                name += ".cs";
                if (!Directory.Exists($"{assetPath}/{sScripts}")) {
                    AssetDatabase.CreateFolder(assetPath, sScripts);
                }
                if (!File.Exists($"{assetPath}/{sScripts}/{name}")) {
                    WriteScript(name, -1);
                }
                break;
        }

    }

    private void CreatePath(string name, int type, bool saveData, int index) {
        if (!saveData) {
            CreatePath(name, type);
            return;
        }
        name += ".cs";
        if (!Directory.Exists($"{assetPath}/{sScripts}")) {
            AssetDatabase.CreateFolder(assetPath, sScripts);
        }
        if (!File.Exists($"{assetPath}/{sScripts}/{name}")) {
            WriteScript(name, index);
        }
    }

    private void WriteScript(string name, int index) {
        using (StreamWriter outfile = new StreamWriter($"{assetPath}/{sScripts}/{name}")) {
            switch (name) {
                case "SaveSystem.cs":
                    SaveSystem(outfile);
                    return;
                case "Managers.cs":
                    Managers(outfile);
                    return;
                case "UIManager.cs":
                    Using(outfile);
                    outfile.WriteLine("using UnityEngine.UI;");
                    break;
                case "ScenesManager.cs":
                    Using(outfile);
                    outfile.WriteLine("using UnityEngine.SceneManagement;");
                    break;
                default:
                    Using(outfile);
                    break;
            }
            outfile.WriteLine("");
            if (index > -1) SaveData(outfile, index);
            outfile.WriteLine("public class " + name.Remove(name.Length - 3) + " : MonoBehaviour {");
            outfile.WriteLine("");
            if (name.Contains("Manager")) Singleton(outfile, name);
            outfile.WriteLine("");
            MonoBehaviour(outfile);
            if (index > -1) SaveDataFunction(outfile, index);
            outfile.WriteLine("}");
        }
    }

    void Using(StreamWriter outfile) {
        outfile.WriteLine("using UnityEngine;");
        outfile.WriteLine("using System.Collections;");
        outfile.WriteLine("using System;");
    }
    void SaveData(StreamWriter outfile, int index) {
        outfile.WriteLine("[System.Serializable]");
        outfile.WriteLine("public class " + basicsScripts[index].name + "Data {");
        outfile.WriteLine("    //Declarate the variables as public from the script below. Example -> public var var;");
        outfile.WriteLine("");
        outfile.WriteLine("    public " + basicsScripts[index].name + "Data(" + basicsScripts[index].name + " " + basicsScripts[index].nameAsVar + ") {");
        outfile.WriteLine($"        //Assign the variables here from the {basicsScripts[index].name}. Example -> var = {basicsScripts[index].nameAsVar}.var;");
        outfile.WriteLine("    }");
        outfile.WriteLine("}");
    }
    void SaveDataFunction(StreamWriter outfile, int index) {
        outfile.WriteLine("");
        outfile.WriteLine("    public void Save" + basicsScripts[index].name + "(){");
        outfile.WriteLine($"        SaveSystem.Save{basicsScripts[index].name}(this);");
        outfile.WriteLine("    }");
        outfile.WriteLine("");
        outfile.WriteLine("    public void Load" + basicsScripts[index].name + "(){");
        outfile.WriteLine($"        {basicsScripts[index].name}Data data = SaveSystem.Load{basicsScripts[index].name}();");
        outfile.WriteLine("    }");
    }
    void Singleton(StreamWriter outfile, string name) {
        outfile.WriteLine("    public static " + name.Remove(name.Length - 3) + " Instance { get; private set; }");
        outfile.WriteLine("");
        outfile.WriteLine("    private void Awake() {");
        outfile.WriteLine("        Instance = this;");
        outfile.WriteLine("    }");
    }
    void MonoBehaviour(StreamWriter outfile) {
        outfile.WriteLine("    private void Start() {");
        outfile.WriteLine("");
        outfile.WriteLine("    }");
        outfile.WriteLine("");
        outfile.WriteLine("");
        outfile.WriteLine("    private void Update() {");
        outfile.WriteLine("");
        outfile.WriteLine("    }");
    }
    void SaveSystem(StreamWriter outfile) {
        outfile.WriteLine("using UnityEngine;");
        outfile.WriteLine("using System.IO;");
        outfile.WriteLine("using System.Runtime.Serialization.Formatters.Binary;");
        outfile.WriteLine("");
        outfile.WriteLine("public static class SaveSystem {");
        for (int i = 0; i < basicsScripts.Count; i++) {
            if (basicsScripts[i].saveData) {
                outfile.WriteLine($"#region {basicsScripts[i].name}");
                outfile.WriteLine("    public static void Save" + basicsScripts[i].name + "(" + basicsScripts[i].name + " " + basicsScripts[i].nameAsVar + ") {");
                outfile.WriteLine("         string path = Application.persistentDataPath + \"/" + basicsScripts[i].name + ".data\"; ");
                outfile.WriteLine("         BinaryFormatter formatter = new BinaryFormatter();");
                outfile.WriteLine("         FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);");
                outfile.WriteLine("         " + basicsScripts[i].name + "Data data = new " + basicsScripts[i].name + "Data(" + basicsScripts[i].nameAsVar + ");");
                outfile.WriteLine("         formatter.Serialize(stream, data);");
                outfile.WriteLine("         stream.Close();");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public static " + basicsScripts[i].name + "Data Load" + basicsScripts[i].name + "() {");
                outfile.WriteLine("         string path = Application.persistentDataPath + \"/" + basicsScripts[i].name + ".data\"; ");
                outfile.WriteLine("        if (File.Exists(path)) {");
                outfile.WriteLine("            BinaryFormatter formatter = new BinaryFormatter();");
                outfile.WriteLine("            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);");
                outfile.WriteLine("            " + basicsScripts[i].name + "Data data = formatter.Deserialize(stream) as " + basicsScripts[i].name + "Data;");
                outfile.WriteLine("            stream.Close();");
                outfile.WriteLine("            return data;");
                outfile.WriteLine("        } else {");
                outfile.WriteLine("            Debug.LogError(\"Save file not found at \" + path);");
                outfile.WriteLine("            return null;");
                outfile.WriteLine("        }");
                outfile.WriteLine("    }");
                outfile.WriteLine("#endregion");
            }
        }
        outfile.WriteLine("}");
    }
    void Managers(StreamWriter outfile) {
        outfile.WriteLine("using UnityEngine;");
        outfile.WriteLine("using UnityEngine.SceneManagement;");
        outfile.WriteLine("public class Managers : MonoBehaviour {");
        outfile.WriteLine("");
        outfile.WriteLine("    void Start() {");
        outfile.WriteLine("        DontDestroyOnLoad(this.gameObject);");
        outfile.WriteLine("        SceneManager.LoadScene(\"MainMenu\");");
        outfile.WriteLine("    }");
        outfile.WriteLine("}");
    }
    #endregion
}