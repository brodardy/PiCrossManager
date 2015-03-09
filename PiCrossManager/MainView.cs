using PiCrossManager.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PiCrossManager
{
    public partial class MainView : Form
    {
        private struct PuzzleDraw
        {
            public float CellSize;
            public int OffsetTop;
            public int OffsetLeft;
            public bool Loaded;
            public int TotalWidthWithIndexes;
            public int TotalHeightWithIndexes;
            public string[] TopIndexes;
            public string[] LeftIndexes;
        }

        private PuzzleDraw _puzzleDraw;
        private Puzzle _puzzle;

        public MainView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this._puzzleDraw = new PuzzleDraw();
            this._puzzleDraw.Loaded = false;
            this._puzzleDraw.CellSize = 0;
            this._puzzleDraw.OffsetTop = 0;
            this._puzzleDraw.OffsetLeft = 0;
            this._puzzleDraw.TotalWidthWithIndexes = 0;
            this._puzzleDraw.TotalHeightWithIndexes = 0;
        }

        private void piCrossGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new PiCrossManager.Generator.GeneratorView().Show();
            }
            catch (ObjectDisposedException) { /* no code... */ }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool puzzleLoaded = false;

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dlg.Filter = "PicrossPuzzle XML|*.xml";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(dlg.FileName);
                puzzleLoaded = true;

                this._puzzle = new Puzzle();

                int nbLines = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/PicrossPuzzle/Informations/NbLines").Attributes["dim"].InnerText);
                int nbCols = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/PicrossPuzzle/Informations/NbCols").Attributes["dim"].InnerText);

                this._puzzle.Width = nbCols;
                this._puzzle.Height = nbLines;

                _puzzle.Cells = new Cell[nbCols, nbLines];

                _puzzleDraw.LeftIndexes = new string[nbLines];
                _puzzleDraw.TopIndexes = new string[nbCols];

                int y = 0;
                foreach (XmlNode node in doc.DocumentElement.SelectSingleNode("/PicrossPuzzle/Puzzle/Lines").ChildNodes)
                {
                    string flush = node.ChildNodes[1].InnerText;
                    _puzzleDraw.LeftIndexes[y] = node.ChildNodes[2].InnerText;

                    for (int x = 0; x < nbCols; x++)
                    {
                        _puzzle.Cells[x, y] = new Cell(new Position(x, y), (flush[x] == '0') ? false : true);
                    }
                    y++;
                }

                int ix = 0;
                foreach (XmlNode node in doc.DocumentElement.SelectSingleNode("/PicrossPuzzle/Puzzle/Cols").ChildNodes)
                {
                    _puzzleDraw.TopIndexes[ix] = node.ChildNodes[2].InnerText;
                    ix++;
                }
            }
            else
            {
                puzzleLoaded = false;
            }

            if (puzzleLoaded)
            {
                this._puzzleDraw.TotalHeightWithIndexes = this._puzzle.Height + this._puzzleDraw.TopIndexes.Length;
                this._puzzleDraw.TotalWidthWithIndexes = this._puzzle.Width + this._puzzleDraw.LeftIndexes.Length;

                this._puzzleDraw.Loaded = true;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this._puzzleDraw.Loaded)
            {
                Graphics g = e.Graphics;

                this._puzzleDraw.OffsetTop = 30;

                Pen myPen = new Pen(Color.Black);
                SolidBrush bActive = new SolidBrush(Color.Black);
                SolidBrush bInactive = new SolidBrush(Color.White);
                Font myFont = new Font(this.Font, FontStyle.Regular);

                int biggestIndexesLeft = 0;
                int biggestIndexesTop = 0;

                for (int i = 0; i < this._puzzleDraw.LeftIndexes.Length; i++)
                {
                    string[] numbers = this._puzzleDraw.LeftIndexes[i].Split(' ');
                    biggestIndexesLeft = (numbers.Length > biggestIndexesLeft) ? numbers.Length : biggestIndexesLeft;
                }

                for (int i = 0; i < this._puzzleDraw.TopIndexes.Length; i++)
                {
                    string[] numbers = this._puzzleDraw.TopIndexes[i].Split(' ');
                    biggestIndexesTop = (numbers.Length > biggestIndexesTop) ? numbers.Length : biggestIndexesTop;
                }

                int totw = this._puzzle.Width + biggestIndexesLeft;
                int toth = this._puzzle.Height + biggestIndexesTop;

                this._puzzleDraw.CellSize = (Math.Min(this.Height, this.Width) - this._puzzleDraw.OffsetTop * 2.5f) / Math.Max(totw, toth);

                //Console.WriteLine("biggestIndexesLeft {0}", biggestIndexesLeft);
                //Console.WriteLine("biggestIndexesTop {0}", biggestIndexesTop);

                string[] numbersY = this._puzzleDraw.TopIndexes;
                int[] counterY = new int[this._puzzleDraw.TopIndexes.Length];
                for (int i = 0; i < this._puzzleDraw.TopIndexes.Length; i++)
                {
                    counterY[i] = 0;
                }

                for (int y = 0; y < this._puzzle.Height + biggestIndexesTop; y++)
                {
                    List<string> numbersX = new List<string>();
                    int counterX = 0;
                    int originalCountX = 0;

                    if (y > biggestIndexesTop - 1)
                    {
                        numbersX = this._puzzleDraw.LeftIndexes[y - biggestIndexesTop].Split(' ').ToList();
                        counterX = biggestIndexesLeft;
                        originalCountX = numbersX.Count;
                    }

                    for (int x = 0; x < this._puzzle.Width + biggestIndexesLeft; x++)
                    {
                        if ((x > biggestIndexesLeft - 1) && (y > biggestIndexesTop - 1))
                        {
                            g.FillRectangle(bInactive, x * this._puzzleDraw.CellSize,
                                                       y * this._puzzleDraw.CellSize + this._puzzleDraw.OffsetTop,
                                                       this._puzzleDraw.CellSize,
                                                       this._puzzleDraw.CellSize);

                            g.DrawRectangle(myPen, x * this._puzzleDraw.CellSize,
                                                   y * this._puzzleDraw.CellSize + this._puzzleDraw.OffsetTop,
                                                   this._puzzleDraw.CellSize,
                                                   this._puzzleDraw.CellSize);

                            if (this._puzzle.Cells[x - biggestIndexesLeft, y - biggestIndexesTop].IsBlack)
                            {
                                g.FillRectangle(bActive, (x * this._puzzleDraw.CellSize) + (this._puzzleDraw.CellSize * 0.1f),
                                                         (y * this._puzzleDraw.CellSize + this._puzzleDraw.OffsetTop) + (this._puzzleDraw.CellSize * 0.1f),
                                                         this._puzzleDraw.CellSize * 0.8f,
                                                         this._puzzleDraw.CellSize * 0.8f);
                            }
                        }
                        else
                        {
                            if (x < biggestIndexesLeft || y < biggestIndexesTop)
                            {
                                if (numbersX.Count > 0)
                                {
                                    if (originalCountX < biggestIndexesLeft)
                                    {
                                        if (numbersX.Count == counterX)
                                        {
                                            drawLeftIndex(g, myFont, bActive, x * this._puzzleDraw.CellSize, y * this._puzzleDraw.CellSize + this._puzzleDraw.OffsetTop, numbersX);
                                        }
                                        counterX--;
                                    }
                                    else
                                    {
                                        drawLeftIndex(g, myFont, bActive, x * this._puzzleDraw.CellSize, y * this._puzzleDraw.CellSize + this._puzzleDraw.OffsetTop, numbersX);
                                    }
                                }

                                if (y < biggestIndexesTop && x >= biggestIndexesLeft) // If it on the top indexes drawing zone
                                {
                                    int number = counterY[x - biggestIndexesLeft]; // save the counter of the specified indexes string
                                    string[] aText = numbersY[x - biggestIndexesLeft].Split(' '); // Gets all the different numbers of indexes string
                                    //Console.WriteLine("({0} - {1}) == (({2} - {3}) - {4})", y, biggestIndexesX, x, biggestIndexesY, counterY[x - biggestIndexesY]);
                                    if (number < aText.Length) // Only draw if there's a index
                                    {
                                        if (biggestIndexesTop - y <= aText.Length) // Compares the numbers of indexes to the Y position to indent the indexes properly
                                        {
                                            string text = aText[number]; // gets the correct index in the indexes string with the counter of this one
                                            g.DrawString(text, myFont, bActive, x * this._puzzleDraw.CellSize, y * this._puzzleDraw.CellSize + this._puzzleDraw.OffsetTop); // Draws the index
                                            counterY[x - biggestIndexesLeft] = counterY[x - biggestIndexesLeft] + 1; // increment the counter of the specified indexes string
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void drawLeftIndex(Graphics g, Font font, SolidBrush brush, float posX, float posY, List<string> numbers)
        {
            g.DrawString(numbers[0], font, brush, posX, posY);
            numbers.RemoveAt(0);
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            // TO DO

        }

        private void MainView_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
