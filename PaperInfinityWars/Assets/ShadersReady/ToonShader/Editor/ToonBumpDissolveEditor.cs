using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ToonBumpDissolveEditor : ShaderGUI
{

	bool showCustomInspector = true;

	bool colorsBool = true;
	Color mainColor;
	Color highlights;
	Color shadows;
	Color outlineColor;
	float outlineWidth;


	bool texturesBool = true;
	Vector2 diffuseTiling;
	Texture2D diffuse;
	Vector2 normalMapTiling;
	Texture2D normalMap;
	Vector2 toonRampTiling;
	Texture2D toonRamp;

	bool dissolveBool = true;
	float dissolveAmount;
	Color dissolveColor;
	Vector2 dissolveRampTiling;
	Texture2D dissolveRamp;
	Vector2 dissolveNoiseTiling;
	Texture2D dissolveNoise;

	string GetAssetPath()
	{
		string name = "ToonBumpDissolveEditor";
		string[] temp = AssetDatabase.FindAssets(name, null);
		//Debug.Log(AssetDatabase.GUIDToAssetPath(temp[0]).Remove(AssetDatabase.GUIDToAssetPath(temp[0]).Length - 25));

		return AssetDatabase.GUIDToAssetPath(temp[0]).Remove(AssetDatabase.GUIDToAssetPath(temp[0]).Length - (name.Length + 3));
	}
	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
	{
		showCustomInspector = EditorGUILayout.Toggle("Use Custom Inspector", showCustomInspector);
		if (!showCustomInspector)
		{
			base.OnGUI(materialEditor, properties);
		}
		else
		{
			EditorGUI.BeginChangeCheck();//Start Checking if something Changed
			Material targetMat = materialEditor.target as Material;//Take material
			#region setCustomAssets
			//FONT
			Font customFont = AssetDatabase.LoadAssetAtPath(GetAssetPath() + "Simple.ttf", typeof(Font)) as Font;
			//Textures
			Texture texturesTex = AssetDatabase.LoadAssetAtPath(GetAssetPath() + "Textures.png", typeof(Texture2D)) as Texture2D;
			//Dissolve
			Texture dissolveTex = AssetDatabase.LoadAssetAtPath(GetAssetPath() + "Dissolve.png", typeof(Texture2D)) as Texture2D;
			#endregion
			#region GUIStyles generals
			//STYLE FOR FOLDOUTS
			GUIStyle foldoutTitleStyle = new GUIStyle("Foldout");//New GUIStyle taking Foldout as reference
			foldoutTitleStyle.font = customFont;//Change font
			foldoutTitleStyle.fontSize = 20;//Change font size
			foldoutTitleStyle.fixedHeight = 20;
			foldoutTitleStyle.margin.left = 15;
			//OPTION
			GUILayoutOption[] colorsLayout = new GUILayoutOption[4];
			colorsLayout[0] = GUILayout.MaxHeight(40);
			colorsLayout[1] = GUILayout.MinHeight(20);
			colorsLayout[2] = GUILayout.MinWidth(20);
			colorsLayout[3] = GUILayout.MaxWidth(500);
			#endregion
			#region TitleImage
			//GetImage
			//Texture titleTex = AssetDatabase.LoadAssetAtPath(), typeof(Texture2D)) as Texture2D;
			Texture titleTex = AssetDatabase.LoadAssetAtPath(GetAssetPath() + "ToonBumpDissolve.png", typeof(Texture2D)) as Texture2D;//Get Title image
			GUIContent title = new GUIContent(titleTex);//Set it to a content to be able to use the image
														//Options
			GUILayoutOption[] titleOptions = new GUILayoutOption[2];//Change some options
			titleOptions[0] = GUILayout.MaxWidth(250);
			titleOptions[1] = GUILayout.MaxHeight(150);
			//Draw
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(title, titleOptions);//Image encapsulated to be centered
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			#endregion
			#region Colors Group
			mainColor = EditorGUILayout.ColorField(new GUIContent("Main Color"), targetMat.GetColor("_MainColor"), false, false, false, colorsLayout);
			highlights = EditorGUILayout.ColorField(new GUIContent("Highlights Color"), targetMat.GetColor("_Highlights"), false, false, false, colorsLayout);
			shadows = EditorGUILayout.ColorField(new GUIContent("Shadows Color"), targetMat.GetColor("_Shadows"), false, false, false, colorsLayout);
			outlineWidth = EditorGUILayout.Slider("Outline Width", targetMat.GetFloat("_OutlineWidth"), 0, 1);
			outlineColor = EditorGUILayout.ColorField(new GUIContent("Outline Color"), targetMat.GetColor("_OutlineColor"), false, false, false, colorsLayout);
			GUILayout.Space(10);
			#endregion

			#region Dissolve
			GUILayout.BeginHorizontal("box");
			GUILayout.BeginVertical();
			GUIContent dissolveFoldoutTitle = new GUIContent("Dissolve", dissolveTex);//The content of the foldout: the label + an image

			dissolveBool = EditorGUILayout.Foldout(dissolveBool, dissolveFoldoutTitle, true, foldoutTitleStyle);
			GUILayout.Space(10);
			if (dissolveBool)
			{
				dissolveAmount = EditorGUILayout.Slider("Dissolve Amount", targetMat.GetFloat("_DisolveAmmount"), 0, 1);
				dissolveColor = EditorGUILayout.ColorField(new GUIContent("Dissolve Color"), targetMat.GetColor("_DissolveColor"), false, false, false, colorsLayout);
				dissolveRampTiling = EditorGUILayout.Vector2Field("DissolveRamp Tiling", targetMat.GetTextureScale("_DissolveRamp"));
				dissolveRamp = EditorGUILayout.ObjectField("DissolveRamp", targetMat.GetTexture("_DissolveRamp"), typeof(Texture2D), false) as Texture2D;
				dissolveNoiseTiling = EditorGUILayout.Vector2Field("DissolveGuide Tiling", targetMat.GetTextureScale("_DisolveNoise"));
				dissolveNoise = EditorGUILayout.ObjectField("DissolveGuide", targetMat.GetTexture("_DisolveNoise"), typeof(Texture2D), false) as Texture2D;
			}

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(15);
			#endregion

			#region Textures
			GUILayout.BeginHorizontal("box");
			GUILayout.BeginVertical();
			GUIContent texturesFoldoutTitle = new GUIContent("Textures", texturesTex);//The content of the foldout: the label + an image

			texturesBool = EditorGUILayout.Foldout(texturesBool, texturesFoldoutTitle, true, foldoutTitleStyle);
			GUILayout.Space(10);
			if (texturesBool)
			{
				diffuseTiling = EditorGUILayout.Vector2Field("Diffuse Tiling", targetMat.GetTextureScale("_Diffuse"));
				diffuse = EditorGUILayout.ObjectField("Diffuse", targetMat.GetTexture("_Diffuse"), typeof(Texture2D), false) as Texture2D;
				normalMapTiling = EditorGUILayout.Vector2Field("NormalMap Tiling", targetMat.GetTextureScale("_NormalMap"));
				normalMap = EditorGUILayout.ObjectField("NormalMap", targetMat.GetTexture("_NormalMap"), typeof(Texture2D), false) as Texture2D;
				toonRampTiling = EditorGUILayout.Vector2Field("ToonRamp Tiling", targetMat.GetTextureScale("_ToonRamp"));
				toonRamp = EditorGUILayout.ObjectField("ToonRamp", targetMat.GetTexture("_ToonRamp"), typeof(Texture2D), false) as Texture2D;
			}

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(15);
			#endregion


			//When something changed
			if (EditorGUI.EndChangeCheck())
			{
				targetMat.SetColor("_MainColor", mainColor);
				targetMat.SetColor("_Highlights", highlights);
				targetMat.SetColor("_Shadows", shadows);
				targetMat.SetFloat("_OutlineWidth", outlineWidth);
				targetMat.SetColor("_OutlineColor", outlineColor);

				targetMat.SetTextureScale("_Diffuse", diffuseTiling);
				targetMat.SetTexture("_Diffuse", diffuse);
				targetMat.SetTextureScale("_NormalMap", normalMapTiling);
				targetMat.SetTexture("_NormalMap", normalMap);
				targetMat.SetTextureScale("_ToonRamp", toonRampTiling);
				targetMat.SetTexture("_ToonRamp", toonRamp);

				targetMat.SetFloat("_DisolveAmmount", dissolveAmount);
				targetMat.SetColor("_DissolveColor", dissolveColor);
				targetMat.SetTextureScale("_DissolveRamp", dissolveRampTiling);
				targetMat.SetTexture("_DissolveRamp", dissolveRamp);
				targetMat.SetTextureScale("_DisolveNoise", dissolveNoiseTiling);
				targetMat.SetTexture("_DisolveNoise", dissolveNoise);
			}
		}
	}
}
