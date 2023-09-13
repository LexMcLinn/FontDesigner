using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFCreator
{
    [Serializable]
    public class Contour
    {
      private List<GPoint> points;

        public Contour()
        {   this.points = new List<GPoint>();    }

        public void AddOnPoint(int x, int y)
        {
            Point p = new Point(x, y);
            GPoint gp = new GPoint (p, true);

            points.Add(gp);
        }

        public void AddOffPoint(int x, int y)
        {
            Point p = new Point(x, y);
            GPoint gp = new GPoint(p, false);

            points.Add(gp);
        }

        public List<GPoint> GetPoints
        {
            get { return points; }
        }

        public int NumOfPoints
        {     get { return points.Count; }   }

        public void Clear()
        {
            this.points.Clear();
        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Black);
            Pen pen = new Pen(b);

            int n = 0;
            
            while(n < points.Count)
            {
                if(n+1 < points.Count)
                {
                    if (points[n + 1].OnCurve)
                    {
                        g.DrawLine(pen, points[n].X, points[n].Y, points[n + 1].X, points[n + 1].Y);

                    }
                    else
                    {
                        if (n + 2 < points.Count)
                        {
                            if (points[n + 2].OnCurve)
                            {
                                int x1 = points[n].X + (2 / 3) * (points[n + 1].X - points[n].X);
                                int y1 = points[n].Y + (2 / 3) * (points[n + 1].Y - points[n].Y);
                                int x2 = points[n + 1].X + (1 / 3) * (points[n + 2].X - points[n + 1].X);
                                int y2 = points[n + 1].Y + (1 / 3) * (points[n + 2].Y - points[n + 1].Y);

                                g.DrawBezier(pen, points[n].X, points[n].Y, x1, y1, x2, y2, points[n + 2].X, points[n + 2].Y);
                                n++;
                            }

                            else
                            {
                                if (n + 3 < points.Count)
                                {
                                    int xm = (points[n + 1].X + points[n + 2].X) / 2;
                                    int ym = (points[n + 1].Y + points[n + 2].Y) / 2;

                                    int x1 = points[n].X + (2 / 3) * (points[n + 1].X - points[n].X);
                                    int y1 = points[n].Y + (2 / 3) * (points[n + 1].Y - points[n].Y);
                                    int x2 = points[n + 1].X + (1 / 3) * (xm - points[n + 1].X);
                                    int y2 = points[n + 1].Y + (1 / 3) * (ym - points[n + 1].Y);

                                    int x3 = xm + (2 / 3) * (points[n + 2].X - points[n].X);
                                    int y3 = ym + (2 / 3) * (points[n + 2].Y - points[n].Y);
                                    int x4 = points[n + 2].X + (1 / 3) * (points[n + 3].X - points[n + 2].X);
                                    int y4 = points[n + 2].Y + (1 / 3) * (points[n + 3].Y - points[n + 2].Y);


                                    g.DrawBezier(pen, points[n].X, points[n].Y, x1, y1, x2, y2, xm, ym);
                                    g.DrawBezier(pen, xm, ym, x3, y3, x4, y4, points[n + 3].X, points[n + 3].Y);
                                    n += 2;
                                }
                            }
                        }
                           
                    }

                    
                }
                n++;
            }

            
            if (points.Count > 1)
            {
                if (points[points.Count - 1].OnCurve) g.DrawLine(pen, points[points.Count - 1].X, points[points.Count - 1].Y,
                   points[0].X, points[0].Y);
                else
                {
                    if (points.Count > 2 && points[points.Count - 2].OnCurve)
                    {
                        int last = points.Count - 1;
                        int x1 = points[last - 1].X + (2 / 3) * (points[last].X - points[last - 1].X);
                        int y1 = points[last - 1].Y + (2 / 3) * (points[last].Y - points[last - 1].Y);
                        int x2 = points[last].X + (1 / 3) * (points[0].X - points[last].X);
                        int y2 = points[last].Y + (1 / 3) * (points[0].Y - points[last].Y);

                        g.DrawBezier(pen, points[last - 1].X, points[last - 1].Y,
                                            x1, y1,
                                            x2, y2,
                                            points[0].X, points[0].Y);
                    }

                    else if (points.Count > 3 && points[points.Count - 3].OnCurve)
                    {
                        int x0 = points[points.Count - 3].X;
                        int y0 = points[points.Count - 3].Y;

                        int xm = (points[points.Count-2].X + points[points.Count-1].X) / 2;
                        int ym = (points[points.Count - 2].Y + points[points.Count - 1].Y) / 2;

                        int x1 = points[points.Count - 3].X + (2 / 3) * (points[points.Count - 2].X - points[points.Count - 3].X);
                        int y1 = points[points.Count - 3].Y + (2 / 3) * (points[points.Count - 2].Y - points[points.Count - 3].Y);
                        int x2 = points[points.Count - 2].X + (1 / 3) * (xm - points[points.Count - 2].X);
                        int y2 = points[points.Count - 2].Y + (1 / 3) * (ym - points[points.Count - 2].Y);

                        int x3 = xm + (2 / 3) * (points[points.Count - 1].X - points[points.Count - 3].X);
                        int y3 = ym + (2 / 3) * (points[points.Count - 1].Y - points[points.Count - 3].Y);
                        int x4 = points[points.Count - 1].X + (1 / 3) * (points[0].X - points[points.Count - 1].X);
                        int y4 = points[points.Count - 1].Y + (1 / 3) * (points[0].Y - points[points.Count - 1].Y);

                        g.DrawBezier(pen, x0, y0, x1, y1, x2, y2, xm, ym);
                        g.DrawBezier(pen, xm, ym, x3, y3, x4, y4, points[0].X, points[0].Y);

                    }
                }
                

                
                    
            }
            
            

            foreach (GPoint cp in points)
            {
                if (cp.OnCurve) g.FillRectangle(b, (cp.X) - 2, (cp.Y) - 2, 4, 4);
                if (!cp.OnCurve) g.DrawEllipse(pen, (cp.X ) - 2, ( cp.Y) - 2, 4, 4);
            }

            
        }

        public int GetMinX()
        {
            int xMin = 0;
            if (this.NumOfPoints > 0) xMin = points[0].X;
            if (this.NumOfPoints > 1)
                for (int i = 1; i < this.NumOfPoints; i++) 
            {
                    if (points[i].X < xMin) xMin = points[i].X;
            }
            return xMin;
        }

        public int GetMinY()
        {
            int yMin = 0;
            if (this.NumOfPoints > 0) yMin = points[0].Y;
            if (this.NumOfPoints > 1)
                for (int i = 1; i < this.NumOfPoints; i++)
                {
                    if (points[i].Y < yMin) yMin = points[i].Y;
                }
            return yMin;
        }

        public int GetMaxX()
        {
            int xMax = 0;
            if (this.NumOfPoints > 0) xMax = points[0].X;
            if (this.NumOfPoints > 1)
                for (int i = 1; i < this.NumOfPoints; i++)
                {
                    if (points[i].X > xMax) xMax = points[i].X;
                }
            return xMax;
        }

        public int GetMaxY()
        {
            int yMax = 0;
            if (this.NumOfPoints > 0) yMax = points[0].Y;
            if (this.NumOfPoints > 1)
                for (int i = 1; i < this.NumOfPoints; i++)
                {
                    if (points[i].Y > yMax) yMax = points[i].Y;
                }
            return yMax;
        }
    }
}
