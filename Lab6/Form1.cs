using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab6 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
            g.Clear(Color.White);
        }
        Graphics g;
        Figure3D p;

        private void draw_btn_Click(object sender, EventArgs e) {
            g.Clear(Color.White);
            if (radioButton1.Checked) {
                p = new Tetrahedron();
            } else if (radioButton2.Checked) {
                p = new Hexahedron();
            } else if (radioButton3.Checked) {
                p = new Octahedron();
            }
            p.Draw(g);
        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void transform_Click(object sender, EventArgs e) {
            g.Clear(Color.White);

            p.Move((float)numericMoveX.Value, (float)numericMoveY.Value, (float)numericMoveZ.Value);
            p.Scaling((float)numericScaleX.Value, (float)numericScaleY.Value, (float)numericScaleZ.Value);
            
            p.Draw(g);
        }

        private void rotationValueX_ValueChanged(object sender, EventArgs e) {
            g.Clear(Color.White);

            p.RotateX(rotationValueX.Value);
            p.Draw(g);
        }

        private void rotationValueY_ValueChanged(object sender, EventArgs e) {
            g.Clear(Color.White);

            p.RotateY(rotationValueY.Value);
            p.Draw(g);
        }

        private void rotationValueZ_ValueChanged(object sender, EventArgs e) {
            g.Clear(Color.White);

            p.RotateZ(rotationValueZ.Value);
            p.Draw(g);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            g.Clear(Color.White);
            if (comboBox1.SelectedIndex == 0) {
                p.ReflectX();
            } else if (comboBox1.SelectedIndex == 1) {
                p.ReflectY();
            } else if (comboBox1.SelectedIndex == 2) {
                p.ReflectZ();
            }
            p.Draw(g);
        }

        private void button1_Click(object sender, EventArgs e) {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            StreamWriter writer = new StreamWriter(File.OpenWrite(saveFileDialog1.FileName));
            foreach (var side in p.Sides) {
                foreach (var pt in side.Points) {
                    writer.Write(pt.X);
                    writer.Write(" ");
                    writer.Write(pt.Y);
                    writer.Write(" ");
                    writer.Write(pt.Z);
                    writer.Write(" ");
                }
                writer.WriteLine("");
            }

            writer.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fileText = File.ReadAllText(openFileDialog1.FileName);
            Figure3D figure = new Figure3D();
            figure.Sides = new List<Figure>();

            var rows = fileText.Split('\n');
            foreach (string row in rows) {
                string[] strPoints = row.Split(' ');
                List<PointXYZ> pts = new List<PointXYZ>();

                if (strPoints.Length > 0) {
                    for (int i = 0; i < strPoints.Length - 1; i += 3) {
                        float x, y, z;
                        x = (float)Convert.ToDouble(strPoints[i]);
                        y = (float)Convert.ToDouble(strPoints[i + 1]);
                        z = (float)Convert.ToDouble(strPoints[i + 2]);

                        pts.Add(new PointXYZ(x, y, z));

                    }
                    figure.Sides.Add(new Figure(pts));
                    pts.Clear();
                }
                
            }
            figure.UpdateCenter();
            g.Clear(Color.White);
            p = figure;
            p.Draw(g);
        }
    }
}
