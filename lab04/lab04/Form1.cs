using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab04 {
    public partial class Form1 : Form 
    {
        Graphics pic;
        List<PointF> pointsArr;
        PointF firstP, lastP;

        public Form1() {
            InitializeComponent();
            pointsArr = new List<PointF>();
            picArea.Image = new Bitmap(picArea.Width, picArea.Height);
            pic = Graphics.FromImage(picArea.Image);
            pic.Clear(Color.White);
        }

        private void button1_Click(object sender, EventArgs e) {
            picArea.Image = new Bitmap(picArea.Width, picArea.Height);
            pic = Graphics.FromImage(picArea.Image);
            pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
            pointsArr.Clear();
        }

        private void picArea_MouseDown(object sender, MouseEventArgs e) {
            firstP = e.Location;
            pointsArr.Add(firstP);
        }

        private void button2_Click(object sender, EventArgs e) {
            if (poly.Checked) {
                if (polyAcions.SelectedIndex == 0) {
                    for (int i = 0; i < pointsArr.Count; i++)
                        pointsArr[i] = AT.Move(pointsArr[i], (int)dx.Value, (int)dy.Value);
                    pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
                    for (int i = 0; i < pointsArr.Count - 1; i++)
                        pic.DrawLine(new Pen(Color.Black), pointsArr[i], pointsArr[i + 1]);
                } else if (polyAcions.SelectedIndex == 1) {
                    PointF center = AT.PolygonCenter(pointsArr);
                    for (int i = 0; i < pointsArr.Count; i++)
                        pointsArr[i] = AT.Rotation(center, (int)angleVal.Value, pointsArr[i]);
                    pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
                    for (int i = 0; i < pointsArr.Count - 1; i++)
                        pic.DrawLine(new Pen(Color.Black), pointsArr[i], pointsArr[i + 1]);
                } else if(polyAcions.SelectedIndex == 2) {
                    PointF center = AT.PolygonCenter(pointsArr);
                    for (int i = 0; i < pointsArr.Count; i++)
                        pointsArr[i] = AT.Scaling(center, (double)dx.Value, (double)dy.Value, pointsArr[i]);
                    pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
                    for (int i = 0; i < pointsArr.Count - 1; i++)
                        pic.DrawLine(new Pen(Color.Black), pointsArr[i], pointsArr[i + 1]);
                } 
                
            } else if (line.Checked) {
               if(lineActions.SelectedIndex == 2)
                {
                    PointF center = new PointF((pointsArr[0].X + pointsArr[1].X) / 2, (pointsArr[0].Y + pointsArr[1].Y) / 2);
                    for (int i = 0; i < pointsArr.Count; i++)
                        pointsArr[i] = AT.Rotation(center, 90, pointsArr[i]);
                    pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
                    for (int i = 0; i < pointsArr.Count - 1; i++)
                        pic.DrawLine(new Pen(Color.Black), pointsArr[i], pointsArr[i + 1]);
                }
               else if (lineActions.SelectedIndex == 3) {
                    if (pointsArr.Count >= 3) {
                        PointF dotPos = pointsArr.Last();
                        double res = (dotPos.X - pointsArr[pointsArr.Count - 2].X) 
                            * (pointsArr[pointsArr.Count - 3].Y - pointsArr[pointsArr.Count - 2].Y) 
                            - (dotPos.Y - pointsArr[pointsArr.Count - 2].Y) 
                            * (pointsArr[pointsArr.Count - 3].X - pointsArr[pointsArr.Count - 2].X);


                        //!!! линию рисовать сверху вниз!!!
                        if (res < 0)
                            pointPositionInfo.Text = "Точка находится правее";
                        else if (res > 0)
                            pointPositionInfo.Text = "Точка находится левее";
                        else
                            pointPositionInfo.Text = "Точка находится на линии";
                    }
                }
            }
            else if (dot.Checked)
            {
                if (polyAcions.SelectedIndex == 3)
                {
                    CheckConvex(pointsArr[pointsArr.Count - 1]);
                }
                else if (polyAcions.SelectedIndex == 4)
                {
                    CheckNonConvex(pointsArr[pointsArr.Count - 1]);
                }
            }


            picArea.Invalidate();
        }

        private void picArea_MouseUp(object sender, MouseEventArgs e) {
            Pen p = new Pen(Color.Black);
            if (line.Checked) {
                lastP = e.Location;
                pointsArr.Add(lastP);
                pic.DrawLine(p, firstP, lastP);
                if (lineActions.SelectedIndex == 0) {
                    PointF inter;

                    if (pointsArr.Count > 3) {
                        inter = AT.Intersection(pointsArr[pointsArr.Count - 4],
                            pointsArr[pointsArr.Count - 3],
                            pointsArr[pointsArr.Count - 2],
                            pointsArr[pointsArr.Count - 1]);
                    } else
                        inter = new PointF(float.MaxValue, float.MaxValue);

                    if (inter.X == float.MaxValue && inter.Y == float.MaxValue)
                        interOut.Text = "Нет пересечения";
                    else
                        interOut.Text = $"X: {inter.X} Y: {inter.Y}";

                }
            } else if (dot.Checked) {
                lastP = e.Location;
                
                pic.FillEllipse(new SolidBrush(Color.Red), new Rectangle((int)lastP.X, (int)lastP.Y, 4, 4));
            } else if (poly.Checked) {
                PointF oldLP;
                if (pointsArr.Count == 1)
                    oldLP = firstP;
                else
                    oldLP = lastP;
                lastP = e.Location;
                pointsArr.Add(lastP);
                pic.DrawLine(p, oldLP, lastP);
            }
            picArea.Invalidate();
        }

        //Принадлежит ли точка выпуклому многоугольнику
        public void CheckNonConvex(PointF point)
        {
            bool flag = false;

            float x0 = pointsArr[0].X;
            float y0 = pointsArr[0].Y;
            float xD = pointsArr[1].X - x0;
            float yD = pointsArr[1].Y - y0;

            int sign = Math.Sign(yD * (point.X - x0) - xD * (point.Y - y0));
            for (int i = 2; i < pointsArr.Count; i++)
            {
                x0 = pointsArr[i - 1].X;
                y0 = pointsArr[i - 1].Y;
                xD = pointsArr[i].X - x0;
                yD = pointsArr[i].Y - y0;

                if (sign * (yD * (point.X - x0) - xD * (point.Y - y0)) < 0)
                {
                    flag = !flag;
                }
            }
            x0 = pointsArr[pointsArr.Count - 1].X;
            y0 = pointsArr[pointsArr.Count - 1].Y;
            xD = pointsArr[0].X - x0;
            yD = pointsArr[0].Y - y0;

            if (sign * (yD * (point.X - x0) - xD * (point.Y - y0)) < 0)
            {
                flag = !flag;
            }

            if(flag)
               interOut.Text = "Точка не принадлежит невыпуклому многоугольнику";
            else
               interOut.Text = "Точка принадлежит невыпуклому многоугольнику";
        }


        //Принадлежит ли точка невыпуклому многоугольнику
        public void CheckConvex(PointF point)
        {
            float x0 = pointsArr[0].X;
            float y0 = pointsArr[0].Y;
            float xD = pointsArr[1].X - x0;
            float yD = pointsArr[1].Y - y0;

            int sign = Math.Sign(yD * (point.X - x0) - xD * (point.Y - y0));
            for (int i = 2; i < pointsArr.Count; i++)
            {
                x0 = pointsArr[i - 1].X;
                y0 = pointsArr[i - 1].Y;
                xD = pointsArr[i].X - x0;
                yD = pointsArr[i].Y - y0;

                if (sign * (yD * (point.X - x0) - xD * (point.Y - y0)) < 0)
                {
                    interOut.Text = "Точка не принадлежит выпуклому многоугольнику";
                    return;
                }
            }
            x0 = pointsArr[pointsArr.Count - 1].X;
            y0 = pointsArr[pointsArr.Count - 1].Y;
            xD = pointsArr[0].X - x0;
            yD = pointsArr[0].Y - y0;

            if (sign * (yD * (point.X - x0) - xD * (point.Y - y0)) < 0)
            {
                interOut.Text = "Точка не принадлежит выпуклому многоугольнику";
                return;
            }
                interOut.Text = "Точка принадлежит выпуклому многоугольнику";
        }

        
    }
}
