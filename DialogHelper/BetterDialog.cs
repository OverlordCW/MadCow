/*
 * BetterDialog.cs (also the designer.cs, and .resx)
 * 
 * This is a special, custom dialog box to replace the standard message boxes. The
 * benefits are that it has a large header, which looks much nicer on Vista. Is uses
 * special Vista styling. It is carefully tweaked to look good on XP and Vista. Another
 * big plus is that you can specify any icon. 32-pixel icons will look best. Invoke
 * with the static ShowDialog.
 * 
 * Original code by Samuel Allen. Copyright 2008.
 * Dot Net Perls, http://dotnetperls.com/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 * */

//todo: benchmark, enhance performance

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace DotNetPerls
{
    /// <summary>
    /// An improved dialog, with OS styling, an icon, a large header, and smaller explanation text.
    /// </summary>
    public partial class BetterDialog : Form
    {
        /// <summary>
        /// Create a special dialog in the style of Windows XP or Vista. A dialog has a custom icon, an optional large
        /// title in the form, body text, window text, and one or two custom-labeled buttons.
        /// </summary>
        /// <param name="titleString">This string will be displayed in the system window frame.</param>
        /// <param name="bigString">This is the first string to appear in the dialog. It will be most prominent.</param>
        /// <param name="smallString">This string appears either under the big string, or is null, which means it is
        /// not displayed at all.</param>
        /// <param name="leftButton">This is the left button, typically the "accept" button--label it with an
        /// action verb (or "OK").</param>
        /// <param name="rightButton">The right button--typically "Cancel", but could be "No".</param>
        /// <param name="iconSet">An image to be displayed on the left side of the dialog. Should be 32 x 32 pixels.</param>
        static public DialogResult ShowDialog(string title, string largeHeading, string smallExplanation,
            string leftButton, string rightButton, Image iconSet)
        {
            using (BetterDialog dialog = new BetterDialog(title, largeHeading, smallExplanation, leftButton,
                rightButton, iconSet))
            {
                DialogResult result = dialog.ShowDialog();
                return result;
            }
        }

        /// <summary>
        /// The private constructor. This is only called by the static method ShowDialog.
        /// </summary>
        private BetterDialog(string title, string largeHeading, string smallExplanation,
            string leftButton, string rightButton, Image iconSet)
        {
            this.Font = SystemFonts.MessageBoxFont;
            this.ForeColor = SystemColors.WindowText;

            InitializeComponent();

            // set our width and height to these values (redundant, but who cares?)
            this.Width = 350;
            this.Height = 150;

            using (Graphics graphics = this.CreateGraphics())
            {
                SizeF smallSize;
                SizeF bigSize;

                if (string.IsNullOrEmpty(smallExplanation) == false)
                {
                    if (SystemFonts.MessageBoxFont.FontFamily.Name == "Segoe UI")
                    {
                        // use the special, windows-vista font style if we are running vista (using Segoe UI).
                        label1.ForeColor = Color.FromArgb(0, 51, 153); // [ColorTranslator.FromHtml("#003399")]
                        label1.Font = new Font("Segoe UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point);

                        // bigger for vista/aero
                        this.Width += 50;

                        label1.Width += 50;
                        label2.Width += 50;

                        smallSize = graphics.MeasureString(smallExplanation, this.Font, this.label2.Width);
                        bigSize = graphics.MeasureString(largeHeading, label1.Font, this.label1.Width);

                        this.Height = (int)smallSize.Height + 158;

                        // add in a little margin on the top as well
                        pictureBox1.Margin = new Padding(pictureBox1.Margin.Left, pictureBox1.Margin.Top + 6,
                            pictureBox1.Margin.Right, pictureBox1.Margin.Bottom);
                    }
                    else
                    {
                        // might want to special case the old, MS Sans Serif font.
                        // use the regular font but bold it for XP, etc.
                        label1.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name, 8.0f,
                            FontStyle.Bold, GraphicsUnit.Point);

                        smallSize = graphics.MeasureString(smallExplanation, this.Font, this.label2.Width);
                        bigSize = graphics.MeasureString(largeHeading, label1.Font, this.label1.Width);

                        // set our height according to the small string
                        this.Height = (int)smallSize.Height + 166; // went from 164 to 168 to improve bottom space on XP.
                        // removed 2 pixels for XP
                    }

                    // modify our width (clean this up a bit) based on the longest text's width
                    double bigger = (smallSize.Width > bigSize.Width) ? smallSize.Width : bigSize.Width;
                    this.Width = (int)bigger + 100;
                }
                else
                {
                    // do layout for a single-message dialog
                    // reduce size for plain dialog
                    this.Width -= 20;

                    // this is our text's dimensions
                    bigSize = graphics.MeasureString(largeHeading, label1.Font, label1.Width);

                    // set our height
                    label1.Height = (int)bigSize.Height;
                    tableLayoutPanel1.Height = (int)bigSize.Height + 58;

                    // hide the second table, which is used for the small text, but we don't have any.
                    tableLayoutPanel2.Visible = false;
                    tableLayoutPanel2.Height = 0;

                    this.Height = tableLayoutPanel1.Height + 71;

                    // remove the top margin from the label (everything is vertically centered)
                    label1.Margin = new Padding(label1.Margin.Left, 0, label1.Margin.Right, label1.Margin.Bottom);

                    // remove top margin of picture; everything is just centered.
                    pictureBox1.Margin = new Padding(pictureBox1.Margin.Left, 0, pictureBox1.Margin.Right, pictureBox1.Margin.Bottom);

                    // modify our width (clean this up a bit) based on text's physical width
                    this.Width = (int)bigSize.Width + 100;
                }
            }

            // expand to be at least 260 pixels
            if (this.Width < 260)
            {
                this.Width = 260;
            }

            // set our text
            this.Text = title;
            label1.Text = largeHeading;
            label2.Text = string.IsNullOrEmpty(smallExplanation) ? string.Empty : smallExplanation;

            // setup our left button (optional)
            if (string.IsNullOrEmpty(leftButton) == false)
            {
                // if we have the button, we are fine
                this.buttonLeft.Text = leftButton;
            }
            else
            {
                // move settings to right button if we don't have the left button
                this.AcceptButton = buttonRight;
                this.buttonLeft.Visible = false;
            }

            // this button must always be present
            this.buttonRight.Text = rightButton;
            pictureBox1.Image = iconSet;
        }
    }
}
