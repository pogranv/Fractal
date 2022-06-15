using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Fractals
{
    /// <summary>
    /// Класс, отвечающий за построение и отрисовку фрактала "Кривая Коха".
    /// </summary>
    class KohaLine : Fractal
    {
        /// <summary>
        /// Конструктор класса. Инициализирует поля базового класса.
        /// </summary>
        /// <param name="canvas">Канвас, на котором будет строиться фрактал.</param>
        /// <param name="deepOfRecursion">Глубина рекурсии.</param>
        public KohaLine(Canvas canvas, int deepOfRecursion) : base(canvas, deepOfRecursion) { }

        /// <summary>
        /// Метод для построения и отрисовки фрактала.
        /// Метод строит 2 крайние точки и вызывает рекурсивный метод построения фрактала.
        /// </summary>
        public override void ToDraw()
        {
            Point leftBorder = new Point(canvas.MinWidth, canvas.Height / 2);
            Point rightPoint = new Point(canvas.Width, canvas.Height / 2);
            GetRecursionImage(deepOfRecursion, leftBorder, rightPoint);
        }

        /// <summary>
        /// Рекурсивный метод построения фрактала.
        /// Принимает 2 крайние точки, строит кривую между ними.
        /// Рекурсивно вызывается от сторон получившейся кривой.
        /// </summary>
        /// <param name="deepOfRecursion">Глубина рекурсии фрактала.</param>
        /// <param name="startSidePoint">Начальная точка прямой.</param>
        /// <param name="endSidePoint">Конечная точка прямой.</param>
        private void GetRecursionImage(int deepOfRecursion, Point startSidePoint, Point endSidePoint)
        {
            if (deepOfRecursion == 0)
            {
                Line newSide = new Line();
                newSide.X1 = startSidePoint.X;
                newSide.Y1 = startSidePoint.Y;
                newSide.X2 = endSidePoint.X;
                newSide.Y2 = endSidePoint.Y;

                newSide.Stroke = Brushes.Red;
                newSide.StrokeThickness = 0.5;

                canvas.Children.Add(newSide);
                return;
            }

            Point oneThirdOfSide = new Point((endSidePoint.X + 2 * startSidePoint.X) / 3, (endSidePoint.Y + 2 * startSidePoint.Y) / 3);
            Point twoThirdOfSide = new Point((2 * endSidePoint.X + startSidePoint.X) / 3, (startSidePoint.Y + 2 * endSidePoint.Y) / 3);
            Point topPointOfTriangle = new Point((oneThirdOfSide.X + twoThirdOfSide.X - (oneThirdOfSide.Y - twoThirdOfSide.Y) * Math.Sqrt(3)) / 2, 
                (oneThirdOfSide.Y + twoThirdOfSide.Y - (twoThirdOfSide.X - oneThirdOfSide.X) * Math.Sqrt(3))/ 2);

            GetRecursionImage(deepOfRecursion - 1, startSidePoint, oneThirdOfSide);
            GetRecursionImage(deepOfRecursion - 1, oneThirdOfSide, topPointOfTriangle);
            GetRecursionImage(deepOfRecursion - 1, topPointOfTriangle, twoThirdOfSide);
            GetRecursionImage(deepOfRecursion - 1, twoThirdOfSide, endSidePoint);
        }
    }
}
