using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCrossManager.Game
{
    public class Puzzle
    {
        private int _nbLines;
        private int _nbCols;
        private string _name;
        private DateTime _creationDate;
        private string _createdBy;
        private string _createdByVersion;

        private Line[] _rows;
        private Line[] _cols;
    }
}
