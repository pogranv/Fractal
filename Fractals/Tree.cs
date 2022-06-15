using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Fractals
{
    /// <summary>
    /// Класс, отвечающий за построение и зарисовку фрактала 
    /// "Обдуваемое фрактальное дерево".
    /// </summary>
    class Tree : Fractal
    {
        /// <summary>
        /// Угол, на который будет повернут каждый левый отрезок.
        /// </summary>
        int angleOfSegment1;
        /// <summary>
        /// Угол, на который будет повернут каждый правый отрезок.
        /// </summary>
        int angleOfSegment2;
        /// <summary>
        /// Отношение длин отрезков каждой итерации.
        /// </summary>
        double proportion;
        /// <summary>
        /// Длина первого отрезка дерева.
        /// </summary>
        int lenghtOfFirstSegment = 80;

        /// <summary>
        /// Конструктор класса. Инициализирует необходимые переменные.
        /// </summary>
        /// <param name="canvas">Канвас, на котором строится дерево.</param>
        /// <param name="deepOfRecursion">Глубина рекурсии.</param>
        /// <param name="angleOfSegment1">Угол, на который будет повернут каждый левый отрезок.</param>
        /// <param name="angleOfSegment2">Угол, на который будет повернут каждый правый отрезок.</param>
        /// <param name="proportion">Отношение длин отрезков каждой итерации.</param>
        public Tree(Canvas canvas, int deepOfRecursion, int angleOfSegment1, int angleOfSegment2, double proportion) : base(canvas, deepOfRecursion)
        {
            this.angleOfSegment1 = angleOfSegment1;
            this.angleOfSegment2 = angleOfSegment2;
            this.proportion = proportion;
        }

        /// <summary>
        /// Переопределенный метод отрисовки фрактала.
        /// Определяет стартовую точку дерева и вызывает вспомогательный
        /// метод для построения фрактала.
        /// </summary>
        public override void ToDraw()
        {
            Point startPoint = new Point(canvas.Width / 2, canvas.Height - canvas.Height / 10);
            GetRecursionTree(deepOfRecursion, startPoint, lenghtOfFirstSegment, -90, -90);
        }

        /// <summary>
        /// Рекурсивный метод построения фрактального дерева.
        /// От прошлой точки откладываются 2 отрезка под
        /// заданными углами и с заданной пропорциональностью отрезков.
        /// От концов этих отрезков снова вызывается рекурсивный метод.
        /// </summary>
        /// <param name="deepOfRecursion">Глебина рекурсии.</param>
        /// <param name="lastPoint">Прошлая точка.</param>
        /// <param name="lengthOfCurrentSegment">Длина текущего отрезка.</param>
        /// <param name="currentAngleOfSegment1">Угол, на который будет повернут каждый левый отрезок.</param>
        /// <param name="currentAngleOfSegment2">Угол, на который будет повернут каждый правый отрезок.</param>
        private void GetRecursionTree(int deepOfRecursion, Point lastPoint, double lengthOfCurrentSegment,
            int currentAngleOfSegment1, int currentAngleOfSegment2)
        {
            if (deepOfRecursion == 0)
            {
                return;
            }

            Line leftLine = GetLine(lastPoint, lengthOfCurrentSegment, currentAngleOfSegment1);
            Line rightLine = GetLine(lastPoint, lengthOfCurrentSegment, currentAngleOfSegment2);

            canvas.Children.Add(leftLine);
            canvas.Children.Add(rightLine);

            GetRecursionTree(deepOfRecursion - 1, new Point(leftLine.X2, leftLine.Y2), lengthOfCurrentSegment * proportion,
                currentAngleOfSegment1 + this.angleOfSegment1, currentAngleOfSegment1 - this.angleOfSegment2);

            GetRecursionTree(deepOfRecursion - 1, new Point(rightLine.X2, rightLine.Y2), lengthOfCurrentSegment * proportion,
                currentAngleOfSegment2 + this.angleOfSegment1, currentAngleOfSegment2 - this.angleOfSegment2);
        }

        /// <summary>
        /// Метод строит отрезок от заданной точки с заданным углом и длиной.
        /// </summary>
        /// <param name="lastPoint">Прошлая точка.</param>
        /// <param name="length">Длина нового отрезка.</param>
        /// <param name="angle">Угол, под которым откладывается новый отрезок
        /// от прошлой точки относительно оси X экрана.</param>
        /// <returns></returns>
        private Line GetLine(Point lastPoint, double length, int angle)
        {
            angle = angle - (angle / 360) * 360;
            double nextX = lastPoint.X + length * Math.Cos(angle * Math.PI / 180.0);
            double nextY = lastPoint.Y + length * Math.Sin(angle * Math.PI / 180.0);

            Line line = new Line();
            line.Stroke = Brushes.Red;
            line.StrokeThickness = 0.5;
            line.X1 = lastPoint.X;
            line.Y1 = lastPoint.Y;
            line.X2 = nextX;
            line.Y2 = nextY;
            return line;
        }


    }
}
