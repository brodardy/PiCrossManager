/*
 * Yannick Brodard
 * 23/02/15
 * Cell class
 */

namespace PiCrossManager
{
    /// <summary>
    /// Defines a cell of the PiCross game
    /// </summary>
    public class Cell
    {
        /* CLASS VARIABLES */
        private bool _isBlack;
        private Position _position;

        /* CLASS PROPRIETIES */
        /// <summary>
        /// State of the cell (Black or not)
        /// </summary>
        public bool IsBlack
        {
            get { return _isBlack; }
            private set { _isBlack = value; }
        }

        /// <summary>
        /// Position of the cell
        /// </summary>
        public Position Position
        {
            get { return _position; }
            private set { _position = value; }
        }

        /* CLASS CONSTRUCTORS */
        /// <summary>
        /// Creates a cell with custom proprieties
        /// </summary>
        /// <param name="pPosX">Position of the cell (X)</param>
        /// <param name="pPosY">Position of the cell (Y)</param>
        /// <param name="pIsBlack">State of the cell</param>
        public Cell(int pPosX, int pPosY, bool pIsBlack) : this(new Position(pPosX, pPosY), pIsBlack) { /* no code... */ }

        /// <summary>
        /// Creates a cell with custom proprieties
        /// </summary>
        /// <param name="pPosition">Position of the cell</param>
        /// <param name="pIsBlack">State of the cell</param>
        public Cell(Position pPosition, bool pIsBlack)
        {
            this.IsBlack = pIsBlack;
            this.Position = pPosition;
        }
    }
}
