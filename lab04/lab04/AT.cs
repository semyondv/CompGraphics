using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04
{
    static class AT
    {
        public static PointF Intersection(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            PointF i = new PointF(float.MaxValue, float.MaxValue);
            PointF s1 = new PointF();
            PointF s2 = new PointF();
            s1.X = p1.X - p0.X;
            s1.Y = p1.Y - p0.Y;
            s2.X = p3.X - p2.X;
            s2.Y = p3.Y - p2.Y;
            float s, t;
            s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
            t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                i.X = p0.X + (t * s1.X);
                i.Y = p0.Y + (t * s1.Y);

            }
            return i;
        }

        //Смещение на dx, dy
        public static PointF Move(PointF curP, int dx, int dy)
        {
            double[,] matrix = 
                { { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { dx, dy, 1 } };
            PointF newP = MatrixMul(curP, matrix);

            return newP;
        }


        public static PointF MatrixMul(PointF point, double[,] mat)
        {
            PointF newP = new PointF(0, 0);
            double[,] m1 = { 
                { 0, 0, 0 },
                { point.X, point.Y, 1 },
                { 0, 0, 0 } 
            },
                m2 = { 
                { 0, 0, 0 },
                { 0, 0, 0 }, 
                { 0, 0, 0 } 
            };

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        m2[i, j] += m1[i, k] * mat[k, j];

            newP.X = (int)m2[1, 0];
            newP.Y = (int)m2[1, 1];
            return newP;
        }

        //Масштабирование относительно центра
        public static PointF Scaling(PointF center, double kx, double ky, PointF curP)
        {
            double[,] move_mat1 = { { 1, 0, 0 }, { 0, 1, 0 }, { -center.X, -center.Y, 1 } };
            double[,] move_mat2 = { { 1, 0, 0 }, { 0, 1, 0 }, { center.X, center.Y, 1 } };
            double[,] scale_mat = { { 1 / kx, 0, 0 }, { 0, 1 / ky, 0 }, { 0, 0, 1 } };

            PointF newP = MatrixMul(curP, move_mat1);
            newP = MatrixMul(newP, scale_mat);
            newP = MatrixMul(newP, move_mat2);

            return newP;
        }

        //Вращение относительно центра
        public static PointF Rotation(PointF center, int angle, PointF curP)
        {
            double[,] m1 = { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { -center.X, -center.Y, 1 } },
                m2 = { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { center.X, center.Y, 1 } 
            },
                m3 = { 
                { Math.Cos(Math.PI / 180.0 * angle), Math.Sin(Math.PI / 180.0 * angle), 0 }, 
                { -Math.Sin(Math.PI / 180.0 * angle), Math.Cos(Math.PI / 180.0 * angle), 0 }, 
                { 0, 0, 1 } 
            };

            PointF newP = MatrixMul(curP, m1);
            newP = MatrixMul(newP, m3);
            newP = MatrixMul(newP, m2);

            return newP;
        }


        public static PointF PolygonCenter(List<PointF> points)
        {
            float sum_x = 0, sum_y = 0;
            for (int i = 0; i < points.Count; i++)
            {
                sum_x += points[i].X;
                sum_y += points[i].Y;
            }
            return new PointF(sum_x / points.Count, sum_y / points.Count);
        }
    }
}
