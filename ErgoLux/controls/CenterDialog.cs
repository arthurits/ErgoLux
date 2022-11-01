using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>
/// Centers a dialog into its parent window
/// </summary>
/// https://stackoverflow.com/questions/2576156/winforms-how-can-i-make-messagebox-appear-centered-on-mainform
/// https://stackoverflow.com/questions/1732443/center-messagebox-in-parent-form
public class CenterWinDialog : IDisposable
{
    private int mTries = 0;
    private readonly Form mOwner;
    private System.Drawing.Rectangle clientRect;
    //private delegate System.Windows.Forms.Form SafeCallGetOwner(Form owner);

    public CenterWinDialog(Form owner)
    {
        this.mOwner = owner;
        //clientRect = Screen.FromControl(mOwner).WorkingArea;

        owner.Invoke((MethodInvoker)delegate
        {
            this.clientRect = Screen.FromControl(owner).WorkingArea;
        });

        if (owner.WindowState != FormWindowState.Minimized)
            owner.BeginInvoke(new MethodInvoker(this.FindDialog));
    }

    private void FindDialog()
    {
        // Enumerate windows to find the message box
        if (this.mTries < 0) return;
        EnumThreadWndProc callback = new(this.CheckWindow);
        if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
        {
            if (++this.mTries < 10) this.mOwner.BeginInvoke(new MethodInvoker(this.FindDialog));
        }
    }
    private bool CheckWindow(IntPtr hWnd, IntPtr lp)
    {
        // Checks if <hWnd> is a dialog
        System.Text.StringBuilder sb = new(260);
        int result = GetClassName(hWnd, sb, sb.Capacity);
        if (result == 0) return true;   // Error in GetClassName
        if (sb.ToString() != "#32770") return true;

        // Got it
        System.Drawing.Rectangle frmRect = new(this.mOwner.Location, this.mOwner.Size);
        GetWindowRect(hWnd, out RECT dlgRect);

        int x = frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2;
        int y = frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2;

        this.clientRect.Width -= (dlgRect.Right - dlgRect.Left);
        this.clientRect.Height -= (dlgRect.Bottom - dlgRect.Top);
        this.clientRect.X = x < this.clientRect.X ? this.clientRect.X : (x > this.clientRect.Right ? this.clientRect.Right : x);
        this.clientRect.Y = y < this.clientRect.Y ? this.clientRect.Y : (y > this.clientRect.Bottom ? this.clientRect.Bottom : y);

        MoveWindow(hWnd, this.clientRect.X, this.clientRect.Y, dlgRect.Right - dlgRect.Left, dlgRect.Bottom - dlgRect.Top, true);

        return false;
    }
    public void Dispose()
    {
        this.mTries = -1;
        GC.SuppressFinalize(this);
    }

    // P/Invoke declarations
    private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern int GetCurrentThreadId();
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder buffer, int buflen);
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
    private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
}