using UnityEditor;

public static class MenuItemsForTemplates
{
    [MenuItem("Assets/Create/Code/MonoBehaviourScript", priority = 40)]
    public static void CreateMonoBehaviourMenuItem()
	{
		string templatePath = "Assets/_ALS/_Tools/ScriptTemplates/Editor/Templates/81-C#_Script-NewBehaviourScript.cs.txt";
		ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScript.cs");
	}

	[MenuItem("Assets/Create/Code/ScriptableObject", priority = 40)]
	public static void CreateScriptableObjectMenuItem()
	{
		string templatePath = "Assets/_ALS/_Tools/ScriptTemplates/Editor/Templates/83-C#_Scriptable Object-NewScriptableObjectScript.cs.txt";
		ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScript.cs");
	}
}
