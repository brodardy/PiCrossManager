using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiCrossManager.Generator
{
    public partial class GeneratorView : Form
    {
        private const int IMG_SIZE_MAX = 30;
        private const int IMG_SIZE_MIN = 5;
        private const int THRESHOLD_MAX = 255;
        private const int THRESHOLD_MIN = 0;

        private Image _origImage;
        private Image _grayscaleImage;
        private Image _finalImage;
        private int _threshold;

        public int Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        public Image FinalImage
        {
            get { return _finalImage; }
            set { _finalImage = value; }
        }

        public Image GrayscaleImage
        {
            get { return _grayscaleImage; }
            set { _grayscaleImage = value; }
        }

        public Image OrigImage
        {
            get { return _origImage; }
            private set { _origImage = value; }
        }

        public GeneratorView()
        {
            InitializeComponent();

            numHeight.Minimum = IMG_SIZE_MIN;
            numHeight.Maximum = IMG_SIZE_MAX;
            numWidth.Minimum = IMG_SIZE_MIN;
            numWidth.Maximum = IMG_SIZE_MAX;

            numThreshold.Minimum = THRESHOLD_MIN;
            numThreshold.Maximum = THRESHOLD_MAX;

            this.loadImage();
            this.loadGrayScale();
            this.loadFinal();

            this.pbxOriginal.Image = this.OrigImage;
            this.pbxGrayscale.Image = this.GrayscaleImage;
            this.pbxFinal.Image = this.FinalImage;
        }

        private void loadImage()
        {
            this.ofdOpenImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.ofdOpenImage.Filter = "JPG|*.jpg|PNG|*.png";
            this.ofdOpenImage.FilterIndex = 0;
            this.ofdOpenImage.RestoreDirectory = true;

            if (this.ofdOpenImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.OrigImage = new Bitmap(this.ofdOpenImage.FileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void loadGrayScale()
        {
            Bitmap img = new Bitmap(OrigImage);

            int height = img.Height;
            int width = img.Width;

            Rectangle rc = new Rectangle(0, 0, width, height);
            PixelFormat pf = img.PixelFormat;
            BitmapData bmpData = img.LockBits(rc, ImageLockMode.ReadWrite, pf);
            int stride = bmpData.Stride;
            IntPtr scan0 = bmpData.Scan0;

            int jumpImg = valueOfPixelFormat(pf);

            unsafe
            {
                byte* p = (byte*)(void*)scan0;
                int offset = stride - width * jumpImg;

                for (int i = 0; i < height; ++i)
                {

                    for (int j = 0; j < width; ++j)
                    {
                        byte rgb = (byte)((double)p[0] * .114 + (double)p[1] * .587 + (double)p[2] * .299);

                        p[0] = rgb;
                        p[1] = rgb;
                        p[2] = rgb;

                        p += jumpImg;
                    }

                    p += offset;
                }
            }

            img.UnlockBits(bmpData);

            GrayscaleImage = img;
        }

        private void loadFinal()
        {

        }

        private int valueOfPixelFormat(System.Drawing.Imaging.PixelFormat pf)
        {
            int result = 0;
            switch (pf)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    result = 3;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    result = 4;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                    result = 4;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    result = 4;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    result = 3;
                    break;
                default:
                    throw new ArgumentException("Unsupported pixel format");
            }
            return result;
        }
    }
}
