using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace System.Windows.Forms
{
    class ToolStripStatusLabelEx : System.Windows.Forms.ToolStripStatusLabel
    {
        // Another example could be
        // https://www.codeproject.com/Articles/21419/Label-with-ProgressBar-in-a-StatusStrip

        private bool _checked;
        private System.Drawing.Brush _border;
        private System.Drawing.Brush _checkedBackground;

        /// <summary>
        /// Class constructor. Sets SteelBlue and LightSkyBlue as defaults colors
        /// </summary>
        public ToolStripStatusLabelEx()
        {
            _border = System.Drawing.Brushes.SteelBlue;
            _checkedBackground = System.Drawing.Brushes.LightSkyBlue;
            BackColor = System.Drawing.Color.Transparent;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="border"><cref>Brush</cref> for the checked border</param>
        /// <param name="checkedBackground">Brush for the checked background</param>
        public ToolStripStatusLabelEx(System.Drawing.Brush border, System.Drawing.Brush checkedBackground)
        {
            _border = border;
            _checkedBackground = checkedBackground;
        }

        /// <summary>
        /// Sets and gets the border color of the checked button
        /// </summary>
        public System.Drawing.Brush BorderColor
        {
            get { return _border; }
            set { _border = value; }
        }

        /// <summary>
        /// Sets and gets the background color of the checked button
        /// </summary>
        public System.Drawing.Brush CheckedColor
        {
            get { return _checkedBackground; }
            set { _checkedBackground = value; }
        }

        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                Invalidate();       // Force repainting the client area
            }
        }

        /// <summary>
		/// Paint Function
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
            // Only render if the state is checked
            if (_checked)
            {
                // fill the entire button with a color (will be used as a border)
                System.Drawing.Rectangle rectButtonFill = new System.Drawing.Rectangle(System.Drawing.Point.Empty, new System.Drawing.Size(Size.Width, Size.Height));
                e.Graphics.FillRectangle(_border, rectButtonFill);

                // fill the entire button offset by 1,1 and height/width subtracted by 2 used as the fill color
                int backgroundHeight = Size.Height - 2;
                int backgroundWidth = Size.Width - 6;
                System.Drawing.Rectangle rectBackground = new System.Drawing.Rectangle(3, 1, backgroundWidth, backgroundHeight);
                e.Graphics.FillRectangle(_checkedBackground, rectBackground);
            }

            base.OnPaint(e);    
		}

        protected override void OnClick(EventArgs e)
        {
            _checked = !_checked;
            base.OnClick(e);
        }

    }
}
