using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//new Surface(0, 0.7f, 0.1f, 0.5f, 1f);

namespace RayTrace {
    public partial class Form1 : Form
    {
        public List<Figure> figuresList = new List<Figure>();
        Lamp raySource;
        public Color[,] pixels_color;                    
        public PointXYZ[,] pixels;
        public PointXYZ cameraPoint;
        public PointXYZ ul, ur, dl, dr;
        public int h, w;

        public Form1()
        {
            InitializeComponent();
            cameraPoint = new PointXYZ();
            ul = new PointXYZ();
            ur = new PointXYZ();
            dl = new PointXYZ();
            dr = new PointXYZ();
            h = pictureBox1.Height;
            w = pictureBox1.Width;
            pictureBox1.Image = new Bitmap(w, h);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Figure room = MakeRoom();
            cameraPoint = MakeCamera(room.Polygons[0]);
            figuresList.Add(room);

            raySource = new Lamp(new PointXYZ(0f, 2f, 4.9f), new PointXYZ(1f, 1f, 1f));

            AddBigCube();
            AddLittleCube();

            /*
 Учитывая разницу между размером комнаты и экранным отображение приводим координаты к пикселям
 */
            pixels = new PointXYZ[w, h];
            pixels_color = new Color[w, h];
            PointXYZ step_up = (ur - ul) / (w - 1);//отношение ширины комнаты к ширине экрана
            PointXYZ step_down = (dr - dl) / (w - 1);//отношение высоты комнаты к высоте экрана
            PointXYZ up = new PointXYZ(ul);
            PointXYZ down = new PointXYZ(dl);
            for (int i = 0; i < w; ++i) {
                PointXYZ step_y = (up - down) / (h - 1);
                PointXYZ d = new PointXYZ(down);
                for (int j = 0; j < h; ++j) {
                    pixels[i, j] = d;
                    d += step_y;
                }
                up += step_up;
                down += step_down;
            }
            /*
             Количество первичных лучей также известно – это общее
             количество пикселей видового окна
             */
            for (int i = 0; i < w; ++i) {
                for (int j = 0; j < h; ++j) {
                    Ray r = new Ray(cameraPoint, pixels[i, j]);
                    r.Start = new PointXYZ(pixels[i, j]);
                    PointXYZ color = RTAlgotithm(r, 10, 1);//луч,кол-во итераций,коэфф
                    if (color.X > 1.0f || color.Y > 1.0f || color.Z > 1.0f)
                        color = PointXYZ.normal(color);
                    pixels_color[i, j] = Color.FromArgb((int)(255 * color.X), (int)(255 * color.Y), (int)(255 * color.Z));
                }
            }


            for (int i = 0; i < w; ++i){
                for (int j = 0; j < h; ++j)
                {
                    (pictureBox1.Image as Bitmap).SetPixel(i, j, pixels_color[i, j]);
                }
                pictureBox1.Invalidate();
            }
        }

        PointXYZ MakeCamera(Polygon side) {
            //нормаль стороны комнаты
            PointXYZ normal = Polygon.GetNormal(side);
            // центр стороны комнаты
            PointXYZ center = (ul + ur + dl + dr) / 4;   
            return center + normal * 11;
        }


        Figure MakeRoom() {
            Figure room = Figure.CreateCube(10);
            ul = room.Polygons[0].GetPoint(0);
            ur = room.Polygons[0].GetPoint(1);
            dr = room.Polygons[0].GetPoint(2);
            dl = room.Polygons[0].GetPoint(3);

            room.SetPen(new Pen(Color.Gray));
            room.Polygons[0].DPen = new Pen(Color.Green);
            room.Polygons[1].DPen = new Pen(Color.Green);
            room.Polygons[2].DPen = new Pen(Color.GreenYellow);
            room.Polygons[3].DPen = new Pen(Color.GreenYellow);
            room.FigureSurface = new Surface(0, 0, 0.05f, 0.7f);

            return room;
        }

        void AddBigCube() {
            Figure bigCube = Figure.CreateCube(2.8f);
            bigCube.SetLocation(-1.5f, 3.5f, -3.9f);
            bigCube.SetPen(new Pen(Color.DeepPink));
            bigCube.FigureSurface = new Surface(0f, 0f, 0.1f, 0.7f, 1.5f);
            figuresList.Add(bigCube);
        }

