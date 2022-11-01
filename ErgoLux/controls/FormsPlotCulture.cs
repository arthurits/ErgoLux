namespace ScottPlot;

public class FormsPlotCulture : ScottPlot.FormsPlot
{
    private System.Globalization.CultureInfo _cultureUI = System.Globalization.CultureInfo.CurrentCulture;

    /// <summary>
    /// Culture used to show the right-click context menu
    /// </summary>
    public System.Globalization.CultureInfo CultureUI
    {
        get { return this._cultureUI; }
        set { this._cultureUI = value; this.ContextMenuUILanguage(); }
    }

    /// <summary>
    /// This is the right-click context (shortcut) menu associated with the plot.
    /// </summary>
    protected ContextMenuStrip ContextMenu { get; set; } = new();

    private readonly System.Resources.ResourceManager StringsRM = new("ScottPlot.FormsPlotCulture", typeof(FormsPlotCulture).Assembly);

    public FormsPlotCulture()
        : base()
    {
        this.InitilizeContextMenu();
        // Unsubscribe from the default right-click menu event
        RightClicked -= this.DefaultRightClickEvent;
        // Add a custom right-click action
        RightClicked += this.CustomRightClickEvent;
    }

    /// <summary>
    /// Sets the context menu items texts
    /// </summary>
    protected virtual void ContextMenuUILanguage()
    {
        string strResource;
        foreach (ToolStripItem menuItem in this.ContextMenu.Items)
        {
            strResource = $"strMenu{menuItem.Name}";
            menuItem.Text = this.StringsRM.GetString(strResource, this.CultureUI);
        }

        //ContextMenu.Items["Copy"].Text = StringsRM.GetString("strMenuCopy", CultureUI) ?? "Copy image";
        //ContextMenu.Items["Save"].Text = StringsRM.GetString("strMenuSave", CultureUI) ?? "Save image as...";
        //ContextMenu.Items["Zoom"].Text = StringsRM.GetString("strMenuZoom", CultureUI) ?? "Zoom to fit data";
        //ContextMenu.Items["Help"].Text = StringsRM.GetString("strMenuHelp", CultureUI) ?? "Help";
        //ContextMenu.Items["Open"].Text = StringsRM.GetString("strMenuOpen", CultureUI) ?? "Open in new window";
        //ContextMenu.Items["Detach"].Text = StringsRM.GetString("strMenuDetach", CultureUI) ?? "Detach legend";
    }

    /// <summary>
    /// This is used to create all the items comprising the context menu.
    /// Default items created: "Copy", "Save", | , "Zoom", | , "Help", | , "Open", "Detach"
    /// </summary>
    protected virtual void InitilizeContextMenu()
    {
        int item;
        System.Windows.Forms.ToolStripMenuItem menuItem;

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Copy image", null, new EventHandler(this.RightClickMenu_Copy)));
        menuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        menuItem.Name = "Copy";

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Save image as...", null, new EventHandler(this.RightClickMenu_SaveImage)));
        menuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        menuItem.Name = "Save";

        item = this.ContextMenu.Items.Add(new ToolStripSeparator());

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Zoom to fit data", null, new EventHandler(this.RightClickMenu_AutoAxis)));
        menuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        menuItem.Name = "Zoom";

        item = this.ContextMenu.Items.Add(new ToolStripSeparator());

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Help", null, new EventHandler(this.RightClickMenu_Help)));
        menuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        menuItem.Name = "Help";

        item = this.ContextMenu.Items.Add(new ToolStripSeparator());

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Open in new window", null, new EventHandler(this.RightClickMenu_OpenInNewWindow)));
        menuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        menuItem.Name = "Open";

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Detach legend", null, new EventHandler(this.RightClickMenu_DetachLegend)));
        menuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        menuItem.Name = "Detach";
    }

    /// <summary>
    /// Launch the default right-click menu.
    /// </summary>
    protected virtual void CustomRightClickEvent(object? sender, EventArgs e)
    {
        this.ContextMenu.Items["Detach"].Visible = this.Plot.Legend(null).Count > 0;
        this.ContextMenu.Show(System.Windows.Forms.Cursor.Position);
    }
    protected virtual void RightClickMenu_Copy(object? sender, EventArgs e) => Clipboard.SetImage(this.Plot.Render());
    protected virtual void RightClickMenu_Help(object? sender, EventArgs e) => new FormHelp().Show();
    protected virtual void RightClickMenu_AutoAxis(object? sender, EventArgs e) { this.Plot.AxisAuto(); this.Refresh(); }
    protected virtual void RightClickMenu_OpenInNewWindow(object? sender, EventArgs e) => new FormsPlotViewer(this.Plot).Show();
    protected virtual void RightClickMenu_DetachLegend(object? sender, EventArgs e) => new FormsPlotLegendViewer(this);
    protected virtual void RightClickMenu_SaveImage(object? sender, EventArgs e)
    {
        SaveFileDialog fileDialog = new()
        {
            FileName = this.StringsRM.GetString("strFileDlgFileName", this.CultureUI) ?? "Plot.png",
            Filter = this.StringsRM.GetString("strFileDlgFilter", this.CultureUI) ?? "PNG Files (*.png)|*.png;*.png" +
                     "|JPG Files (*.jpg, *.jpeg)|*.jpg;*.jpeg" +
                     "|BMP Files (*.bmp)|*.bmp;*.bmp" +
                     "|All files (*.*)|*.*"
        };

        if (fileDialog.ShowDialog() == DialogResult.OK)
            this.Plot.SaveFig(fileDialog.FileName);
    }
}