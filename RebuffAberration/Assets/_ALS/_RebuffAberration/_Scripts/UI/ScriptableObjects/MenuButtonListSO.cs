using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace ALS.Aberration
{
	/// <summary>
    /// MenuButtonListSO
    /// </summary>
	[CreateAssetMenu(fileName = "MenuButtonListSO", menuName = "Quiz/MenuButtonListSO")]
	public class MenuButtonListSO : DescriptionSO
	{
		[SerializeField] MenuButtonSO[] _menuButtonList;
		public MenuButtonSO[] MenuButtonList
		{
			get => _menuButtonList;
		}

		public MenuButtonSO GetMenuButtonById(string id)
		{
			foreach (MenuButtonSO data in _menuButtonList)
			{
				if(data.ElementID == id)
				{
					return data;
				}
			}

			return null;
		}

		public void PopulateMenuButtonList(MenuButtonSO[] menuButtonData)
		{
			if (menuButtonData != null && menuButtonData.Length != 0) _menuButtonList = menuButtonData;
		}
	}
}