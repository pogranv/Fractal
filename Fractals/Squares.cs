using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Fractals
{
    /// <summary>
    /// Класс, отвечающий за построение и отрисовку фрактала "Ковер Серпинского".
    /// </summary>
    class Squares : Fractal
    {
        /// <summary>
        /// Конструктор класса. Инициализирует поля базового класса.
        /// </summary>
        /// <param name="canvas">Канвас, на котором будет строиться фрактал.</param>
        /// <param name="deep">Глубина рекурсии фрактала.</param>
        public Squares(Canvas canvas, int deep) : base(canvas, deep) { }

        /// <summary>
        /// Метод, отвечающий за отрисовку фрактала. Строит 4 точки
        /// первого центрального фрактала и запускает рекурсивный алгоритм
        /// построения фрактала.
        /// </summary>
        public override void ToDraw()
        {
            Point a = new Point(canvas.MinWidth, canvas.MinHeight);
            Point b = new Point(canvas.Width, canvas.MinHeight);
            Point c = new Point(canvas.Width, canvas.Height);
            Point d = new Point(canvas.MinWidth, canvas.Height);
            
            DrowRecurcionSquares(deepOfRecursion, a, c);
        }

        /// <summary>
        /// Рекурсивный метод построения фрактала. Метод определяет 4
        /// точки центрального квадрата, закрашивает его и вызывает 
        /// себя 8 раз для каждого квадратика.
        /// </summary>
        /// <param name="deep">Глубина рекурсии.</param>
        /// <param name="topLeftPoint">Левая верхняя точка текущего квадрата.</param>
        /// <param name="bottomRightPoint">Правая нижняя точка текущего квадрата.</param>
        private void DrowRecurcionSquares(int deep, Point topLeftPoint, Point bottomRightPoint)
        {
            if (deep == 0)
            {
                return;
            }
            double lenSideOfSquare = bottomRightPoint.X - topLeftPoint.X;
            double lenSideOfCenterSquare = lenSideOfSquare / 3;
            Point topLeftPointOfCenterSquare = new Point(topLeftPoint.X + lenSideOfSquare / 3, topLeftPoint.Y + lenSideOfSquare / 3); ;
            Point bottomRightPointOfCenterSquare = new Point(topLeftPointOfCenterSquare.X + lenSideOfCenterSquare,
                topLeftPointOfCenterSquare.Y + lenSideOfCenterSquare);
            Point topRightPointOfCenterSquare = new Point(bottomRightPointOfCenterSquare.X,
                topLeftPointOfCenterSquare.Y);
            Point bottomLeftPointOfCenterSquare = new Point(topLeftPointOfCenterSquare.X,
                bottomRightPointOfCenterSquare.Y);
            Polygon square = new Polygon();
            square.Points.Add(topLeftPointOfCenterSquare);
            square.Points.Add(topRightPointOfCenterSquare);
            square.Points.Add(bottomRightPointOfCenterSquare);
            square.Points.Add(bottomLeftPointOfCenterSquare);
            square.Stroke = Brushes.Red;
            square.Fill = Brushes.Red;
            canvas.Children.Add(square);
            DrowRecurcionSquares(deep - 1, topLeftPoint, topLeftPointOfCenterSquare);
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X,topLeftPoint.Y + lenSideOfCenterSquare), 
                bottomLeftPointOfCenterSquare);
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X, topLeftPoint.Y + 2 * lenSideOfCenterSquare),
                new Point(bottomLeftPointOfCenterSquare.X,bottomLeftPointOfCenterSquare.Y+lenSideOfCenterSquare));
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X, topLeftPoint.Y + 2 * lenSideOfCenterSquare),
                new Point(bottomLeftPointOfCenterSquare.X, bottomLeftPointOfCenterSquare.Y + lenSideOfCenterSquare));
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X + lenSideOfCenterSquare, topLeftPoint.Y),
                topRightPointOfCenterSquare);
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X + lenSideOfCenterSquare, topLeftPoint.Y + 2*lenSideOfCenterSquare),
                new Point(bottomRightPointOfCenterSquare.X, bottomRightPointOfCenterSquare.Y + lenSideOfCenterSquare));
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X + 2*lenSideOfCenterSquare, topLeftPoint.Y),
                new Point(topRightPointOfCenterSquare.X+lenSideOfCenterSquare, topRightPointOfCenterSquare.Y));
            DrowRecurcionSquares(deep - 1, new Point(topLeftPoint.X + 2 * lenSideOfCenterSquare, topLeftPoint.Y+lenSideOfCenterSquare),
                new Point(bottomRightPointOfCenterSquare.X+lenSideOfCenterSquare, bottomRightPointOfCenterSquare.Y));
            DrowRecurcionSquares(deep - 1, bottomRightPointOfCenterSquare, bottomRightPoint);
            return;
        }
    }
}
