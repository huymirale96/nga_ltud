using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphVector
{
    class GraphicsTools
    {
        private Size sizeCircle = new Size(20, 20); // size node
        private Bitmap bmpGraphics;
        private const int _RADIUS = 10; // ban kinh node 
        private const int _WIDTH = 20; // chieu rong node
        private const int _HEIGHT = 20; // chieu cao node



        public GraphicsTools(int Width, int Height)
        {
            this.bmpGraphics = new Bitmap(Width, Height);
        }

        public int RADIUS
        {
            get { return _RADIUS; }
        }
        public int HEIGHT
        {
            get { return _HEIGHT; }
        }

        public int WIDTH
        {
            get { return _WIDTH; }
        }

        // draw line
        // thu vien ve line 
        public void DrawLine(Graphics g, Pen pLine, Point ptStart, Point ptEnd)
        {
            g.DrawLine(pLine, ptStart, ptEnd);
        }


        // draw node
        // ham thuc thi ve diem
        private void DrawNode(Graphics g, Brush bFillNode, Pen pEllipse, Rectangle rect, Point pt, string name)
        {
            // ve hinh tron
            g.FillEllipse(bFillNode, rect);
            //ve duong tron
            g.DrawEllipse(pEllipse, rect);
            //font cho ten cua node
            Font stringFont = new Font("Arial", 9);

            // lay gia tri width va height cua ten node
            SizeF stringSize = g.MeasureString(name, stringFont);

            // ve ten o chinh giua node
            // WIDTH , HEIGHT
            g.DrawString(name, stringFont, Brushes.Black,
                        pt.X + (this.WIDTH - stringSize.Width) / 2,
                        pt.Y + (this.HEIGHT - stringSize.Height) / 2); 
        }

        // tao 1 bitmap do thi
        public Bitmap CreateGraphics(List<List<int>> adjacent, List<Point> lstPointVertex, Color colorPen)
        {
            Graphics g = Graphics.FromImage(this.bmpGraphics);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.White);
            Pen pLine;
            pLine = new Pen(colorPen, 3);

            Brush cbrush = new SolidBrush(Color.Wheat);
            Pen pCircle = new Pen(Color.Black, 2);

            // lam min bitmap

            Point ptStart, ptEnd;

            // ve nhung duong thang noi cac dinh lai
            for (int index = 0; index < adjacent.Count; ++index)
            {

                List<int> row = new List<int>(adjacent[index]);
                // diem dau cua duong thang
                ptStart = new Point(lstPointVertex[index].X,
                                     lstPointVertex[index].Y);

                foreach (int col in row)
                {
                    // diem cuoi cua duong thang
                    ptEnd = new Point(lstPointVertex[col].X,
                                    lstPointVertex[col].Y);
                    // tien hang ve duong thang noi cac noi ke lai voi nhau
                    this.DrawLine(g, pLine, ptStart, ptEnd);
                }
            }

            // ve node
            for (int index = 0; index < adjacent.Count; ++index)
            {
                // toa do ve duong tron 
                Point pt = new Point(lstPointVertex[index].X - this.RADIUS,
                                     lstPointVertex[index].Y - this.RADIUS);
                // tao rect cho node
                Rectangle rect = new Rectangle(pt, this.sizeCircle);
                this.DrawNode(g, cbrush, pCircle, rect, pt, (index + 1).ToString());
            }
            g.Dispose();
            pLine.Dispose();
            cbrush.Dispose();
            pCircle.Dispose();
            return this.bmpGraphics;
        }


        // draw path
        public Bitmap DrawPath(List<int> ReportPath, List<Point> lstPointVertices)
        {
            Graphics g = Graphics.FromImage(this.bmpGraphics);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen pLine = new Pen(Color.Red, 3); // pen for line mau do kieu -----
            Pen pClear = new Pen(Color.White, 3); // pen clear
            pLine.DashStyle = DashStyle.Dash; // dash type for pen line
            Brush cbrush = new SolidBrush(Color.Yellow); // fill ecllipse O---
            Brush cbrush_ = new SolidBrush(Color.Green); // fill ecllipse O---

            Pen pCircle = new Pen(Color.Black); // pen for circle

            // hightlight duong di
            // thuat toan ve duong thang tu dinh nay sang dinh kia
            for (int index = 0; index < ReportPath.Count - 1; ++index)
            {
                this.DrawLine(g, pClear, lstPointVertices[ReportPath[index]], lstPointVertices[ReportPath[index + 1]]); // clear line
                this.DrawLine(g, pLine, lstPointVertices[ReportPath[index]], lstPointVertices[ReportPath[index + 1]]);

                
            }
            // end ve dinh.

            // lam noi bat diem nam tren duong di
            // lam dinh noi len mau vang .. 
            for (int index = 0; index < ReportPath.Count; ++index)
            {
                Point ptCircle = new Point(lstPointVertices[ReportPath[index]].X - this.RADIUS,
                                    lstPointVertices[ReportPath[index]].Y - this.RADIUS);
                Rectangle rect = new Rectangle(ptCircle, this.sizeCircle);

                this.DrawNode(g, cbrush, pCircle, rect, ptCircle, (ReportPath[index] + 1).ToString());

            }
            //using (Font myFont = new Font("Arial", 14))
            //{
            //    g.DrawString("Hello .NET Guide!", myFont, Brushes.Green, new PointF(2, 2));
            //}
            // end lam noi mau vang 
            g.Dispose();
            pLine.Dispose();
            cbrush.Dispose();
            pClear.Dispose();
            pCircle.Dispose();

            return this.bmpGraphics;
        }


        public Bitmap Reset(List<List<int>> adjacent, List<Point> lstPointVertices)
        {
            Color clr = Color.FromArgb(100, 149, 237);
            return this.CreateGraphics(adjacent, lstPointVertices,clr);
        }

        public Bitmap DrawMSTPath(List<int> ReportPath, List<Point> lstPointVertices,Color color,Color penLineColor, Color brushColor, int[,] x)
        {

            foreach (object o in x)
            {
                Console.WriteLine("data_resxxx: " + o);
            }

            Console.WriteLine("vit ri 2 3: " + x[2,3]);
            Graphics g = Graphics.FromImage(this.bmpGraphics);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen pLine = new Pen(penLineColor, 3); // pen for line
            Pen pClear = new Pen(Color.White, 3); // pen clear
            pLine.DashStyle = DashStyle.Dash; // dash type for pen line
            Brush cbrush = new SolidBrush(color); // fill ecllipse
            Brush cbrush_ = new SolidBrush(Color.Green); // fill ecllipse O---

            Pen pCircle = new Pen(Color.Black); // pen for circle

            // hightlight duong di
            for (int index = 0; index < ReportPath.Count - 1; ++index)
            {
                Console.WriteLine("ReportPath[index + 1]] : " + lstPointVertices[ReportPath[index]] + "  " + ReportPath[index] +"  " + ReportPath[index+1]);
                this.DrawLine(g, pClear, lstPointVertices[ReportPath[index]], lstPointVertices[ReportPath[index + 1]]); // clear line
                this.DrawLine(g, pLine, lstPointVertices[ReportPath[index]], lstPointVertices[ReportPath[index + 1]]);

                Point ptCircle = new Point((lstPointVertices[ReportPath[index]].X + lstPointVertices[ReportPath[index + 1]].X) / 2,
                                    (lstPointVertices[ReportPath[index]].Y + lstPointVertices[ReportPath[index + 1]].Y) / 2);
                Rectangle rect = new Rectangle(ptCircle, this.sizeCircle);

                if (x[ReportPath[index], ReportPath[index + 1]] != 0)
                {
                    this.DrawNode(g, cbrush_, pCircle, rect, ptCircle, x[ReportPath[index], ReportPath[index + 1]].ToString());
                    Console.WriteLine(ptCircle.X + "  " + ptCircle.Y + "vit ri  x xy: " + x[ReportPath[index], ReportPath[index + 1]].ToString());
                }
            }

            // hightlight node nam tren duong di
            for (int index = 0; index < ReportPath.Count; ++index)
            {
                Point ptCircle = new Point(lstPointVertices[ReportPath[index]].X - this.RADIUS,
                                    lstPointVertices[ReportPath[index]].Y - this.RADIUS);
                Rectangle rect = new Rectangle(ptCircle, this.sizeCircle);

                this.DrawNode(g, cbrush, pCircle, rect, ptCircle, (ReportPath[index] + 1).ToString());

            }

            g.Dispose();
            pLine.Dispose();
            cbrush.Dispose();
            pClear.Dispose();
            pCircle.Dispose();

            return this.bmpGraphics;
        }// # End draw path
    } // #End class 

}
