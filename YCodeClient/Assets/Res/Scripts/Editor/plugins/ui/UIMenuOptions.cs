﻿using System.Collections.Generic;
using edit.pure.etui.utils;
using mono.ui.controls;
using mono.ui.elements;
using pure.ui.core;
using pure.ui.element;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace plugins.ui {
    internal static class UIMenuOptions {
        [MenuItem("GameObject/UI/Text", false, 2030)]
        internal static void AddText(MenuCommand menuCommand) {
            GameObject go = Create_Text.Create<PText>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Image", false, 2030)]
        internal static void AddImage(MenuCommand menuCommand) {
            GameObject go = Create_Image.Create<PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Repeat Image", false, 2030)]
        internal static void AddRepeatImag(MenuCommand menuCommand) {
            GameObject go = Create_RepeatImage.Create<PRepeatImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/TabButton", false, 2031)]
        internal static void AddTabButton(MenuCommand menuCommand) {
            GameObject go = Creator_TabButton.Create<Tab, PText, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Button", false, 2032)]
        internal static void AddButton(MenuCommand menuCommand) {
            GameObject go = Creator_Button.Create<PButton, PText, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Toggle", false, 2033)]
        internal static void AddToggle(MenuCommand menuCommand) {
            GameObject go = Creator_Toggle.Create<PToggle, PText, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Dropdown", false, 2034)]
        internal static void AddDropDown(MenuCommand menuCommand) {
            GameObject go = Creator_Dropdonw.Create<PDropdown, PText, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Panel", false, 2035)]
        internal static void AddPanel(MenuCommand menuCommand) {
            GameObject go = Create_Panel.Create<PPanel_Dll>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Scroll View", false, 2080)]
        internal static void AddScrollview(MenuCommand menuCommand) {
            GameObject go = Creator_ScrollView.Create<ScrollView, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Bar", false, 2081)]
        internal static void AddBar(MenuCommand menuCommand) {
            GameObject go = Creator_Bar.Create<Bar, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Star", false, 2082)]
        internal static void AddStar(MenuCommand menuCommand) {
            GameObject go = Creator_PStar.Create<Star, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Resource", false, 2083)]
        internal static void AddResource(MenuCommand menuCommand) {
            GameObject go = Creator_Resource.Create<ResourceField, PText, PImage>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Rich Text", false, 2084)]
        internal static void AddRichText(MenuCommand menuCommand) {
            GameObject go = Create_RichText.Create<RichText, SpriteGraphic>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/Number Field", false, 2085)]
        internal static void AddNumberField(MenuCommand menuCommand) {
            GameObject go = Create_NumberField.Create<NumberField>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI/MovieClip", false, 2086)]
        internal static void AddMovieClip(MenuCommand menuCommand) {
            GameObject go = Create_MovieClip.Create<MovieClip>(UIMenuControls.GetStandardResources());
            UIMenuControls.PlaceUIElementRoot(go, menuCommand);
        }

        [InitializeOnLoadMethod]
        internal static void OnEditorLoaded() {
            RegisterFactory();
            RegisterPrefabChecker();
        }

        private static void RegisterFactory() {
            UIStyleInstanceDropper.RegisterUI<UIButton>(UIType.Button);
            UIStyleInstanceDropper.RegisterUI<UITabNavigator>(UIType.TabNavigator);
            UIStyleInstanceDropper.RegisterUI<UICheckBox>(UIType.Checkbox);
            UIStyleInstanceDropper.RegisterUI<UIRadioButton>(UIType.RadioButton);
            UIStyleInstanceDropper.RegisterUI<UICombobox>(UIType.Combobox);
            UIStyleInstanceDropper.RegisterUI<UISlider>(UIType.Slider);
            UIStyleInstanceDropper.RegisterUI<UIBar>(UIType.Bar);
            UIStyleInstanceDropper.RegisterUI<UIStar>(UIType.Star);
            UIStyleInstanceDropper.RegisterUI<UIResourceField>(UIType.ResourceField);
            UIStyleInstanceDropper.RegisterUI<UIDataPane>(UIType.DataPane);
            UIStyleInstanceDropper.RegisterUI<UITreePane>(UIType.TreePane);
            UIStyleInstanceDropper.RegisterUI<UIRichText>(UIType.RichText);
            UIStyleInstanceDropper.RegisterUI<UIPortrait>(UIType.Portrait);
            UIStyleInstanceDropper.RegisterUI<UITextArea>(UIType.TextArea);
            UIStyleInstanceDropper.RegisterUI<UITextInput>(UIType.TextInput);
            UIStyleInstanceDropper.RegisterUI<UIBackground>(UIType.Background);
            UIStyleInstanceDropper.RegisterUI<UILabel>(UIType.Label);
            UIMenuControls.panelType = typeof (Panel);
        }

        private static void RegisterPrefabChecker() {
            UIPrefabUtility.AddCheckType<Panel>(UIType.Panel, false);
            UIPrefabUtility.AddCheckType<PButton>(UIType.Button, false);
            UIPrefabUtility.AddCheckType<Tab>(UIType.TabNavigator, false);
            UIPrefabUtility.AddCheckType<PToggle>(UIType.Checkbox, false);
            UIPrefabUtility.AddCheckType<PToggle>(UIType.RadioButton, false);
            UIPrefabUtility.AddCheckType<PDropdown>(UIType.Combobox, false);
            UIPrefabUtility.AddCheckType<Slider>(UIType.Slider, false);
            UIPrefabUtility.AddCheckType<Bar>(UIType.Bar, false);
            UIPrefabUtility.AddCheckType<Star>(UIType.Star, false);
            UIPrefabUtility.AddCheckType<ResourceField>(UIType.ResourceField, false);
            UIPrefabUtility.AddCheckType<ScrollView>(UIType.DataPane, false);
            UIPrefabUtility.AddCheckType(UIType.DataPane, CheckScrollBar);
            UIPrefabUtility.AddCheckType<ScrollView>(UIType.TreePane, false);
            UIPrefabUtility.AddCheckType(UIType.TreePane, CheckScrollBar);
            UIPrefabUtility.AddCheckType(UIType.TextArea, CheckScrollBar);
            UIPrefabUtility.AddCheckType<ScrollView>(UIType.TextArea, false);
            UIPrefabUtility.AddCheckType<PText>(UIType.TextArea, true);
            UIPrefabUtility.AddCheckType<InputField>(UIType.TextInput, true);
            UIPrefabUtility.AddCheckType<PText>(UIType.TextInput, true);
        }

        private static void CheckScrollBar(GameObject o, List<string> errMsg) {
            Scrollbar[] bars = o.GetComponentsInChildren<Scrollbar>();
            bool h = false, v = false;
            foreach (var b in bars) {
                switch (b.direction) {
                    case Scrollbar.Direction.BottomToTop:
                    case Scrollbar.Direction.TopToBottom:
                        v = true;
                        break;
                    case Scrollbar.Direction.LeftToRight:
                    case Scrollbar.Direction.RightToLeft:
                        h = true;
                        break;
                }
            }
            if (!h) errMsg.Add("horizontal scrollbar");
            if (!v) errMsg.Add("vertical scrollbar");
        }
    }
}