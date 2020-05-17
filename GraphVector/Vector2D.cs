using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GraphVector
{
    class Vector2D
    {
        private int _vertices;
        private int _width;
        private int _height;
        public Vector2D(int vertices, int width, int height)
        {
            this._vertices = vertices;
            this._width = width;
            this._height = height;
        }

        public List<Point> getRandomPoint()
        {
            List<Point> lstPoint = new List<Point>();
            Random ran = new Random();
            int distance = (int)(this._width / this._vertices);
            int x = 10, y = 0;
            for (int index = 1; index <= this._vertices; ++index)
            {
                x += distance;
                y = ran.Next(50, this._height);
                lstPoint.Add(new Point(x, y));
            }
            return lstPoint;
        }
    }
}
