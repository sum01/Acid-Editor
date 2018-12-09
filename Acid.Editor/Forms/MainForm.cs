﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Acid.UI.Docking;
using Acid.UI.Forms;
using Acid.UI.Win32;
using Acid.Editor.Forms.Dialogs;
using Acid.Editor.Forms.Docking;

namespace Acid.Editor.Forms
{
    public partial class MainForm : DarkForm
    {
        #region Field Region

        private readonly List<DarkDockContent> _toolWindows = new List<DarkDockContent>();

        private readonly DockAssets _dockAssets;
        private readonly DockScene _dockScene;
        private readonly DockHierarchy _dockHierarchy;
        private readonly DockInspector _dockInspector;
        private readonly DockConsole _dockConsole;

        #endregion

        #region Constructor Region

        public MainForm()
        {
            InitializeComponent();

            // Add the control scroll message filter to re-route all mousewheel events
            // to the control the user is currently hovering over with their cursor.
            Application.AddMessageFilter(new ControlScrollFilter());

            // Add the dock content drag message filter to handle moving dock content around.
            Application.AddMessageFilter(DockPanel.DockContentDragFilter);

            // Add the dock panel message filter to filter through for dock panel splitter
            // input before letting events pass through to the rest of the application.
            Application.AddMessageFilter(DockPanel.DockResizeFilter);

            // Hook in all the UI events manually for clarity.
            HookEvents();

            // Build the tool windows and add them to the dock panel
            _dockAssets = new DockAssets();
            _dockScene = new DockScene();
            _dockHierarchy = new DockHierarchy();
            _dockInspector = new DockInspector();
            _dockConsole = new DockConsole();

            // Add the tool windows to a list
            _toolWindows.Add(_dockAssets);
            _toolWindows.Add(_dockScene);
            _toolWindows.Add(_dockHierarchy);
            _toolWindows.Add(_dockInspector);
            _toolWindows.Add(_dockConsole);

            // Deserialize if a previous state is stored
            if (File.Exists("dockpanel.xml"))
            {
                DeserializeDockPanel("dockpanel.xml");
            }
            else
            {
                // Add the tool window list contents to the dock panel
                foreach (var toolWindow in _toolWindows)
                    DockPanel.AddContent(toolWindow);
            }

            // Check window menu items which are contained in the dock panel
            BuildWindowMenu();
        }

        #endregion

        #region Method Region

        private void HookEvents()
        {
            FormClosing += MainForm_FormClosing;

            DockPanel.ContentAdded += DockPanel_ContentAdded;
            DockPanel.ContentRemoved += DockPanel_ContentRemoved;

            mnuNewFile.Click += NewFile_Click;
            mnuClose.Click += Close_Click;

            mnuSettings.Click += SettingsClick;

            mnuAssets.Click += AssetsClick;
            mnuScene.Click += SceneClick;
            mnuHierarchy.Click += HierarchyClick;
            mnuInspector.Click += InspectorClick;
            mnuConsole.Click += Console_Click;

            mnuAbout.Click += About_Click;
        }

        private void ToggleToolWindow(DarkToolWindow toolWindow)
        {
            if (toolWindow.DockPanel == null)
                DockPanel.AddContent(toolWindow);
            else
                DockPanel.RemoveContent(toolWindow);
        }

        private void BuildWindowMenu()
        {
            mnuAssets.Checked = DockPanel.ContainsContent(_dockAssets);
            mnuScene.Checked = DockPanel.ContainsContent(_dockScene);
            mnuHierarchy.Checked = DockPanel.ContainsContent(_dockHierarchy);
            mnuInspector.Checked = DockPanel.ContainsContent(_dockInspector);
            mnuConsole.Checked = DockPanel.ContainsContent(_dockConsole);
        }

        #endregion

        #region Event Handler Region

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeDockPanel("dockpanel.xml");
        }

        private void DockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
                BuildWindowMenu();
        }

        private void DockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (_toolWindows.Contains(e.Content))
                BuildWindowMenu();
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            var newFile = new DockDocument("New document", Icons.document_16xLG);
            DockPanel.AddContent(newFile);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsClick(object sender, EventArgs e)
        {
            var test = new DialogSettings();
            test.ShowDialog();
        }

        private void AssetsClick(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockAssets);
        }

        private void SceneClick(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockScene);
        }

        private void HierarchyClick(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockHierarchy);
        }
        
        private void InspectorClick(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockInspector);
        }

        private void Console_Click(object sender, EventArgs e)
        {
            ToggleToolWindow(_dockConsole);
        }
        
        private void About_Click(object sender, EventArgs e)
        {
            var about = new DialogAbout();
            about.ShowDialog();
        }

        #endregion

        #region Serialization Region

        private void SerializeDockPanel(string path)
        {
	        DockPanelState state = DockPanel.GetDockPanelState();
	        var serializer = new XmlSerializer(typeof(DockPanelState));
	        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
		        serializer.Serialize(stream, state);
		}

        private void DeserializeDockPanel(string path)
        {
	        DockPanelState state;
	        var serializer = new XmlSerializer(typeof(DockPanelState));
	        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
		        state = (DockPanelState)serializer.Deserialize(stream);

			DockPanel.RestoreDockPanelState(state, GetContentBySerializationKey);
        }
         
        private DarkDockContent GetContentBySerializationKey(string key)
        {
            foreach (var window in _toolWindows)
            {
                if (window.SerializationKey == key)
                    return window;
            }

            return null;
        }

        #endregion
    }
}