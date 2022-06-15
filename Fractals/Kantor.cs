using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Fractals
{
    /// <summary>
    /// Класс, отвечающий за построение и 
    /// зарисовку фрактала "множество Кантора"
    /// </summary>
    class Kantor : Fractal
    {
        /// <summary>
        /// Расстояние между прямыми множества Кантора.
        /// </summary>
        int distanceBetweenLines;

        /// <summary>
        /// Конструктор класса. Инициализирует необходимые поля класса.
        /// </summary>
        /// <param name="canvas">Канвас, на котором строится фрактал.</param>
        /// <param name="deep">Глубина рекурсии.</param>
        /// <param name="distanceBetweenLines">Расстояние между прямыми множества Кантора.</param>
        public Kantor(Canvas canvas, int deep, int distanceBetweenLines) : base(canvas, deep)
        {
            this.distanceBetweenLines = distanceBetweenLines;
        }

        /// <summary>
        /// Переопределенный метод для рисования фрактала.
        /// Вызывает вспомогательный метод для построения самого фрактала.
        /// </summary>
        public override void ToDraw()
        {

            GetRecursionKantor(deepOfRecursion, new Point(0, 30), new Point(canvas.Width, 30));
        }

        /// <summary>
        /// Метод рекурсивно строит линии множества Кантора.
        /// В методе текущая прямая делится на 3 части, 
        /// 2 крайние закрашиваются и от них вызывается
        /// снова рекурсивный метод.
        /// </summary>
        /// <param name="deepOfRecursion">Глубина рекурсии.</param>
        /// <param name="leftPoint">Левая граница текущей прямой.</param>
        /// <param name="rightPoint">Правая граница текущей прямой.</param>
        private void GetRecursionKantor(int deepOfRecursion, Point leftPoint, Point rightPoint)
        {
            if (deepOfRecursion == 0)
            {
                return;
            }

            Line currentLine = new Line();
            currentLine.X1 = leftPoint.X;
            currentLine.Y1 = leftPoint.Y;
            currentLine.X2 = rightPoint.X;
            currentLine.Y2 = rightPoint.Y;

            currentLine.Stroke = Brushes.Red;
            currentLine.StrokeThickness = 10;

            canvas.Children.Add(currentLine);
            double lengthOfCurrentLine = rightPoint.X - leftPoint.X;

            Point leftBorder = new Point(leftPoint.X + lengthOfCurrentLine / 3, leftPoint.Y + distanceBetweenLines);
            Point rightBorder = new Point(leftPoint.X + 2 * lengthOfCurrentLine / 3, rightPoint.Y + distanceBetweenLines);
            leftPoint.Y += distanceBetweenLines;
            rightPoint.Y += distanceBetweenLines;
            GetRecursionKantor(deepOfRecursion - 1, rightBorder, rightPoint);
            GetRecursionKantor(deepOfRecursion - 1, leftPoint, leftBorder);

        }
    }
}
