namespace ScottPlot;

public class FormsPlotCulture : ScottPlot.FormsPlot
{
    private System.Globalization.CultureInfo _cultureUI = System.Globalization.CultureInfo.CurrentCulture;
    /// <summary>
    /// Culture used to show the right click context menu
    /// </summary>
    public System.Globalization.CultureInfo CultureUI
    {
        get { return _cultureUI; }
        set { _cultureUI = value; ContextMenuUILanguage(); }
    }

    private ContextMenuStrip customMenu = new();
    protected ContextMenuStrip ContextMenu
    {
        get { return customMenu; }
        set { customMenu = value; }
    }
    //private System.Windows.Forms.ToolStripMenuItem detachLegendMenuItem = new();

    private readonly System.Resources.ResourceManager StringsRM = new ("ScottPlot.FormsPlotCulture", typeof(FormsPlotCulture).Assembly);

    public FormsPlotCulture()
        : base()
    {
        InitilizeContextMenu();
        // Unsubscribe from the default right-click menu event
        this.RightClicked -= DefaultRightClickEvent;
        // Add a custom right-click action
        this.RightClicked += CustomRightClickEvent;
    }

    /// <summary>
    /// Sets the context menu items texts
    /// </summary>
    public virtual void ContextMenuUILanguage()
    {
        customMenu.Items["Copy"].Text = StringsRM.GetString("strMenuCopy", CultureUI) ?? "Copy image";
        customMenu.Items["Save"].Text = StringsRM.GetString("strMenuSave", CultureUI) ?? "Save image as...";
        customMenu.Items["Zoom"].Text = StringsRM.GetString("strMenuZoom", CultureUI) ?? "Zoom to fit data";
        customMenu.Items["Help"].Text = StringsRM.GetString("strMenuHelp", CultureUI) ?? "Help";
        customMenu.Items["Open"].Text = StringsRM.GetString("strMenuOpen", CultureUI) ?? "Open in new window";
        customMenu.Items["Detach"].Text = StringsRM.GetString("strMenuDetach", CultureUI) ?? "Detach legend";
    }

    /// <summary>
    /// This is used to create all the items comprising the context menu.
    /// Default items created: "Copy", "Save", | , "Zoom", | , "Help", | , "Open", "Detach"
    /// </summary>
    public virtual void InitilizeContextMenu()
    {
        int item;
        System.Windows.Forms.ToolStripMenuItem menuItem;

        item = customMenu.Items.Add(new ToolStripMenuItem("Copy image", null, new EventHandler(RightClickMenu_Copy_Click)));
        menuItem = (ToolStripMenuItem)customMenu.Items[item];
        menuItem.Name = "Copy";

        item = customMenu.Items.Add(new ToolStripMenuItem("Save image as...", null, new EventHandler(RightClickMenu_Help_Click)));
        menuItem = (ToolStripMenuItem)customMenu.Items[item];
        menuItem.Name = "Save";

        item = customMenu.Items.Add(new ToolStripSeparator());

        item = customMenu.Items.Add(new ToolStripMenuItem("Zoom to fit data", null, new EventHandler(RightClickMenu_AutoAxis_Click)));
        menuItem = (ToolStripMenuItem)customMenu.Items[item];
        menuItem.Name = "Zoom";

        item = customMenu.Items.Add(new ToolStripSeparator());

        item = customMenu.Items.Add(new ToolStripMenuItem("Help", null, new EventHandler(RightClickMenu_Help_Click)));
        menuItem = (ToolStripMenuItem)customMenu.Items[item];
        menuItem.Name = "Help";

        item = customMenu.Items.Add(new ToolStripSeparator());

        item = customMenu.Items.Add(new ToolStripMenuItem("Open in new window", null, new EventHandler(RightClickMenu_OpenInNewWindow_Click)));
        menuItem = (ToolStripMenuItem)customMenu.Items[item];
        menuItem.Name = "Open";

        item = customMenu.Items.Add(new ToolStripMenuItem("Detach legend", null, new EventHandler(RightClickMenu_DetachLegend_Click)));
        menuItem = (ToolStripMenuItem)customMenu.Items[item];
        menuItem.Name = "Detach";
        //detachLegendMenuItem = (ToolStripMenuItem)customMenu.Items[item];
        //detachLegendMenuItem.Name = "Detach";

    }

    /// <summary>
    /// Launch the default right-click menu.
    /// </summary>
    private void CustomRightClickEvent(object? sender, EventArgs e)
    {
        //detachLegendMenuItem.Visible = Plot.Legend(null).Count > 0;
        customMenu.Items["Detach"].Visible = Plot.Legend(null).Count > 0;
        customMenu.Show(System.Windows.Forms.Cursor.Position);
    }
    private void RightClickMenu_Copy_Click(object? sender, EventArgs e) => Clipboard.SetImage(Plot.Render());
    private void RightClickMenu_Help_Click(object? sender, EventArgs e) => new FormHelp().Show();
    private void RightClickMenu_AutoAxis_Click(object? sender, EventArgs e) { Plot.AxisAuto(); Refresh(); }
    private void RightClickMenu_OpenInNewWindow_Click(object? sender, EventArgs e) => new FormsPlotViewer(Plot).Show();
    private void RightClickMenu_DetachLegend_Click(object? sender, EventArgs e) => new FormsPlotLegendViewer(this);
    private void RightClickMenu_SaveImage_Click(object? sender, EventArgs e)
    {
        SaveFileDialog fileDialog = new()
        {
            FileName = StringsRM.GetString("strFileDlgFileName", CultureUI) ?? "Plot.png",
            Filter = StringsRM.GetString("strDlgFilter", CultureUI) ?? "PNG Files (*.png)|*.png;*.png" +
                     "|JPG Files (*.jpg, *.jpeg)|*.jpg;*.jpeg" +
                     "|BMP Files (*.bmp)|*.bmp;*.bmp" +
                     "|All files (*.*)|*.*"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK)
            Plot.SaveFig(fileDialog.FileName);
    }
}
