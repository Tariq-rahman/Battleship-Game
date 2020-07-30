using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Battleships.Models;
using Battleships.Helpers;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Battleships.Controllers
{
    public class GameController : Controller
    {        
        private Board gameBoard;        
        public GameController()
        {            
            gameBoard = new Board();
            AddShip(ShipSizes.HMS_ELIZABETH, CalculateRandomPoints(ShipSizes.HMS_ELIZABETH));
            AddShip(ShipSizes.HMS_DEFENDER, CalculateRandomPoints(ShipSizes.HMS_DEFENDER));         
            AddShip(ShipSizes.HMS_DUNCAN,  CalculateRandomPoints(ShipSizes.HMS_DEFENDER));
            //AddShip(2, CalculateRandomPoints(2));

        }

        public IActionResult Index()
        {            
            return View(gameBoard);
        }

        public void AddShip(int size, List<Point> points)
        {            
            foreach(Point p in points)
            {
                gameBoard.SetCellValue(p.X, p.Y, 1);
            }            
        }

        public List<Point> CalculateRandomPoints(int shipSize)
        {
            Random random = new Random();
            int x = 0;
            int y = 0;
            int orientation = 0;
            do
            {
                do
                {
                    //select random cell in grid
                    x = random.Next(0, 10);
                    y = random.Next(0, 10);
                    //check if cell is empty
                } while (!IsEmpty(x, y));

                //select random orientation, 1 = up, 2 = right, 3 = down, 4 = left
                orientation = random.Next(1, 5);

            } while (!isOrientationValid(x, y, orientation, shipSize));

            return GeneratePoints(x, y, orientation, shipSize);
        }

        public List<Point> GeneratePoints(int x, int y, int orientation, int shipSize)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < shipSize; i++)
            {                
                Point point = new Point(x, y);
                points.Add(point);

                if (orientation == Orientation.NORTH)
                {
                    y--;
                }
                else if (orientation == Orientation.EAST)
                {
                    x++;
                }
                else if (orientation == Orientation.SOUTH)
                {
                    y++;
                }
                else if (orientation == Orientation.WEST)
                {
                    x--;
                }
            }
            return points;
        }

        public bool IsEmpty(int x, int y)
        {
            int value = gameBoard.GetBoard()[x][y];
            if(value != 0)
            {
                return false;
            }
            return true;
        }

        public bool IsValid(int x, int y)
        {
            //check for out of bounds exceptions            
            if(x > Board.MAX_SIZE || x < Board.MIN_SIZE || y > Board.MAX_SIZE || y < Board.MIN_SIZE)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool isOrientationValid( int xOrigin, int yOrigin, int orientation, int size)
        {
            if(orientation == Orientation.NORTH)
            {
                int yEnd = yOrigin - (size - 1);
                //check if end section of ship is valid/empty
                if(!IsValid(xOrigin, yEnd) || !IsEmpty(xOrigin, yEnd))
                {
                    return false;
                }                
                //check if middle section of ship is empty
                while(yEnd != yOrigin)
                {
                    yEnd = yEnd + 1;
                    if (!IsEmpty(xOrigin, yEnd))
                    {
                        return false;
                    }
                }
            }
            else if (orientation == Orientation.EAST)
            {
                int xEnd = xOrigin + (size - 1);
                //check if end section of ship is valid/empty
                if(!IsValid(xEnd, yOrigin) || !IsEmpty(xEnd, yOrigin))
                {
                    return false;
                }
                //check if middle siction of ship is empty
                while(xEnd != xOrigin)
                {
                    xEnd = xEnd - 1;
                    if(!IsEmpty(xEnd, yOrigin))
                    {
                        return false;
                    }
                }

            }
            else if (orientation == Orientation.SOUTH)
            {
                int yEnd = yOrigin + (size - 1);
                //check if end section of ship is valid/empty
                if(!IsValid(xOrigin, yEnd) || !IsEmpty(xOrigin, yEnd))
                {
                    return false;
                }
                //check if middle section of ship is empty
                while (yEnd != yOrigin)
                {
                    yEnd = yEnd -1;
                    if (!IsEmpty(xOrigin, yEnd))
                    {
                        return false;
                    }
                }
            }
            else if (orientation == Orientation.WEST)
            {
                int xEnd = xOrigin - (size - 1);
                //check if end section of ship is valid/empty
                if (!IsValid(xEnd, yOrigin) || !IsEmpty(xEnd, yOrigin))
                {
                    return false;
                }
                //check if middle siction of ship is empty
                while (xEnd != xOrigin)
                {
                    xEnd = xEnd + 1;
                    if (!IsEmpty(xEnd, yOrigin))
                    {
                        return false;
                    }
                }
            }                      
            return true;                  
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
