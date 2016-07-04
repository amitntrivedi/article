using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfLife.Models
{
    public class GameOfLife
    {
        
    }

    public class Board
    {
        public int RowCount { get; set;  }
        public int ColumnCount { get; set;  }
        public List<Row> _Rows; 
        public List<Row> Rows
        { set
            {
                _Rows=value ;
            }
            get {
                return _Rows; 
            }
        }
        public Board(int rows, int columns)
        {
            Setup(rows, columns);
        }

        public Board()
        {
        }
        /// <summary>
        /// Indexer to get grid row by using index for ease of use
        /// </summary>
        /// <param name="x"></param>
        /// <returns>returns row</returns>
        public Row this[int x]
        {
            get { if (Rows.Count <= x) throw new ArgumentOutOfRangeException("Argument out of bound"); return Rows[x]; }
            set { if (Rows.Count <= x) throw new ArgumentOutOfRangeException("Argument out of bound"); Rows[x] = value; }
        }

        private void Setup(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0) throw new ArgumentOutOfRangeException("Row and Column size must be greater than zero");
            Rows = new List<Row>();
            for (int i = 0; i < rows; i++)
            {
                Row row = new Row();
                for (int j = 0; j < columns; j++)
                {
                    Cell cell = new Cell(false);
                    row.AddCell(cell);
                }
                Rows.Add(row);
            }
            ColumnCount = columns;
            RowCount = rows; 
        }
    }

    public class Row
    {
        public List<Cell> Cells { get; set;  }

        public Row()
        {
            Cells = new List<Cell>(); 

        }

        public void AddCell(Cell cell)
        {
            Cells.Add(cell);
        }

        public void InsertCell(int index, Cell cell, int ColumnCount)
        {
            if (index < 0 || index >= ColumnCount) throw new ArgumentOutOfRangeException("Invalid Index value: must be greater than zero and less than Column count");
            Cells.Insert(index, cell);
        }

        /// <summary>
        /// indexer to get cell using index
        /// </summary>
        /// <param name="y"></param>
        /// <returns>returns cell</returns>
        public Cell this[int y]
        {
            get { if (y >= Cells.Count) throw new ArgumentOutOfRangeException("Argument out of bound"); return Cells[y]; }
            set { if (y >= Cells.Count) throw new ArgumentOutOfRangeException("Argument out of bound"); Cells[y] = value; }
        }
    }


    public class Cell
    {
        public Boolean IsAlive { get; set;  }

        public Cell(Boolean isAlive)
        {
            this.IsAlive = isAlive; 
        }
    }
}