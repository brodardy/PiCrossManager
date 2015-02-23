/*
 * Yannick Brodard
 * 23/02/15
 * Position class
 */

namespace PiCrossManager.Game
{
    /// <summary>
    /// Defines the coordinates of an object (X and Y)
    /// </summary>
    public class Position
    {
        /* CLASS CONSTANTS */
        private const int DEFAULT_X = 0;
        private const int DEFAULT_Y = 0;

        /* CLASS VARIABLES */
        private int _x;
        private int _y;

        /* CLASS PROPRIETIES */
        /// <summary>
        /// Get : Gets the X value of the object
        /// Set (private) : Sets the X value of the object
        /// </summary>
        public int X
        {
            get { return _x; }
            private set { _x = value; }
        }

        /// <summary>
        /// Get : Gets the Y value of the object
        /// Set (private) : Sets the Y value of the object
        /// </summary>
        public int Y
        {
            get { return _y; }
            private set { _y = value; }
        }

        /* CLASS CONSTRUCTORS */
        /// <summary>
        /// Creates a position with the default coordinates
        /// </summary>
        public Position() : this(DEFAULT_X, DEFAULT_Y) { /* no code... */ }

        /// <summary>
        /// Creates a position with custom coordinates
        /// </summary>
        /// <param name="pPosX">X position</param>
        /// <param name="pPosY">Y position</param>
        public Position(int pPosX, int pPosY)
        {
            this.SetPosition(pPosX, pPosY);
        }

        /* CLASS METHODS */
        /// <summary>
        /// Sets the position (coordinates) of the object
        /// </summary>
        /// <param name="pPosX">X position</param>
        /// <param name="pPosY">Y position</param>
        public void SetPosition(int pPosX, int pPosY)
        {
            this.X = pPosX;
            this.Y = pPosY;
        }
    }
}
