using System.Windows.Forms;

namespace PiCrossManager.Generator
{
    public class MyPictureBox : PictureBox
    {
        public MyPictureBox()
            : base()
        {

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            base.OnPaint(pe);
        }
    }
}
