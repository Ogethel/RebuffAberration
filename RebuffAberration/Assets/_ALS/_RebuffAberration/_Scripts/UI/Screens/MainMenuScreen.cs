using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace ALS.Aberration
{
    /// <summary>
    /// This class adds some custom text to the MainMenu screen and functionality to buttons.
    /// 
    /// </summary>
    public class MainMenuScreen : UIScreen
    {
        MenuButtonSO[] m_MenuButtonData;

        // The Demo Selector scene path
        [SerializeField] string m_DemosScenePath = "Unity Technologies/QuizU - A UI Toolkit demo/Assets/Demos/0_DemoSelection/DemoSelection.unity";
        [SerializeField] int m_DemosSceneIndex = 11;

		Label m_Description;        // Label element to display descriptions
		Button m_PlayButton;        // Button to switch to the Level Selection screen
        Button m_SettingsButton;    // Button to switch to the Settings screen
        Button m_QuitButton;        // Button to Quit the application from the main menu
        //Button m_DemosButton;       // Button load the DemoSelection scene
        //Button m_MoreButton;        // Button to open a URL to the sample project
        //Button m_BackButton;

        //VisualElement m_ButtonContainer1;
        //VisualElement m_ButtonContainer2;

        public MainMenuScreen(VisualElement parentElement) : base(parentElement)
        {
            SetVisualElements();
            RegisterCallbacks();
            ShowButtonContainer(1);
        }

        private void SetVisualElements()
        {
			m_Description = m_RootElement.Q<Label>("menu__description");
			m_Description.text = string.Empty;

			m_SettingsButton = m_RootElement.Q<Button>("menu__button-settings");
            m_PlayButton = m_RootElement.Q<Button>("menu__button-play");
            m_QuitButton = m_RootElement.Q<Button>("menu__button-quit");

            //m_DemosButton = m_RootElement.Q<Button>("menu__button-demos");

            //m_MoreButton = m_RootElement.Q<Button>("menu__button-more");
            //m_BackButton = m_RootElement.Q<Button>("menu__button-back");

            //m_ButtonContainer1 = m_RootElement.Q<VisualElement>("menu__button-container--1");
            //m_ButtonContainer2 = m_RootElement.Q<VisualElement>("menu__button-container--2");

            m_MenuButtonData = Resources.LoadAll<MenuButtonSO>("MenuButtonData");
            // The ScriptableObjects store the MenuButton and ElementID fields store references to the
            // Button and button id, so we don't have to store it here (we use this for the buttons that
            // link to the Dragon Crashers, UI Documentation, How To articles, and UI Artist resources
            Debug.Log("Tried to Load resources");
            for (int i = 0; i < m_MenuButtonData.Length; i++)
            {
                Debug.Log($"PreLoaded data {i} out of {m_MenuButtonData.Length}");
                m_MenuButtonData[i].MenuButton = m_RootElement.Q<Button>(m_MenuButtonData[i].ElementID);

                // Use the userData property to store custom description for use later
                m_MenuButtonData[i].MenuButton.userData = m_MenuButtonData[i].Description;
            }
        }

        private void RegisterCallbacks()
        {
            // Register System.Action delegates to each Button's ClickEvent. 
            m_EventRegistry.RegisterCallback<ClickEvent>(m_SettingsButton, evt => UIEvents.SettingsShown?.Invoke());
            m_EventRegistry.RegisterCallback<ClickEvent>(m_PlayButton, evt => UIEvents.GameScreenShown?.Invoke());

            //m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_PlayButton, FocusPlay);
            //m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_SettingsButton, FocusSettings);
            //m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_QuitButton, FocusQuit);
            //m_PlayButton.RegisterCallback<PointerEnterEvent>(evt => UIEvents.PlayFocused?.Invoke());
            m_SettingsButton.RegisterCallback<PointerEnterEvent>(evt => UIEvents.SettingsFocused?.Invoke());
            //m_QuitButton.RegisterCallback<PointerEnterEvent>(evt => UIEvents.QuitFocused?.Invoke());

            //m_EventRegistry.RegisterCallback<ClickEvent>(m_DemosButton, evt => LoadSceneByIndex(m_DemosSceneIndex));

            //m_EventRegistry.RegisterCallback<ClickEvent>(m_MoreButton, evt => ShowButtonContainer(2));
            //m_EventRegistry.RegisterCallback<ClickEvent>(m_BackButton, evt => ShowButtonContainer(1));

            // Loop through all MenuButtonData ScriptableObjects, get the corresponding Button objects,
            // and register callbacks for MouseEnterEvent and MouseLeaveEvent.
            for (int i = 0; i < m_MenuButtonData.Length; i++)
			{
                string switchString = m_MenuButtonData[i].ElementID;
                Debug.Log($"Loaded data {switchString}");
				switch (switchString)
				{
                    case "menu__button-play":
                        m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_MenuButtonData[i].MenuButton, FocusPlay);
                        break;
                    case "menu__button-settings":
                        m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_MenuButtonData[i].MenuButton, FocusSettings);
                        break;
                    case "menu__button-quit":
                        m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_MenuButtonData[i].MenuButton, FocusQuit);
                        break;
                }
				//m_EventRegistry.RegisterCallback<MouseEnterEvent>(m_MenuButtonData[i].MenuButton, EnterMenuHandler);
				m_EventRegistry.RegisterCallback<MouseLeaveEvent>(m_MenuButtonData[i].MenuButton, ExitMenuHandler);

				// If a MenuButton has a corresponding URL, verify the link and then set up the corresponding Button ClickEvent
				if (Uri.IsWellFormedUriString(m_MenuButtonData[i].URL, UriKind.Absolute))
				{
					m_EventRegistry.RegisterCallback<ClickEvent>(m_MenuButtonData[i].MenuButton, evt => OpenURL(evt.target as Button));
				}
			}
		}

        void FocusPlay(MouseEnterEvent evt)
		{
            Button eventButton = evt.target as Button;
            m_Description.text = (string)eventButton.userData;
            Debug.Log("FocusPlay was called in MainMenuScreen");
            EventBus<SwitchMenuFocus>.Raise(new SwitchMenuFocus
            {
                View = 0
            });
		}

        void FocusSettings(MouseEnterEvent evt)
        {
            Button eventButton = evt.target as Button;
            m_Description.text = (string)eventButton.userData;
            EventBus<SwitchMenuFocus>.Raise(new SwitchMenuFocus
            {
                View = 1
            });
        }

        void FocusQuit(MouseEnterEvent evt)
        {
            Button eventButton = evt.target as Button;
            m_Description.text = (string)eventButton.userData;
            EventBus<SwitchMenuFocus>.Raise(new SwitchMenuFocus
            {
                View = 2
            });
        }


        private void ShowButtonContainer(int index)
        {
            //if (index == 1)
            //{
            //    m_ButtonContainer1.style.display = DisplayStyle.Flex;
            //    m_ButtonContainer2.style.display = DisplayStyle.None;
            //}
            //else if (index == 2)
            //{
            //    m_ButtonContainer1.style.display = DisplayStyle.None;
            //    m_ButtonContainer2.style.display = DisplayStyle.Flex;
            //}
            //else
            //{
            //    Debug.LogWarning("[MenuMenuScreen]: Invalid button container...");
            //}
        }

        // Handle MouseEnterEvent by updating the description text based on the button being hovered.
        private void EnterMenuHandler(MouseEnterEvent evt)
        {
            // Get the button that raised the event
            Button eventButton = evt.target as Button;

			//// Update the description text using the previously stored custom data
			m_Description.text = (string)eventButton.userData;
		}

        // Handle MouseLeaveEvent by clearing the description text.
        private void ExitMenuHandler(MouseLeaveEvent evt)
        {
            //m_Description.text = string.Empty;
        }

        private void OpenURL(Button button)
        {
            for (int i = 0; i < m_MenuButtonData.Length; i++)
            {
                if (m_MenuButtonData[i].MenuButton == button)
                {
                    UIEvents.UrlOpened?.Invoke(m_MenuButtonData[i].URL);
                    break;
                }
            }
        }

        private void LoadScenePath(string scenePath)
        {
            SceneEvents.LoadSceneByPath?.Invoke(scenePath);
        }

        private void LoadSceneByIndex(int sceneIndex)
        {
            SceneEvents.SceneIndexLoaded?.Invoke(sceneIndex);
        }
    }
}
