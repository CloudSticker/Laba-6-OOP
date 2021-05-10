using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Лабораторная_6
{
    public abstract class GeomFig
    {
        private Point pointCenter;
        public Point PointCenter{ get; set; }

        private Color color;
        public Color Color { get; set; }

        private double size;
        public double Size { get; set; }

        private string name;
        public string Name { get; set; }

        public abstract int Аrea(); 

        public abstract Shape Draw();

        public abstract bool PointCheck(double x, double y);

        public abstract Label FigChose();

    }
}
