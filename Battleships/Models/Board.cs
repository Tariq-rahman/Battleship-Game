using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleships.Models
{   
    public class Board
    {
        private bool IsShipSet;
        private int[][] board;
        public static int MAX_SIZE = 9;
        public static int MIN_SIZE = 0;
        //creates an empty board with no ships
        public Board()
        {
            board = new int[][]{ 
                new int[10],
                new int[10],
                new int[10],
                new int[10],
                new int[10],
                new int[10],
                new int[10],
                new int[10],
                new int[10],
                new int[10],
            };
        }

        //sets random ships onto the board
        public void SetShips()
        {
            //add the ships
            //set isShipSet to true
            IsShipSet = true;
        }      
        
        public int[][] GetBoard()
        {
            return board;
        }

        public void SetBoard(int[][] b)
        {
            board = b;
        }

        public void SetCellValue(int x, int y, int value) 
        {
            board[x][y] = value;
        }
        public int GetCellValue(int x,int y)
        {
            return board[x][y];
        }
        public bool GetIsShipSet()
        {
            return IsShipSet;
        }
    }
}
