using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

/*
 * RevealLabel : un label dont le texte apparaît de gauche à droite progressivement.
 * (à l'aide d'un timer).
 * source : http://www.dotnetfunda.com/articles/show/2811/create-animated-label-in-windows-forms
 * 
 */


namespace PlusUltra
{
    public class RevealLabel : Label
    {
        private System.Windows.Forms.Timer revealTimer = new System.Windows.Forms.Timer();

        private int paintWidth = 0;
        private int paintIncrement = 7;

        public RevealLabel()
        {
            this.DoubleBuffered = true;
            revealTimer.Interval = 200;
            revealTimer.Tick += new EventHandler(revealTimer_Tick);
        }

        void revealTimer_Tick(object sender, EventArgs e)
        {
            paintWidth += paintIncrement;

            if (paintWidth > this.ClientSize.Width)
            {
                revealTimer.Enabled = false;
            }
            else
            {
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (revealTimer.Enabled)
            {
                e.Graphics.Clear(this.BackColor);
                Rectangle r = new Rectangle(0, 0, paintWidth, this.ClientSize.Height);
                TextRenderer.DrawText(e.Graphics, this.Text, this.Font, r, Color.Black, Color.Empty, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            else
            {
                paintWidth = 0;
                revealTimer.Start();
            }
        }
    }
}
