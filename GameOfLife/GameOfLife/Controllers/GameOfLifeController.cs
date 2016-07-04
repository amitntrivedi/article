using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameOfLife.Models; 
namespace GameOfLife.Controllers
{
    public class GameOfLifeController : ApiController
    {
        [HttpPost]
        public Board CreateNewBoard(Board CurrentBoard)
        {
            int rows=CurrentBoard.RowCount;

            int columns=CurrentBoard.ColumnCount; 
            Board NewBoard = new Board ( rows,  columns);
            NewBoard.RowCount = 10; 


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    NewBoard[i][j].IsAlive = getNewLifeValue(CurrentBoard, i, j, rows, columns);
                   
                }
            }
            return NewBoard; 

        }
        /// <summary>
        /// http://localhost:50013/api/gameoflife/getNewBoard?rows=10&columns=10
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetNewBoard( Board prevboard, int rows, int columns)
        {

            return "New board" + rows.ToString() +columns.ToString();

        }

       

        private Boolean getNewLifeValue(Board CurrentBoard, int row, int column, int rows, int columns)
        {

            // save current life value
            Boolean startLifeValue = CurrentBoard[row][column].IsAlive;
            Boolean newLifeValue = false;

            // check all surrounding squares
            int  startrow = (row > 0) ? (row - 1) : row;
            int endrow = (row < (rows - 1)) ? (row + 1) : row;
            int startcol = (column > 0) ? (column - 1) : column;
            int endcol = (column < (column - 1)) ? (column + 1) : column;
            int liveNeighbors = 0;

            for (int i = startrow; i <= endrow; i++)
            {
                for (int j = startcol; j <= endcol; j++)
                {
                    // skip the current space itself
                    if ((i == row) && (j == column))
                    {
                        // skip
                    }
                    else {
                        liveNeighbors += CurrentBoard[i][j].IsAlive ? 1:0;
                    }
                }
            }

            if (startLifeValue == false)
            {
                // if currently DEAD, only ONE check
                newLifeValue = false;
                if (liveNeighbors == 3) newLifeValue = true;
            }
            else {
                // WAS alive; only lives on if 2 or 3 live neighbors
                newLifeValue = false;
                if ((liveNeighbors == 2) || (liveNeighbors == 3)) newLifeValue = true;
            }

            return newLifeValue;
        }
    }
}
