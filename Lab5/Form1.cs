using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
        }

        Graphics g;
        Point firstP = Point.Empty;
        Point secondP = Point.Empty;
        
        private void button1_Click(object sender, EventArgs e) {
            if (firstP != Point.Empty && secondP != Point.Empty) {
                DiamondSquareAsync(firstP, secondP, (double)k_box.Value / 10.0);
            }
        }

        void RemoveLine(Point p1, Point p2) {
            g.DrawLine(new Pen(Color.White), p1, p2);
        }
        private async void DiamondSquareAsync(Point l, Point r, double k) {
            if (Math.Abs(l.X - r.X) == 1) 
                return;

            double length = Math.Sqrt((l.X - r.X) * (l.X - r.X) + (l.Y - r.Y) * (l.Y - r.Y));
            Random rand = new Random((int)length);

            int y = (l.Y + r.Y) / 2 + rand.Next((int)(-k * length), (int)(k * length));
            Point h = new Point((l.X + r.X) / 2, y);

            RemoveLine(l, r);
            g.DrawLine(new Pen(Color.Black), l, h);
            g.DrawLine(new Pen(Color.Black), h, r);

            pictureBox1.Invalidate();

            await Task.Delay((int)delay_box.Value);
            //System.Threading.Thread.Sleep(1);
            DiamondSquareAsync(l, h, k);
            DiamondSquareAsync(h, r, k);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            firstP = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            secondP = e.Location;

            g.DrawLine(new Pen(Color.Black), firstP, secondP);
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e) {
            g.Clear(Color.White);
            firstP = Point.Empty;
            secondP = Point.Empty;

            pictureBox1.Invalidate();
        }
    }
}
