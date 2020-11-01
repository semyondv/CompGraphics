using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6 {
    public class PointXYZ {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public PointXYZ(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public PointXYZ(PointXYZ p) {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public void ReflectX() {
            X = -X;
        }

        public void ReflectY() {
            Y = -Y;
        }

        public void ReflectZ() {
            Z = -Z;
        }

        public PointF Draw(Graphics g) {
            PointF p = Point.Empty;

            float[,] mat = { 
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, (float)(-1/1001.0) },
                { 0, 0, 0, 1 }
            };

            float[,] c = new float[1, 4];
            float[,] xyz = { { X, Y, Z, 1 } };

            for (int j = 0; j < 4; ++j) {
                for (int r = 0; r < 4; ++r)
                    c[0, j] += xyz[0, r] * mat[r, j];
            }
            //AT.MatrixMul(this, mat);
            p.X = c[0, 0] / c[0, 3];
            p.Y = c[0, 1] / c[0, 3];

            //g.DrawRectangle(Pens.Black, p.X, p.Y, 2, 2);

            return p;
        }
    }
    class Figure {
        public List<PointXYZ> Points { get; }
        public PointXYZ Center { get; set; } = new PointXYZ(0, 0, 0);
        public Figure(Figure side) {
            Points = side.Points.Select(pt => new PointXYZ(pt.X, pt.Y, pt.Z)).ToList();
            Center = new PointXYZ(side.Center);
        }

        public Figure(List<PointXYZ> pts = null) {
            if (pts != null) {
                Points = new List<PointXYZ>(pts);
                //foreach (var item in pts) {
                //    Points.Add(new PointXYZ(item.X, item.Y, item.Z));
                //}
            }
        }

        public void UpdateCenter() {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
            foreach (PointXYZ p in Points) {
                Center.X += p.X;
                Center.Y += p.Y;
                Center.Z += p.Z;
            }
            Center.X /= Points.Count;
            Center.Y /= Points.Count;
            Center.Z /= Points.Count;
        }

        public void ReflectX() {
            UpdateCenter();
            Center.X = -Center.X;
            if (Points != null)
                foreach (var p in Points)
                    p.ReflectX();
            UpdateCenter();
        }
        public void ReflectY() {
            UpdateCenter();
            Center.Y = -Center.Y;
            if (Points != null)
                foreach (var p in Points)
                    p.ReflectY();
            UpdateCenter();
        }
        public void ReflectZ() {
            UpdateCenter();
            Center.Z = -Center.Z;
            foreach (var p in Points)
                p.ReflectZ();
            UpdateCenter();

        }
        public void Draw(Graphics g) {
            List<PointF> pts = new List<PointF>();

            foreach (PointXYZ p in Points) {
                pts.Add(p.Draw(g));
            }

            if (pts.Count > 1) {
                g.DrawLines(Pens.Black, pts.ToArray());
                g.DrawLine(Pens.Black, pts[0], pts[pts.Count - 1]);
            } else if (pts.Count == 1)
                g.DrawRectangle(Pens.Black, pts[0].X, pts[0].Y, 1, 1);
        }

        public void Move(float dx, float dy, float dz) {
            for (int i = 0; i < Points.Count; i++)
                AT.Move(Points[i], dx, dy, dz);
            UpdateCenter();
        }

        public void RotateX(double angle) {
            for (int i = 0; i < Points.Count; i++)
                AT.RotateX(Points[i], angle);
            UpdateCenter();
        }
        public void RotateY(double angle) {
            for (int i = 0; i < Points.Count; i++)
                AT.RotateY(Points[i], angle);
            UpdateCenter();
        }

        public void RotateZ(double angle) {
            for (int i = 0; i < Points.Count; i++)
                AT.RotateZ(Points[i], angle);
            UpdateCenter();
        }

        public void Scaling(float kx, float ky, float kz) {
            for (int i = 0; i < Points.Count; i++)
                AT.Scaling(Points[i], kx, ky, kz);
            UpdateCenter();
        }
    }
    class Figure3D {
        public List<Figure> Sides { get; set; } = null;
        public PointXYZ Center { get; set; } = new PointXYZ(0, 0, 0);
        public Figure3D(List<Figure> fs = null) {
            if (fs != null) {
                Sides = fs.Select(side => new Figure(side)).ToList();
                UpdateCenter();
            }
        }
        public void UpdateCenter() {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
            foreach (Figure f in Sides) {
                Center.X += f.Center.X;
                Center.Y += f.Center.Y;
                Center.Z += f.Center.Z;
            }
            Center.X /= Sides.Count;
            Center.Y /= Sides.Count;
            Center.Z /= Sides.Count;
        }

        public void Draw(Graphics g) {
            foreach (Figure f in Sides)
                f.Draw(g);
        }

        public void Move(float dx, float dy, float dz) {
            foreach (Figure f in Sides)
                f.Move(dx, dy, dz);
            UpdateCenter();
        }

        public void RotateX(double angle) {
            foreach (Figure f in Sides)
                f.RotateX(angle);
            UpdateCenter();
        }

        public void RotateY(double angle) {
            foreach (Figure f in Sides)
                f.RotateY(angle);
            UpdateCenter();
        }

        public void RotateZ(double angle) {
            foreach (Figure f in Sides)
                f.RotateZ(angle);
            UpdateCenter();
        }

        public void Scaling(float kx, float ky, float kz) {
            foreach (Figure f in Sides)
                f.Scaling(kx, ky, kz);
            UpdateCenter();
        }

        public void ReflectX() {
            UpdateCenter();
            foreach (var f in Sides)
                f.ReflectX();
            UpdateCenter();
        }

        public void ReflectY() {
            UpdateCenter();
            foreach (var f in Sides)
                f.ReflectY();
            UpdateCenter();
        }

        public void ReflectZ() {
            UpdateCenter();
            foreach (var f in Sides)
                f.ReflectZ();

            UpdateCenter();
        }
    }

    class Hexahedron : Figure3D {
        public Hexahedron(int szie = 50) {
            Figure f = new Figure(
             new List<PointXYZ>
             {
                    new PointXYZ(-szie, szie, szie),
                    new PointXYZ(szie, szie, szie),
                    new PointXYZ(szie, -szie, szie),
                    new PointXYZ(-szie, -szie, szie)
             }
         );


            Sides = new List<Figure> { f };

            List<PointXYZ> l1 = new List<PointXYZ>();
            foreach (var point in f.Points) {
                l1.Add(new PointXYZ(point.X, point.Y, point.Z - 2 * szie));
            }
            Figure f1 = new Figure(
                    new List<PointXYZ>
                    {
                        new PointXYZ(-szie, szie, -szie),
                        new PointXYZ(-szie, -szie, -szie),
                        new PointXYZ(szie, -szie, -szie),
                        new PointXYZ(szie, szie, -szie)
                    });

            Sides.Add(f1);

            List<PointXYZ> l2 = new List<PointXYZ>
            {
                new PointXYZ(f.Points[2]),
                new PointXYZ(f1.Points[2]),
                new PointXYZ(f1.Points[1]),
                new PointXYZ(f.Points[3]),
            };
            Figure f2 = new Figure(l2);
            Sides.Add(f2);

            List<PointXYZ> l3 = new List<PointXYZ>
            {
                new PointXYZ(f1.Points[0]),
                new PointXYZ(f1.Points[3]),
                new PointXYZ(f.Points[1]),
                new PointXYZ(f.Points[0]),
            };
            Figure f3 = new Figure(l3);
            Sides.Add(f3);

            List<PointXYZ> l4 = new List<PointXYZ>
            {
                new PointXYZ(f1.Points[0]),
                new PointXYZ(f.Points[0]),
                new PointXYZ(f.Points[3]),
                new PointXYZ(f1.Points[1])
            };
            Figure f4 = new Figure(l4);
            Sides.Add(f4);

            List<PointXYZ> l5 = new List<PointXYZ>
            {
                new PointXYZ(f1.Points[3]),
                new PointXYZ(f1.Points[2]),
                new PointXYZ(f.Points[2]),
                new PointXYZ(f.Points[1])
            };
            Figure f5 = new Figure(l5);
            Sides.Add(f5);

            UpdateCenter();
        }
    }
    class Tetrahedron : Figure3D {
        public Tetrahedron() {
            //if (cube == null) {
            //    cube = new Figure3D();
            //    cube.make_hexahedron();
            //}
            Figure3D cube = new Hexahedron();
            Figure f0 = new Figure(
                new List<PointXYZ>
                {
                    new PointXYZ(cube.Sides[0].Points[0]),
                    new PointXYZ(cube.Sides[1].Points[1]),
                    new PointXYZ(cube.Sides[1].Points[3])
                }
            );

            Figure f1 = new Figure(
                new List<PointXYZ>
                {
                    new PointXYZ(cube.Sides[1].Points[3]),
                    new PointXYZ(cube.Sides[1].Points[1]),
                    new PointXYZ(cube.Sides[0].Points[2])
                }
            );

            Figure f2 = new Figure(
                new List<PointXYZ>
                {
                    new PointXYZ(cube.Sides[0].Points[2]),
                    new PointXYZ(cube.Sides[1].Points[1]),
                    new PointXYZ(cube.Sides[0].Points[0])
                }
            );

            Figure f3 = new Figure(
                new List<PointXYZ>
                {
                    new PointXYZ(cube.Sides[0].Points[2]),
                    new PointXYZ(cube.Sides[0].Points[0]),
                    new PointXYZ(cube.Sides[1].Points[3])
                }
            );

            Sides = new List<Figure> { f0, f1, f2, f3 };
            UpdateCenter();
        }
    }
    class Octahedron : Figure3D {
        public Octahedron() {
            
        }
    }
}
