using System;
using System.Collections.Generic;
using System.IO;
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

namespace Лабораторная_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Circle crc;
        static Square sqr;
        static Triangle trg;

        static List<GeomFig> geomFig = new List<GeomFig>();

        public MainWindow()
        {
            InitializeComponent();
            CanvasMain.Background = Brushes.PapayaWhip;
        }

        void Clear()
        {
            CanvasMain.Children.Clear();
            CanvasMain.Background = Brushes.PapayaWhip;
            geomFig = new List<GeomFig>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
           Random rnd = new Random();
            int a = rnd.Next(0, 3);


            crc = new Circle();
            sqr = new Square();
            trg = new Triangle();

            switch (a)
            {
                case 0:
                    geomFig.Add(crc);

                    break;

                case 1:
                    geomFig.Add(sqr);
                    break;

                case 2:
                    geomFig.Add(trg);
                    break;
            }

            CanvasMain.Children.Clear();
            CanvasMain.Background = Brushes.PapayaWhip;

            for (int i = 0; i < geomFig.Count; i++)
                CanvasMain.Children.Add(geomFig[i].Draw());
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            Point p = Mouse.GetPosition(CanvasMain);

            double X = p.X;
            double Y = p.Y;

            for (int i = 0; i < geomFig.Count; i++)
                if (geomFig[i].PointCheck(X, Y)) 
                {
                    CanvasMain.Children.Add(geomFig[i].FigChose());
                }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            string path = @"SavedFigures.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                if (File.Exists(path))
                {
                    for (int i = 0; i < geomFig.Count; i++)
                    {
                        string str;
                        str = geomFig[i].Name + " " + 
                              geomFig[i].PointCenter.X  + " " + 
                              geomFig[i].PointCenter.Y + " " + 
                              geomFig[i].Color.R + " " + 
                              geomFig[i].Color.G + " " + 
                              geomFig[i].Color.B + " " + 
                              geomFig[i].Size; 

                        sw.WriteLine(str);
                    }
                    
                }
               
            }
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            CanvasMain.Children.Clear();
            CanvasMain.Background = Brushes.PapayaWhip;
            geomFig = new List<GeomFig>();
            

            string path = @"SavedFigures.txt";
            if (File.Exists(path))
            {
                string[] str = File.ReadAllLines(path);

                for (int i = 0; i < str.Length; i++)
                {
                    string[] lines = new string[str.Length];
                    lines = str[i].Split(' ');

                    if (lines[0] == crc.Name)
                    {
                        crc = new Circle();
                        Color clr = new Color();
                        clr.R = (byte)Convert.ToInt32(lines[3]);
                        clr.G = (byte)Convert.ToInt32(lines[4]);
                        clr.B = (byte)Convert.ToInt32(lines[5]);
                        clr.A = 255;

                        Name = lines[0];
                        crc.Color = clr;
                        crc.Size = Convert.ToInt32(lines[6]);
                        crc.PointCenter = new Point(Convert.ToInt32(lines[1]), Convert.ToInt32(lines[2]));

                        geomFig.Add(crc);

                    }else if (lines[0] == sqr.Name)
                    {
                        sqr = new Square();

                        Color clr = new Color();
                        clr.R = (byte)Convert.ToInt32(lines[3]);
                        clr.G = (byte)Convert.ToInt32(lines[4]);
                        clr.B = (byte)Convert.ToInt32(lines[5]);
                        clr.A = 255;

                        Name = lines[0];
                        sqr.Color = clr;
                        sqr.Size = Convert.ToInt32(lines[6]);
                        sqr.PointCenter = new Point(Convert.ToInt32(lines[1]), Convert.ToInt32(lines[2]));

                        geomFig.Add(sqr);

                    }
                    else if (lines[0] == trg.Name)
                    {
                        trg = new Triangle();
                        Color clr = new Color();
                        clr.R = (byte)Convert.ToInt32(lines[3]);
                        clr.G = (byte)Convert.ToInt32(lines[4]);
                        clr.B = (byte)Convert.ToInt32(lines[5]);
                        clr.A = 255;

                        Name = lines[0];
                        trg.Color = clr;
                        trg.Size = Convert.ToInt32(lines[6]);
                        trg.PointCenter = new Point(Convert.ToInt32(lines[1]), Convert.ToInt32(lines[2]));

                        geomFig.Add(trg);
                    }


                    /* geomFig[i].Name;
                     geomFig[i].PointCenter.X;
                     geomFig[i].PointCenter.Y;
                     geomFig[i].Color.R;
                     geomFig[i].Color.G;
                     geomFig[i].Color.B;
                     geomFig[i].Size;*/
                }
            }

            for (int i = 0; i < geomFig.Count; i++)
                CanvasMain.Children.Add(geomFig[i].Draw());
        }
    }

    class Circle : GeomFig
    {


        public override Shape Draw()
        {
            Ellipse circle = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush(Color);

            circle.Height = Size * 2;
            circle.Width = Size * 2;
            circle.SetValue(Canvas.LeftProperty, PointCenter.X);
            circle.SetValue(Canvas.TopProperty, PointCenter.Y * -1);
            circle.Stroke = brush;
            circle.Fill = brush;

            return circle;
        }

        public override int Аrea()
        {
            double area = Math.PI * Size * Size;
            return Convert.ToInt32(area);
        }

        public override bool PointCheck(double x, double y)
        {
            if (Math.Pow(Size, 2) >= (Math.Pow(PointCenter.X + Size - x, 2) + Math.Pow(-1 * PointCenter.Y + Size - y, 2)))
                return true;

            return false;
        }

        public override Label FigChose()
        {
            Label _ = new Label
            {
                Content = $"Area of {Name}: {this.Аrea()}",
            };
            _.SetValue(Canvas.LeftProperty, PointCenter.X + Size * 1 / 9);
            _.SetValue(Canvas.TopProperty, PointCenter.Y * -1 + Size * 5 / 6 );

            return _;
        }

        public Circle()
        {
            Random rnd = new Random();
            Color clr = new Color();
            clr.R = (byte)rnd.Next(0, 255);
            clr.G = (byte)rnd.Next(1, 255);
            clr.B = (byte)rnd.Next(2, 255);
            clr.A = 255;

            Name = "Circle";
            Color = clr;
            Size = rnd.Next(50, 100);
            PointCenter = new Point(rnd.Next(0, 800), rnd.Next(-400, 0));
        }
    }

    class Square : GeomFig
    {


        public override Shape Draw()
        {
            Rectangle square = new Rectangle();
            SolidColorBrush brush = new SolidColorBrush(Color);

            Canvas.SetLeft(square, PointCenter.X);
            Canvas.SetTop(square, PointCenter.Y * -1);

            square.Height = Size;
            square.Width = Size;
            square.Stroke = brush;
            square.Fill = brush;



            return square;
        }

        public override int Аrea()
        {
            double area = Size * Size;
            return Convert.ToInt32(area);
        }

        public override bool PointCheck(double x, double y)
        {
            if ((x >= PointCenter.X) && (y >= PointCenter.Y * -1) && (x <= PointCenter.X + Size) && (y <= PointCenter.Y * -1 + Size))
                return true;

            return false;
        }

        public override Label FigChose()
        {
            Label _ = new Label
            {
                Content = $"Area of {Name}: {this.Аrea()}",
            };
            _.SetValue(Canvas.LeftProperty, PointCenter.X + Size * 1 / 9 );
            _.SetValue(Canvas.TopProperty, PointCenter.Y * -1 + Size * 3 / 7);

            return _;
        }

        public Square()
        {
            Random rnd = new Random();
            Color clr = new Color();
            clr.R = (byte)rnd.Next(0, 255);
            clr.G = (byte)rnd.Next(1, 255);
            clr.B = (byte)rnd.Next(2, 255);
            clr.A = 255;

            Name = "Square";
            Color = clr;
            Size = rnd.Next(150, 200);
            PointCenter = new Point(rnd.Next(0, 800), rnd.Next(-400, 0));
        }
    }

    class Triangle : GeomFig
    {


        public override Shape Draw()
        {
            Random rnd = new Random();
            Polyline myPolygon = new Polyline();
            SolidColorBrush brush = new SolidColorBrush(Color);
            PointCollection points = new PointCollection();

            myPolygon.Stroke = brush;
            myPolygon.Fill = brush;
            myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;


            

            points.Add(new Point(PointCenter.X, -1 * PointCenter.Y));
            points.Add(new Point(PointCenter.X + Size / 2, -1 * PointCenter.Y + Size));
            points.Add(new Point(PointCenter.X - Size / 2, -1 * PointCenter.Y  + Size ));

            myPolygon.Points = points;



            return myPolygon;
        }

        public override int Аrea()
        {
            double area = Size * Size / 2;
            return Convert.ToInt32(area);
        }

        public override bool PointCheck(double x, double y)
        {
            double xA = PointCenter.X;
            double yA = -1 * PointCenter.Y;

            double xB = PointCenter.X + Size;
            double yB = -1 * PointCenter.Y + Size / 2;

            double xC = PointCenter.X - Size;
            double yC = -1 * PointCenter.Y + Size / 2;

            bool first =  0 >= (xA - x) * (yB - yA) - (xB - xA) * (yA - y);
            bool second = 0 >= (xB - x) * (yC - yB) - (xC - xB) * (yB - y);
            bool third =  0 >= (xC - x) * (yA - yC) - (xA - xC) * (yC - y);

            if (((first)&&(second)&&(third))||((!first) && (!second) && (!third)))
            {
                return true;
            }


            return false;
        }

        public override Label FigChose()
        {
            Label _ = new Label
            {
                Content = $"Area of {Name}: {this.Аrea()}",
            };
            _.SetValue(Canvas.LeftProperty, PointCenter.X - Size / 2);
            _.SetValue(Canvas.TopProperty, -1 * PointCenter.Y + Size * 2 / 7);

            return _;
        }

        public Triangle()
        {
            Random rnd = new Random();
            Color clr = new Color();
            clr.R = (byte)rnd.Next(0, 255);
            clr.G = (byte)rnd.Next(1, 255);
            clr.B = (byte)rnd.Next(2, 255);
            clr.A = 255;

            Name = "Triangle";
            Color = clr;
            Size = rnd.Next(100, 150);
            PointCenter = new Point(rnd.Next(100, 800), rnd.Next(-400, 0));
        }
    }
}
