using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс для построения и отрисовки фрактала треугольников.
    /// </summary>
    class Triangle : Fractal
    {
        /// <summary>
        /// Конструктор класса. Инициализирует поля с помощью базового класса.
        /// </summary>
        /// <param name="canvas">Канвас, на котором будет строиться фрактал.</param>
        /// <param name="deepOfRecursion">Глубина рекурсии.</param>
        public Triangle(Canvas canvas, int deepOfRecursion): base(canvas, deepOfRecursion) { }

        /// <summary>
        /// Метод определяет 3 начальные точки треугольника.
        /// Вызывает рекурсивную функцию построения меньших треугольников.
        /// </summary>
        public override void ToDraw()
        {
            Point bottomLeftPoint = new Point(canvas.MinWidth, canvas.Height);
            Point topPoint = new Point(canvas.Width / 2, canvas.MinHeight);
            Point bottomRightPoint = new Point(canvas.Width, canvas.Height);
            GetRecursionImage(deepOfRecursion, bottomLeftPoint, topPoint, bottomRightPoint);
        }
        
        /// <summary>
        /// Рекурсивная функция построения треугольников.
        /// Функция строит треугольник по трем заданным точкам.
        /// Находит координаты середин сторон текущего треугольника и
        /// рекурсивно запускает себя 3 раза с этими точками.
        /// </summary>
        /// <param name="deepOfRecursion">Глубина рекурсии.</param>
        /// <param name="bottomLeftPoint">Нижняя левая точка треугольника.</param>
        /// <param name="topPoint">Верхняя точка треугольника.</param>
        /// <param name="bottomRightPoint">Нижняя правая точка треугольника.</param>
        private void GetRecursionImage(int deepOfRecursion, Point bottomLeftPoint, Point topPoint, Point bottomRightPoint)
        {
            if (deepOfRecursion == 0)
            {
                return;
            }

            Polygon newTriangle = new Polygon();

            newTriangle.Points.Add(bottomLeftPoint);
            newTriangle.Points.Add(topPoint);
            newTriangle.Points.Add(bottomRightPoint);

            newTriangle.Stroke = System.Windows.Media.Brushes.Red;

            canvas.Children.Add(newTriangle);

            Point centerPointOfLeftSide = new Point((bottomLeftPoint.X + topPoint.X) / 2, (bottomLeftPoint.Y + topPoint.Y) / 2);
            Point centerPointOfRightSide = new Point((bottomRightPoint.X + topPoint.X) / 2, (bottomRightPoint.Y + topPoint.Y) / 2);
            Point centerPointOfBottomSide = new Point((bottomRightPoint.X + bottomLeftPoint.X) / 2, (bottomRightPoint.Y + bottomLeftPoint.Y) / 2);

            GetRecursionImage(deepOfRecursion - 1, bottomLeftPoint, centerPointOfLeftSide, centerPointOfBottomSide);
            GetRecursionImage(deepOfRecursion - 1, centerPointOfLeftSide, topPoint, centerPointOfRightSide);
            GetRecursionImage(deepOfRecursion - 1, centerPointOfBottomSide, centerPointOfRightSide, bottomRightPoint);
        }
    }
}
