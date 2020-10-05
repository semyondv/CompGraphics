using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cg_lab3_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            image = new Bitmap(@"D:\Projects\CG\cg_lab3\pic5.png");
        }

        Bitmap image;
        System.Drawing.Color shellColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
        private bool EqualShellColor(System.Drawing.Color c)
        {
            return (c.R == shellColor.R && c.G == shellColor.G && c.B == shellColor.B);
        }

        private void picture_Loaded(object sender, RoutedEventArgs e)
        {
            picture.Source = GetBitmapSource(image);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (int, int) pos = FindFirstPixel();
            ICollection<(int, int)> points = GetBorderPixels(pos.Item1, pos.Item2);
            foreach (var p in points)
            {
                image.SetPixel(p.Item1, p.Item2, System.Drawing.Color.Red);
                outText.Text += $"({p.Item1}, {p.Item2}) ";
            }
            picture.Source = GetBitmapSource(image);
        }
        private (int, int) FindFirstPixel()
        {
            int x = 0, y = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    System.Drawing.Color color = image.GetPixel(i, j);
                    if (EqualShellColor(color))
                    {
                        x = i;
                        y = j;
                        goto ret;
                    }
                }
            }
            ret:
            return (x, y);
        }

        private static BitmapSource GetBitmapSource(Bitmap b)
        {
            var ScreenCapture = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            b.GetHbitmap(),
            IntPtr.Zero,
            System.Windows.Int32Rect.Empty,
            BitmapSizeOptions.FromWidthAndHeight(b.Width, b.Height));

            return ScreenCapture;
        }

        private ICollection<(int, int)> GetBorderPixels(int x, int y) {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            Queue<(int, int)> notVisited = new Queue<(int, int)>();
            
            System.Drawing.Color c = image.GetPixel(x, y);
            int direction = 5;
            int curX = 0;
            int curY = 0;
            notVisited.Enqueue((x, y));
            visited.Add((x, y));
            while (notVisited.Count != 0) {
                curX = notVisited.Peek().Item1;
                curY = notVisited.Peek().Item2;
                notVisited.Dequeue();

                for (int i = 0; i < 8; i++) {
                    (int, int) point = PixelNear(((direction - i) + 8) % 8, curX, curY);
                    if (image.GetPixel(point.Item1, point.Item2) == c) {
                        if (!visited.Contains((point.Item1, point.Item2))) {
                            direction = (direction + i + 2) % 8;
                            notVisited.Enqueue((point.Item1, point.Item2));
                            visited.Add((point.Item1, point.Item2));
                            break;
                        }
                    }
                }
            }
            return visited;
        }
        //0 1 2
        //7 x 3
        //6 5 4
        private (int, int) PixelNear(int dir, int x, int y) {
            switch (dir) {
                case 0:
                    return (InLimit(picture.Source.Width, x - 1), InLimit(picture.Source.Height, y - 1));
                case 1:
                    return (InLimit(picture.Source.Width, x), InLimit(picture.Source.Height, y - 1));
                case 2:
                    return (InLimit(picture.Source.Width, x + 1), InLimit(picture.Source.Height, y - 1));
                case 3:
                    return (InLimit(picture.Source.Width, x + 1), InLimit(picture.Source.Height, y));
                case 4:
                    return (InLimit(picture.Source.Width, x + 1), InLimit(picture.Source.Height, y + 1));
                case 5:
                    return (InLimit(picture.Source.Width, x), InLimit(picture.Source.Height, y + 1));
                case 6:
                    return (InLimit(picture.Source.Width, x - 1), InLimit(picture.Source.Height, y + 1));
                case 7:
                    return (InLimit(picture.Source.Width, x - 1), InLimit(picture.Source.Height, y));
                default:
                    return (InLimit(picture.Source.Width, x), InLimit(picture.Source.Height, y - 1));
            }
        }
        private int InLimit(double border, double x) => (int)Math.Max(0, Math.Min(border - 1, x));

    }
}