        void AddLittleCube() {
            Figure little = Figure.CreateCube(2f);
            little.SetLocation(2.2f, 2.5f, 1.95f);
            little.SetPen(new Pen(Color.Red));
            little.FigureSurface = new Surface(0f, 0f, 0.1f, 0.7f, 1.5f);

            figuresList.Add(little);
        }

        public PointXYZ RTAlgotithm(Ray r, int iter, float env)
        {
            if (iter <= 0)
                return new PointXYZ(0, 0, 0);
            float intersectionRayFigure = 0;// позиция точки пересечения луча с фигурой на луче
            //нормаль стороны фигуры,с которой пересекся луч
            PointXYZ normal = null;
            Surface surface = new Surface();
            PointXYZ res_color = new PointXYZ(0, 0, 0);

            bool isFigureRefrat = false;

            foreach (var f in figuresList)
            {
                if (f.FigureIntersection(r, out float intersect, out PointXYZ norm))
                    if (intersect < intersectionRayFigure || intersectionRayFigure == 0)// нужна ближайшая фигура к точке наблюдения
                    {
                        intersectionRayFigure = intersect;
                        normal = norm;
                        surface = new Surface(f.FigureSurface);
                    }
            }

            if (intersectionRayFigure == 0)//если не пересекается с фигурой
                return new PointXYZ(0, 0, 0);//Луч уходит в свободное пространство .Возвращаем значение по умолчанию

            //угол между направление луча и нормалью стороны острый
            //определяем из какой среды в какую
            //http://edu.glavsprav.ru/info/zakon-prelomleniya-sveta/
            if (PointXYZ.ScalarMul(r.End, normal) > 0)
            {
                normal *= -1;
                isFigureRefrat = true;
            }


            //Точка пересечения луча с фигурой
            PointXYZ hit_point = r.Start + r.End * intersectionRayFigure;
            /*В точке пересечения луча с объектом строится три вторичных
              луча – один в направлении отражения (1), второй – в направлении
              источника света (2), третий в направлении преломления
              прозрачной поверхностью (3).
             */

            //цвет коэффициент принятия фонового освещения
            PointXYZ lightk = raySource.color_light * surface.ambient;
            lightk.X = (lightk.X * surface.Color.X);
            lightk.Y = (lightk.Y * surface.Color.Y);
            lightk.Z = (lightk.Z * surface.Color.Z);
            res_color += lightk;

            //диффузное освещение
            //если точка пересечения луча с объектом видна из источника света
            float raydist = (raySource.point_light - hit_point).Distance(); //координаты источника света луча
            Ray tmp_r = new Ray(hit_point, raySource.point_light);
            foreach (Figure fig in figuresList)
                if (fig.FigureIntersection(tmp_r, out float t, out PointXYZ n))
                    if (t < raydist || t > 0.0001)
                        res_color += raySource.Lumiance(hit_point, normal, surface.Color, surface.diffuse);



            /*Для отраженного луча
              проверяется возможность
              пересечения с другими
              объектами сцены.

                Если пересечений нет, то
                интенсивность и цвет
                отраженного луча равна
                интенсивности и цвету фона.

                Если пересечение есть, то в
                новой точке снова строится
                три типа лучей – теневые,
                отражения и преломления. 
              */
            if (surface.reflection > 0)
            {
                Ray refray = r.Reflect(hit_point, normal);
                res_color += surface.reflection * RTAlgotithm(refray, iter - 1, env);
            }


            //расчет коэфициентов преломления
            if (surface.refraction > 0)
            {
                //взависимости от того,из какой среды в какую,будет меняться коэффициент приломления
                float refractk;
                if (isFigureRefrat)
                    refractk = surface.environment;
                else
                    refractk = 1 / surface.environment;

                Ray refracted_ray = r.Refract(hit_point, normal, surface.refraction, refractk);//создаем приломленный луч

                /*
                 Как и в предыдущем случае,
                 проверяется пересечение вновь
                 построенного луча с объектами,
                 и, если они есть, в новой точке
                 строятся три луча, если нет – используется интенсивность и
                 цвет фона.
                 */
                if (refracted_ray != null)
                    res_color += surface.refraction * RTAlgotithm(refracted_ray, iter - 1, surface.environment);
            }
            return res_color;
        }
    }
}

