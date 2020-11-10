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

            g1 = pictureBox2.CreateGraphics();
            g1.TranslateTransform(pictureBox2.ClientSize.Width / 2, pictureBox2.ClientSize.Height / 2);
            g1.ScaleTransform(1, -1);
            g1.Clear(Color.White);
        }
        Graphics g;
        Graphics g1;
        Figure3D p;
        Figure3D cam_p;

        private void draw_btn_Click(object sender, EventArgs e) {
            g.Clear(Color.White);
            g1.Clear(Color.White);
            if (radioButton1.Checked) {
                p = new Tetrahedron();
            } else if (radioButton2.Checked) {
                p = new Hexahedron();
            } else if (radioButton3.Checked) {
                p = new Octahedron();
            }

            cam_p = p.DeepCopy();
            p.Draw(g);
            DrawCameraFigure();
        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void DrawCameraFigure() {
            g1.Clear(Color.White);
            cam_p = p.DeepCopy();

            cam_p.Move((float)camTranslationX.Value, (float)camTranslationY.Value, (float)camTranslationZ.Value);
            cam_p.RotateX(-(float)camRotationX.Value);
            cam_p.RotateY(-(float)camRotationY.Value);
            cam_p.RotateZ(-(float)camRotationZ.Value);

            cam_p.Draw(g1);

            //cam_p.RotateZ((float)camRotationZ.Value);
            //cam_p.RotateY((float)camRotationY.Value);
            //cam_p.RotateX((float)camRotationX.Value);
            //cam_p.Move(-(float)camTranslationX.Value, -(float)camTranslationY.Value, -(float)camTranslationZ.Value);
        }

        private void transform_Click(object sender, EventArgs e) {
            g.Clear(Color.White);
            g1.Clear(Color.White);

            p.Move((float)numericMoveX.Value, (float)numericMoveY.Value, (float)numericMoveZ.Value);
            p.Scaling((float)numericScaleX.Value, (float)numericScaleY.Value, (float)numericScaleZ.Value);
            
            p.Draw(g);
            DrawCameraFigure();
        }

        private void rotationValueX_ValueChanged(object sender, EventArgs e) {
            g.Clear(Color.White);
            g1.Clear(Color.White);

            p.RotateX(rotationValueX.Value);
            p.Draw(g);
            DrawCameraFigure();
        }

        private void rotationValueY_ValueChanged(object sender, EventArgs e) {
            g.Clear(Color.White);
            g1.Clear(Color.White);

            p.RotateY(rotationValueY.Value);
            p.Draw(g);
            DrawCameraFigure();
        }

        private void rotationValueZ_ValueChanged(object sender, EventArgs e) {
            g.Clear(Color.White);
            g1.Clear(Color.White);

            p.RotateZ(rotationValueZ.Value);
            p.Draw(g);
            DrawCameraFigure();
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
            DrawCameraFigure();
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
            string data = File.ReadAllText(openFileDialog1.FileName);
            Figure3D figure = new Figure3D();
            figure.Sides = new List<Figure>();

            var rows = data.Split('\n');
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
            g1.Clear(Color.White);
            p = figure;
            cam_p = p.DeepCopy();
            p.Draw(g);
            DrawCameraFigure();
        }

        private void button3_Click(object sender, EventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}
