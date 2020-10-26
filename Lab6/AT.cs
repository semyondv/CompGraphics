using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6 {
    static class AT {
        //public static void MatrixMul(Point3d point, float[,] mat) {
        //    float[,] m1 = {
        //        { 0, 0, 0, 0 },
        //        { point.X, point.Y, point.Z, 1 },
        //        { 0, 0, 0 , 0},
        //        { 0, 0, 0 , 0}
        //    },
        //        m2 = {
        //        { 0, 0, 0, 0 },
        //        { 0, 0, 0, 0 },
        //        { 0, 0, 0, 0 },
        //        { 0, 0, 0, 0 }
        //    };

        //    for (int i = 0; i < 3; i++)
        //        for (int j = 0; j < 3; j++)
        //            for (int k = 0; k < 3; k++)
        //                m2[i, j] += m1[i, k] * mat[k, j];

        //    point.X = m2[1, 0];
        //    point.Y = m2[1, 1];
        //    point.Z = m2[1, 2];
        //}
        static public void MatrixMul(PointXYZ p, float[,] matr2) {
            float[,] c = new float[1, 4];
            float[,] xyz = { { p.X, p.Y, p.Z, 1 } };

            for (int j = 0; j < 4; ++j) {
                for (int r = 0; r < 4; ++r)
                    c[0, j] += xyz[0, r] * matr2[r, j];
            }
            p.X = c[0, 0];
            p.Y = c[0, 1];
            p.Z = c[0, 2];
            
        }
        static public PointXYZ Move(PointXYZ p, float dx, float dy, float dz) {
            float[,] T = new float[,] {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { dx, dy, dz, 1 }
            };

            AT.MatrixMul(p, T);
            return p;
        }

        static public void RotateX(PointXYZ p, double angle) {
            double rangle = Math.PI * angle / 180;
            float sin = (float)Math.Sin(rangle);
            float cos = (float)Math.Cos(rangle);

            float[,] rotation_matrix = { 
                { 1,   0,     0,   0},
                { 0,  cos,   sin,  0},
                { 0,  -sin,  cos,  0},
                { 0,   0,     0,   1 } 
            };
            AT.MatrixMul(p, rotation_matrix);
        }

        static public void RotateY(PointXYZ p, double angle) {
            double rangle = Math.PI * angle / 180;
            float sin = (float)Math.Sin(rangle);
            float cos = (float)Math.Cos(rangle);

            float[,] rotation_matrix = {
                { cos,  0,  -sin,  0 },
                { 0,   1,   0,    0 },
                { sin,  0,  cos,  0 },
                { 0,   0,   0,    1 } 
            };
            AT.MatrixMul(p, rotation_matrix);
        }
        static public void RotateZ(PointXYZ p, double angle) {
            double rangle = Math.PI * angle / 180;
            float sin = (float)Math.Sin(rangle);
            float cos = (float)Math.Cos(rangle);

            float[,] rotation_matrix = { 
                {cos,   sin,  0,  0 },
                { -sin,  cos,  0,  0 },
                { 0,     0,   1,  0 },
                { 0,     0,   0,  1 } 
            };
            AT.MatrixMul(p, rotation_matrix);
        }

        static public void Scaling(PointXYZ p, float kx, float ky, float kz) {
            float[,] scale_matrix = {
                {kx, 0,  0,  0 },
                { 0,  ky, 0,  0 },
                { 0,  0,  kz, 0 },
                { 0,  0,  0,  1 }
            };

            AT.MatrixMul(p, scale_matrix);
        }
    }
}
