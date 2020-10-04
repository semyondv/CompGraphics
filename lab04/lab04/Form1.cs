using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab04
{
    public partial class Form1 : Form
    {
        Graphics pic;
        List<PointF> pointsArr;
        PointF firstP, lastP;

        public Form1()
        {
            InitializeComponent();
            pointsArr = new List<PointF>();
            picArea.Image  = new Bitmap(picArea.Width, picArea.Height);
            pic = Graphics.FromImage(picArea.Image);
            pic.Clear(Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            picArea.Image = new Bitmap(picArea.Width, picArea.Height);
            pic = Graphics.FromImage(picArea.Image);
            pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
            pointsArr.Clear();

            //can_draw = true;

            //first = true;
        }

        private void picArea_MouseDown(object sender, MouseEventArgs e)
        {
            firstP = e.Location;
            pointsArr.Add(firstP);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (poly.Checked)
            {
                if (polyAcions.SelectedIndex == 0)
                {
                    for (int i = 0; i < pointsArr.Count; i++)
                        pointsArr[i] = AT.Move(pointsArr[i], (int)dx.Value, (int)dy.Value);
                    pic.FillRectangle(new SolidBrush(Color.White), 0, 0, picArea.Width, picArea.Height);
                    for (int i = 0; i < pointsArr.Count - 1; i++)
                        pic.DrawLine(new Pen(Color.Black), pointsArr[i], pointsArr[i + 1]);
                }
            }
            else if(polyAcions.SelectedIndex == 1)
            {

            }
            picArea.Invalidate();
        }

        private void picArea_MouseUp(object sender, MouseEventArgs e)
        {
            Pen p = new Pen(Color.Black);
            if (line.Checked)
            {
                lastP = e.Location;
                pointsArr.Add(lastP);
                pic.DrawLine(p, firstP, lastP);
                if (lineActions.SelectedIndex == 0)
                {
                    PointF inter;

                    if (pointsArr.Count > 3)
                    {
                        inter = AT.Intersection(pointsArr[pointsArr.Count - 4],
                            pointsArr[pointsArr.Count - 3], 
                            pointsArr[pointsArr.Count - 2], 
                            pointsArr[pointsArr.Count - 1]);
                    }
                    else
                        inter = new PointF(float.MaxValue, float.MaxValue);

                    if (inter.X == float.MaxValue && inter.Y == float.MaxValue)
                        interOut.Text = "Нет пересечения";
                    else
                        interOut.Text = $"X: {inter.X} Y: {inter.Y}";
 
                }                
            }
            else if (dot.Checked)
            {
                lastP = e.Location;
                pic.FillEllipse(new SolidBrush(Color.Red), new Rectangle((int)lastP.X, (int)lastP.Y, 4, 4));
            }
            else if (poly.Checked)
            {
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
    }
}
