using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PiCrossManager.Generator
{
    public partial class GeneratorView : Form
    {
        private const int IMG_SIZE_MAX = 50;
        private const int IMG_SIZE_MIN = 5;
        private const int THRESHOLD_MAX = 255;
        private const int THRESHOLD_MIN = 0;

        private Image _origImage;
        private Image _grayscaleImage;
        private Image _finalImage;
        private int _threshold;

        private bool _imgLoaded;

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

            this._imgLoaded = false;

            numHeight.Minimum = IMG_SIZE_MIN;
            numHeight.Maximum = IMG_SIZE_MAX;
            numHeight.Value = IMG_SIZE_MAX;
            numWidth.Minimum = IMG_SIZE_MIN;
            numWidth.Maximum = IMG_SIZE_MAX;
            numWidth.Value = IMG_SIZE_MAX;

            numThreshold.Minimum = THRESHOLD_MIN;
            numThreshold.Maximum = THRESHOLD_MAX;
            numThreshold.Value = 128;

            tbxSaveLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Untitled_Puzzle.xml";

            this.loadImage();
            if (this._imgLoaded)
            {
                this.loadGrayScale();

                this.pbxOriginal.Image = this.OrigImage;
                this.pbxGrayscale.Image = this.GrayscaleImage;
            }
            else {
                this.Close();
            }
        }

        public void UpdateFinal()
        {
            this.loadFinal();
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
                    this._imgLoaded = true;
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
            Bitmap img = new Bitmap(GrayscaleImage);
            int cWidth = Convert.ToInt32(this.numWidth.Value);
            int cHeight = Convert.ToInt32(this.numHeight.Value);

            int height = img.Height;
            int width = img.Width;

            if (height != cHeight || width != cWidth)
            {
                img = this.ResizeImage(GrayscaleImage, cWidth, cHeight);
                height = cHeight;
                width = cWidth;
            }

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
                        if (p[0] >= Convert.ToByte(this.numThreshold.Value))
                        {
                            p[0] = 255;
                            p[1] = 255;
                            p[2] = 255;
                        }
                        else
                        {
                            p[0] = 0;
                            p[1] = 0;
                            p[2] = 0;
                        }

                        if (jumpImg == 4)
                        {
                            if (p[3] > Byte.MinValue)
                            {
                                p[3] = 255;
                            }
                            else
                            {
                                p[0] = 255;
                                p[1] = 255;
                                p[2] = 255;
                                p[3] = 255;
                            }
                        }

                        p += jumpImg;
                    }

                    p += offset;
                }
            }

            img.UnlockBits(bmpData);

            FinalImage = img;

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

        /// <summary>
        /// Resize the image to the specified width and height.
        /// http://stackoverflow.com/questions/1922040/resize-an-image-c-sharp
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateFinal();
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(FinalImage);    // Prapare image...
            int height = img.Height;
            int width = img.Width;
            string datetime = DateTime.Now.ToString();
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            string fileLocation = tbxSaveLocation.Text;

            //writer configuration
            XmlTextWriter writer = null;
            writer = new XmlTextWriter(fileLocation, UTF8Encoding.Default);
            writer.Formatting = Formatting.Indented;

            /* HEADER */
            writer.WriteStartElement("PicrossPuzzle");
            writer.WriteStartElement("Informations");
            writer.WriteStartElement("Name");
            writer.WriteString(Path.GetFileNameWithoutExtension(fileLocation));
            writer.WriteEndElement(); // End Name
            writer.WriteStartElement("CreatedDate");
            writer.WriteAttributeString("date", datetime);
            writer.WriteString(datetime);
            writer.WriteEndElement(); // End CreatedDate
            writer.WriteStartElement("CreatedBy");
            writer.WriteAttributeString("name", Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName));
            writer.WriteAttributeString("version", version);
            writer.WriteEndElement(); // End CreatedBy
            writer.WriteStartElement("NbLines");
            writer.WriteAttributeString("dim", height.ToString());
            writer.WriteEndElement(); // End NbLines
            writer.WriteStartElement("NbCols");
            writer.WriteAttributeString("dim", width.ToString());
            writer.WriteEndElement(); // End NbCols
            writer.WriteEndElement(); // End Informations

            /* PUZZLE */
            writer.WriteStartElement("Puzzle");


            writer.WriteStartElement("Lines");
            // Lines
            for (int h = 0; h < height; h++)
            {
                writer.WriteStartElement("Line");
                StringBuilder sb = new StringBuilder();
                List<int> nbrs = new List<int>();
                for (int w = 0; w < width; w++)
                {
                    sb.Append((img.GetPixel(w, h).R == 255) ? "0" : "1");
                    nbrs.Add((img.GetPixel(w, h).R == 255) ? 0 : 1);
                }
                string flush = sb.ToString();

                writer.WriteStartElement("index");
                writer.WriteString(h.ToString());
                writer.WriteEndElement(); // End index
                writer.WriteStartElement("flush");
                writer.WriteString(flush);
                writer.WriteEndElement(); // End flush

                sb.Clear();
                writer.WriteStartElement("indices_string");
                int n = 0;
                for (int i = 0; i < nbrs.Count; i++)
                {
                    if (nbrs[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            sb.Append(n.ToString() + " ");
                        }
                        n = 0;
                    }
                }
                if (n > 0)
                {
                    sb.Append(n.ToString() + " ");
                    n = 0;
                }
                writer.WriteString(sb.ToString().Trim());
                writer.WriteEndElement(); // End indices_string

                sb.Clear();
                writer.WriteStartElement("indices_string_separator");
                n = 0;
                for (int i = 0; i < nbrs.Count; i++)
                {
                    if (nbrs[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            sb.Append(n.ToString() + ", ");
                        }
                        n = 0;
                    }
                }
                if (n > 0)
                {
                    sb.Append(n.ToString() + ", ");
                    n = 0;
                }
                string indices_string_separator = sb.ToString().Trim();
                if (indices_string_separator.Length > 0)
                {
                    writer.WriteString(indices_string_separator.Substring(0, indices_string_separator.Length - 1));
                }
                writer.WriteEndElement(); // End indices_string_separator

                int pos = 0;
                n = 0;
                for (int i = 0; i < nbrs.Count; i++)
                {
                    if (nbrs[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            writer.WriteStartElement("indice");
                            writer.WriteAttributeString("position", pos.ToString());
                            writer.WriteString(n.ToString());
                            writer.WriteEndElement(); // End indice
                            pos++;
                        }
                        n = 0;
                    }
                }
                if (n > 0)
                {
                    writer.WriteStartElement("indice");
                    writer.WriteAttributeString("position", pos.ToString());
                    writer.WriteString(n.ToString());
                    writer.WriteEndElement(); // End indice
                    pos++;
                    n = 0;
                }
                writer.WriteEndElement(); // End Line
            }
            writer.WriteEndElement(); // End Lines


            // Cols
            writer.WriteStartElement("Cols");
            for (int w = 0; w < width; w++)
            {
                writer.WriteStartElement("Col");
                StringBuilder sb = new StringBuilder();
                List<int> nbrs = new List<int>();
                for (int h = 0; h < height; h++)
                {
                    sb.Append((img.GetPixel(w, h).R == 255) ? "0" : "1");
                    nbrs.Add((img.GetPixel(w, h).R == 255) ? 0 : 1);
                }
                string flush = sb.ToString();

                writer.WriteStartElement("index");
                writer.WriteString(w.ToString());
                writer.WriteEndElement(); // End index
                writer.WriteStartElement("flush");
                writer.WriteString(flush);
                writer.WriteEndElement(); // End flush

                sb.Clear();
                writer.WriteStartElement("indices_string");
                int n = 0;
                for (int i = 0; i < nbrs.Count; i++)
                {
                    if (nbrs[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            sb.Append(n.ToString() + " ");
                        }
                        n = 0;
                    }
                }
                if (n > 0)
                {
                    sb.Append(n.ToString() + " ");
                    n = 0;
                }
                writer.WriteString(sb.ToString().Trim());
                writer.WriteEndElement(); // End indices_string

                sb.Clear();
                writer.WriteStartElement("indices_string_separator");
                n = 0;
                for (int i = 0; i < nbrs.Count; i++)
                {
                    if (nbrs[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            sb.Append(n.ToString() + ", ");
                        }
                        n = 0;
                    }
                }
                if (n > 0)
                {
                    sb.Append(n.ToString() + ", ");
                    n = 0;
                }
                string indices_string_separator = sb.ToString().Trim();
                if (indices_string_separator.Length > 0)
                {
                    writer.WriteString(indices_string_separator.Substring(0, indices_string_separator.Length - 1));
                }
                writer.WriteEndElement(); // End indices_string_separator

                int pos = 0;
                n = 0;
                for (int i = 0; i < nbrs.Count; i++)
                {
                    if (nbrs[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            writer.WriteStartElement("indice");
                            writer.WriteAttributeString("position", pos.ToString());
                            writer.WriteString(n.ToString());
                            writer.WriteEndElement(); // End indice
                            pos++;
                        }
                        n = 0;
                    }
                }
                if (n > 0)
                {
                    writer.WriteStartElement("indice");
                    writer.WriteAttributeString("position", pos.ToString());
                    writer.WriteString(n.ToString());
                    writer.WriteEndElement(); // End indice
                    pos++;
                    n = 0;
                }
                writer.WriteEndElement(); // End Col
            }
            writer.WriteEndElement(); // End Cols
            writer.WriteEndElement(); // End Puzzle
            writer.WriteEndElement(); // End PicrossPuzzle

            writer.Flush();
            writer.Close();
        }

        private void btnChangeSaveLocation_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgLocation = new SaveFileDialog();

            dlgLocation.Filter = "XML file|*.xml";
            dlgLocation.FilterIndex = 0;
            dlgLocation.RestoreDirectory = true;

            if (dlgLocation.ShowDialog() == DialogResult.OK)
            {
                tbxSaveLocation.Text = dlgLocation.FileName;
            }
        }
    }
}
