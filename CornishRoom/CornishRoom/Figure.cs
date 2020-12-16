using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RayTrace {

    public class Polygon {
        public Figure host = null;
        public List<int> points = new List<int>();
        public Pen DPen = new Pen(Color.Black);
        public PointXYZ Normal;

        public Polygon(Figure h = null) {
            host = h;
        }

        public Polygon(Polygon s) {
            points = new List<int>(s.points);
            host = s.host;
            DPen = s.DPen.Clone() as Pen;
            Normal = new PointXYZ(s.Normal);
        }

        public PointXYZ GetPoint(int index) {
            if (host != null)
                return host.Points[points[index]];
            return null;
        }

        public static PointXYZ GetNormal(Polygon S) {
            if (S.points.Count() < 3)
                return new PointXYZ(0, 0, 0);
            PointXYZ U = S.GetPoint(1) - S.GetPoint(0);
            PointXYZ V = S.GetPoint(S.points.Count - 1) - S.GetPoint(0);
            PointXYZ normal = U * V;
            return PointXYZ.normal(normal);
        }

        public void CalculateSideNormal() {
            Normal = GetNormal(this);
        }
    }

    public class Figure
    {
        public List<PointXYZ> Points = new List<PointXYZ>(); 
        public List<Polygon> Polygons = new List<Polygon>();        
        public Surface FigureSurface;
        public Figure() { }

        public Figure(Figure f)
        {
            foreach (PointXYZ p in f.Points)
                Points.Add(new PointXYZ(p));

            foreach (Polygon s in f.Polygons)
            {
                Polygons.Add(new Polygon(s));
                Polygons.Last().host = this;
            }
        }

        public bool TriangleInter(Ray r, PointXYZ p0, PointXYZ p1, PointXYZ p2, out float intersect)
        {
            intersect = -1;
            PointXYZ edge1 = p1 - p0;
            PointXYZ edge2 = p2 - p0;
            PointXYZ h = r.End * edge2;
            float a = PointXYZ.ScalarMul(edge1, h);
            if (a > -0.0001 && a < 0.0001)
                return false;       // Этот луч параллелен этому треугольнику.
            float f = 1.0f / a;
            PointXYZ s = r.Start - p0;
            float u = f * PointXYZ.ScalarMul(s, h);
            if (u < 0 || u > 1)
                return false;
            PointXYZ q = s * edge1;
            float v = f * PointXYZ.ScalarMul(r.End, q);
            if (v < 0 || u + v > 1)
                return false;
            // На этом этапе мы можем вычислить t, чтобы узнать, где находится точка пересечения на линии.
            float t = f * PointXYZ.ScalarMul(edge2, q);
            if (t > 0.0001)
            {
                intersect = t;
                return true;
            }
            else      //Это означает, что есть пересечение линий, но не пересечение лучей.
                return false;
        }

        // пересечение луча с фигурой
        public virtual bool FigureIntersection(Ray r, out float intersect, out PointXYZ normal)
        {
            intersect = 0;
            normal = null;
            Polygon side = null;
            foreach (Polygon figure_side in Polygons)
            {
                
                if (figure_side.points.Count == 3)
                {
                    if (TriangleInter(r, figure_side.GetPoint(0), figure_side.GetPoint(1), figure_side.GetPoint(2), out float t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                }

                else if (figure_side.points.Count == 4)
                {
                    if (TriangleInter(r, figure_side.GetPoint(0), figure_side.GetPoint(1), figure_side.GetPoint(3), out float t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                    else if (TriangleInter(r, figure_side.GetPoint(1), figure_side.GetPoint(2), figure_side.GetPoint(3), out t) && (intersect == 0 || t < intersect))
                    {
                        intersect = t;
                        side = figure_side;
                    }
                }
            }
            if (intersect != 0)
            {
                normal = Polygon.GetNormal(side);
                FigureSurface.Color = new PointXYZ(side.DPen.Color.R / 255f, side.DPen.Color.G / 255f, side.DPen.Color.B / 255f);
                return true;
            }
            return false;
        }

        public float[,] GetMatrix()
        {
            var matrix = new float[Points.Count, 4];
            for (int i = 0; i < Points.Count; i++)
            {
                matrix[i, 0] = Points[i].X;
                matrix[i, 1] = Points[i].Y;
                matrix[i, 2] = Points[i].Z;
                matrix[i, 3] = 1;
            }
            return matrix;
        }

        public void ApplyMatrix(float[,] matrix)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].X = matrix[i, 0] / matrix[i, 3];
                Points[i].Y = matrix[i, 1] / matrix[i, 3];
                Points[i].Z = matrix[i, 2] / matrix[i, 3];
            }
        }

        private PointXYZ GetCenter()
        {
            PointXYZ res = new PointXYZ(0, 0, 0);
            foreach (PointXYZ p in Points)
            {
                res.X += p.X;
                res.Y += p.Y;
                res.Z += p.Z;

            }
            res.X /= Points.Count();
            res.Y /= Points.Count();
            res.Z /= Points.Count();
            return res;
        }

        public void RotateArondRad(float rangle, string type)
        {
            float[,] mt = GetMatrix();
            PointXYZ center = GetCenter();
            switch (type)
            {
                case "CX":
                    mt = ApplyOffset(mt, -center.X, -center.Y, -center.Z);
                    mt = ApplyRotation_X(mt, rangle);
                    mt = ApplyOffset(mt, center.X, center.Y, center.Z);
                    break;
                case "CY":
                    mt = ApplyOffset(mt, -center.X, -center.Y, -center.Z);
                    mt = ApplyRotation_Y(mt, rangle);
                    mt = ApplyOffset(mt, center.X, center.Y, center.Z);
                    break;
                case "CZ":
                    mt = ApplyOffset(mt, -center.X, -center.Y, -center.Z);
                    mt = ApplyRotation_Z(mt, rangle);
                    mt = ApplyOffset(mt, center.X, center.Y, center.Z);
                    break;
                case "X":
                    mt = ApplyRotation_X(mt, rangle);
                    break;
                case "Y":
                    mt = ApplyRotation_Y(mt, rangle);
                    break;
                case "Z":
                    mt = ApplyRotation_Z(mt, rangle);
                    break;
                default:
                    break;
            }
            ApplyMatrix(mt);
        }

        public void RotateAround(float angle, string type)
        {
            RotateArondRad(angle * (float)Math.PI / 180, type);
        }
        public void RotateAround(float angle) {
            RotateArondRad(angle * (float)Math.PI / 180, "SZ");
        }

        public void Scale_axis(float xs, float ys, float zs)
        {
            float[,] pnts = GetMatrix();
            pnts = ApplyScale(pnts, xs, ys, zs);
            ApplyMatrix(pnts);
        }

        public void SetLocation(float xs, float ys, float zs)
        {
            ApplyMatrix(ApplyOffset(GetMatrix(), xs, ys, zs));
        }

        public void SetPen(Pen dw)
        {
            foreach (Polygon s in Polygons)
                s.DPen = dw;
        }

        public void ScaleAroundCenter(float xs, float ys, float zs)
        {
            float[,] pnts = GetMatrix();
            PointXYZ p = GetCenter();
            pnts = ApplyOffset(pnts, -p.X, -p.Y, -p.Z);
            pnts = ApplyScale(pnts, xs, ys, zs);
            pnts = ApplyOffset(pnts, p.X, p.Y, p.Z);
            ApplyMatrix(pnts);
        }

        public void LineRotateRad(float rang, PointXYZ p1, PointXYZ p2)
        {
            p2 = new PointXYZ(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            p2 = PointXYZ.normal(p2);
            float[,] mt = GetMatrix();
            ApplyMatrix(RotateAroundLine(mt, p1, p2, rang));
        }

        public void LineRotate(float ang, PointXYZ p1, PointXYZ p2)
        {
            ang = ang * (float)Math.PI / 180;
            LineRotateRad(ang, p1, p2);
        }

        private static float[,] RotateAroundLine(float[,] transform_matrix, PointXYZ start, PointXYZ dir, float angle)
        {
            float cos_angle = (float)Math.Cos(angle);
            float sin_angle = (float)Math.Sin(angle);
            float val00 = dir.X * dir.X + cos_angle * (1 - dir.X * dir.X);
            float val01 = dir.X * (1 - cos_angle) * dir.Y + dir.Z * sin_angle;
            float val02 = dir.X * (1 - cos_angle) * dir.Z - dir.Y * sin_angle;
            float val10 = dir.X * (1 - cos_angle) * dir.Y - dir.Z * sin_angle;
            float val11 = dir.Y * dir.Y + cos_angle * (1 - dir.Y * dir.Y);
            float val12 = dir.Y * (1 - cos_angle) * dir.Z + dir.X * sin_angle;
            float val20 = dir.X * (1 - cos_angle) * dir.Z + dir.Y * sin_angle;
            float val21 = dir.Y * (1 - cos_angle) * dir.Z - dir.X * sin_angle;
            float val22 = dir.Z * dir.Z + cos_angle * (1 - dir.Z * dir.Z);
            float[,] rotateMatrix = new float[,] { { val00, val01, val02, 0 }, { val10, val11, val12, 0 }, { val20, val21, val22, 0 }, { 0, 0, 0, 1 } };
            return ApplyOffset(MultiplyMatrix(ApplyOffset(transform_matrix, -start.X, -start.Y, -start.Z), rotateMatrix), start.X, start.Y, start.Z);
        }

        private static float[,] MultiplyMatrix(float[,] m1, float[,] m2)
        {
            float[,] res = new float[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        res[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return res;
        }

        private static float[,] ApplyOffset(float[,] transform_matrix, float offset_x, float offset_y, float offset_z)
        {
            float[,] translationMatrix = new float[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { offset_x, offset_y, offset_z, 1 } };
            return MultiplyMatrix(transform_matrix, translationMatrix);
        }

        private static float[,] ApplyRotation_X(float[,] transform_matrix, float angle)
        {
            float[,] rotationMatrix = new float[,] { { 1, 0, 0, 0 }, { 0, (float)Math.Cos(angle), (float)Math.Sin(angle), 0 },
                { 0, -(float)Math.Sin(angle), (float)Math.Cos(angle), 0}, { 0, 0, 0, 1} };
            return MultiplyMatrix(transform_matrix, rotationMatrix);
        }

        private static float[,] ApplyRotation_Y(float[,] transform_matrix, float angle)
        {
            float[,] rotationMatrix = new float[,] { { (float)Math.Cos(angle), 0, -(float)Math.Sin(angle), 0 }, { 0, 1, 0, 0 },
                { (float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0}, { 0, 0, 0, 1} };
            return MultiplyMatrix(transform_matrix, rotationMatrix);
        }

        private static float[,] ApplyRotation_Z(float[,] transform_matrix, float angle)
        {
            float[,] rotationMatrix = new float[,] { { (float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0 }, { -(float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0 },
                { 0, 0, 1, 0 }, { 0, 0, 0, 1} };
            return MultiplyMatrix(transform_matrix, rotationMatrix);
        }

        private static float[,] ApplyScale(float[,] transform_matrix, float scale_x, float scale_y, float scale_z)
        {
            float[,] scaleMatrix = new float[,] { { scale_x, 0, 0, 0 }, { 0, scale_y, 0, 0 }, { 0, 0, scale_z, 0 }, { 0, 0, 0, 1 } };
            return MultiplyMatrix(transform_matrix, scaleMatrix);
        }

        static public Figure CreateCube(float sz)
        {
            Figure res = new Figure();
            res.Points.Add(new PointXYZ(sz / 2, sz / 2, sz / 2)); // 0 
            res.Points.Add(new PointXYZ(-sz / 2, sz / 2, sz / 2)); // 1
            res.Points.Add(new PointXYZ(-sz / 2, sz / 2, -sz / 2)); // 2
            res.Points.Add(new PointXYZ(sz / 2, sz / 2, -sz / 2)); //3

            res.Points.Add(new PointXYZ(sz / 2, -sz / 2, sz / 2)); // 4
            res.Points.Add(new PointXYZ(-sz / 2, -sz / 2, sz / 2)); //5
            res.Points.Add(new PointXYZ(-sz / 2, -sz / 2, -sz / 2)); // 6
            res.Points.Add(new PointXYZ(sz / 2, -sz / 2, -sz / 2)); // 7

            Polygon s = new Polygon(res);
            s.points.AddRange(new int[] { 3, 2, 1, 0 });
            res.Polygons.Add(s);

            s = new Polygon(res);
            s.points.AddRange(new int[] { 4, 5, 6, 7 });
            res.Polygons.Add(s);

            s = new Polygon(res);
            s.points.AddRange(new int[] { 2, 6, 5, 1 });
            res.Polygons.Add(s);

            s = new Polygon(res);
            s.points.AddRange(new int[] { 0, 4, 7, 3 });
            res.Polygons.Add(s);

            s = new Polygon(res);
            s.points.AddRange(new int[] { 1, 5, 4, 0 });
            res.Polygons.Add(s);

            s = new Polygon(res);
            s.points.AddRange(new int[] { 2, 3, 7, 6 });
            res.Polygons.Add(s);
            return res;
        }
    }

    public class Surface {
        public float reflection;
        public float refraction;
        public float environment;
        //фоновое освещение
        public float ambient;
        //дифузное освещение
        public float diffuse;

        public PointXYZ Color { get; set; }

        public Surface(float refl, float refr, float amb, float dif, float env = 1) {
            reflection = refl;
            refraction = refr;
            ambient = amb;
            diffuse = dif;
            environment = env;
        }

        public Surface(Surface m) {
            reflection = m.reflection;
            refraction = m.refraction;
            environment = m.environment;
            ambient = m.ambient;
            diffuse = m.diffuse;
            Color = new PointXYZ(m.Color);
        }

        public Surface() {

        }
    }

    public class Lamp : Figure {
        public PointXYZ point_light;
        public PointXYZ color_light;

        public Lamp(PointXYZ p, PointXYZ c) {
            point_light = new PointXYZ(p);
            color_light = new PointXYZ(c);
        }

        //модель освещения
        public PointXYZ Lumiance(PointXYZ hit_point, PointXYZ normal, PointXYZ material_color, float diffuse_coef) {
            PointXYZ direction = PointXYZ.normal(point_light - hit_point);// направление луча 
            //если угол между нормалью и направлением луча больше 90 градусов,то диффузное  освещение равно 0
            PointXYZ diff = diffuse_coef * color_light * Math.Max(PointXYZ.ScalarMul(normal, direction), 0);
            return new PointXYZ(diff.X * material_color.X, diff.Y * material_color.Y, diff.Z * material_color.Z);
        }
    }


}
