// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripcontrolhost?view=net-5.0

using System;
using System.Windows.Forms;

//Declare a class that inherits from ToolStripControlHost.
public class ToolStripMonthCalendar : System.Windows.Forms.ToolStripControlHost
{
    // Call the base constructor passing in a MonthCalendar instance.
    public ToolStripMonthCalendar() : base (new MonthCalendar()) { }

    public MonthCalendar MonthCalendarControl
    {
        get
        {
            return Control as MonthCalendar;
        }
    }

    // Expose the MonthCalendar.FirstDayOfWeek as a property.
    public Day FirstDayOfWeek
    {
        get
        {
            return MonthCalendarControl.FirstDayOfWeek;
        }
        set { MonthCalendarControl.FirstDayOfWeek = value; }
    }

    // Expose the AddBoldedDate method.
    public void AddBoldedDate(DateTime dateToBold)
    {
        MonthCalendarControl.AddBoldedDate(dateToBold);
    }

    // Subscribe and unsubscribe the control events you wish to expose.
    protected override void OnSubscribeControlEvents(Control? c)
    {
        // Call the base so the base events are connected.
        base.OnSubscribeControlEvents(c);

        // Add the event.
        if (c is MonthCalendar calendar)
            calendar.DateChanged += new DateRangeEventHandler(OnDateChanged);
    }

    protected override void OnUnsubscribeControlEvents(Control? c)
    {
        // Call the base method so the basic events are unsubscribed.
        base.OnUnsubscribeControlEvents(c);

        // Remove the event.
        if (c is MonthCalendar calendar)
            calendar.DateChanged -= new DateRangeEventHandler(OnDateChanged);
    }

    // Declare the DateChanged event.
    public event DateRangeEventHandler DateChanged;

    // Raise the DateChanged event.
    private void OnDateChanged(object? sender, DateRangeEventArgs e)
    {
        if (DateChanged != null)
        {
            DateChanged(this, e);
        }
    }
}