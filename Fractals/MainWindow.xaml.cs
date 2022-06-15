using System;
using System.Windows;
using System.Windows.Controls;

namespace Fractals
{
    /// <summary>
    /// Класс, отвечающий за поведение главного окна.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор класса. Инициализирует все компоненты главного окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик любых событий, клоторые приходят 
        /// с манипуляций любых компонентов главного окна.
        /// Инифиализирует значения со всех компонентов окна,
        /// и перерисовывает фрактал в зависимости от выбора фрактала
        /// в комбобоксе. 
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Информация о событии.</param>
        private void RenderFractal(object sender, RoutedEventArgs e)
        {
            canvas1.Children.Clear();
            int recursionDeep = 0;
            double koefOfSegments = 0.5;
            int angleOfFirstSegment = 0;
            int angleOfSecondSegment = 0;
            int distanceBetweenLines = 0;
            try 
            {
                koefOfSegments = koefOfSegmentsSlider.Value;
                recursionDeep = (int)recursionDeepSlider.Value;
                angleOfFirstSegment = (int)firstAngleSlider.Value;
                angleOfSecondSegment = (int)secondAngleSlider.Value;
                distanceBetweenLines = (int)distanceBetweenLinesSlider.Value;
            } 
            catch (Exception ex)
            {
                return;
            }
            try
            {
                switch (((ComboBoxItem)comboBox.SelectedItem).Content)
                {
                    case "Фрактальное дерево":
                        GetTree(recursionDeep, angleOfSecondSegment, angleOfFirstSegment, koefOfSegments);
                        break;
                    case "Кривая Коха":
                        GetKohaLine(recursionDeep);
                        break;
                    case "Ковер Серпинского":
                        GetSquares(recursionDeep);
                        break;
                    case "Треугольник Серпинского":
                        GetTriangles(recursionDeep);
                        break;
                    case "Множество Кантора":
                        GetKantor(recursionDeep, distanceBetweenLines);
                        break;
                    default: return;
                }
            }
            catch (Exception ex)
            {
                GetErrorWindow();
            }
        }

        /// <summary>
        /// Отвечает за появление предупреждающуего окна,
        /// если глубина рекурсии слишком большая.
        /// </summary>
        void GetErrorWindow()
        {
            recursionDeepSlider.Value = 1;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxButton button = MessageBoxButton.OK;
            string boxText = "Не хватает производительности.";
            string caption = "Ошибка";
            MessageBox.Show(boxText, caption, button, icon);
        }

        /// <summary>
        /// Отвечает за отключение и включение нужных для этого фрактала
        /// слайдеров, а также вызывает метод постороения фрактала "множество Кантора".
        /// </summary>
        /// <param name="recursionDeep">Глубина рекурсии фрактала.</param>
        /// <param name="distanceBetweenLines">Расстояние между прямыми.</param>
        private void GetKantor(int recursionDeep, int distanceBetweenLines)
        {
            double opacityNotVisible = 0.3;
            double opacityVisible = 1;
                  
            firstAngleSlider.IsEnabled = false;
            secondAngleSlider.IsEnabled = false;
            distanceBetweenLinesSlider.IsEnabled = true;
            koefOfSegmentsSlider.IsEnabled = false;

            firstAngleText.Opacity = opacityNotVisible;
            secondAngleText.Opacity = opacityNotVisible;
            distanceBetweenLinesText.Opacity = opacityVisible;
            koefOfSegmentsText.Opacity = opacityNotVisible;

            recursionDeepSlider.Maximum = 12;

            Kantor kn = new Kantor(canvas1, recursionDeep, distanceBetweenLines);
            kn.ToDraw();
        }

        /// <summary>
        /// Отвечает за отключение и включение нужных для этого фрактала
        /// слайдеров, а также вызывает метод постороения фрактала "Треугольник Серпинского".
        /// </summary>
        /// <param name="recursionDeep">Глубина рекурсии фрактала.</param>
        private void GetTriangles(int recursionDeep)
        {
            double opacity = 0.3;
            firstAngleSlider.IsEnabled = false;
            secondAngleSlider.IsEnabled = false;
            distanceBetweenLinesSlider.IsEnabled = false;
            koefOfSegmentsSlider.IsEnabled = false;

            firstAngleText.Opacity = opacity;
            secondAngleText.Opacity = opacity;
            distanceBetweenLinesText.Opacity = opacity;
            koefOfSegmentsText.Opacity = opacity;

            if (recursionDeep > 8)
            {
                recursionDeep = 8;
            }

            recursionDeepSlider.Maximum = 8;

            Triangle t = new Triangle(canvas1, recursionDeep);
            t.ToDraw();
        }

        /// <summary>
        /// Отвечает за отключение и включение нужных для этого фрактала
        /// слайдеров, а также вызывает метод постороения фрактала "Ковер Серпинского".
        /// </summary>
        /// <param name="recursionDeep">Глубина рекурсии фрактала.</param>
        private void GetSquares(int recursionDeep)
        {
            double opacity = 0.3;
            firstAngleSlider.IsEnabled = false;
            secondAngleSlider.IsEnabled = false;
            distanceBetweenLinesSlider.IsEnabled = false;
            koefOfSegmentsSlider.IsEnabled = false;

            firstAngleText.Opacity = opacity;
            secondAngleText.Opacity = opacity;
            distanceBetweenLinesText.Opacity = opacity;
            koefOfSegmentsText.Opacity = opacity;

            if (recursionDeep > 5)
            {
                recursionDeep = 5;
            }

            recursionDeepSlider.Maximum = 5;

            Squares s = new Squares(canvas1, recursionDeep);
            s.ToDraw();
        }

        /// <summary>
        /// Отвечает за отключение и включение нужных для этого фрактала
        /// слайдеров, а также вызывает метод постороения фрактала "Обдуваемое дерево".
        /// </summary>
        /// <param name="recursionDeep">Глубина рекурсии фрактала.</param>
        /// <param name="angleOfSecondSegment">Угол левого отрезка дерева.</param>
        /// <param name="angleOfFirstSegment">Угол правого отрезка дерева.</param>
        /// <param name="koefOfSegments">Отношение длин отрезков дерева.</param>
        void GetTree(int recursionDeep, int angleOfSecondSegment, int angleOfFirstSegment, double koefOfSegments)
        {

            double opacityNotVisible = 0.3;
            double opacityVisible = 1;
            firstAngleSlider.IsEnabled = true;
            secondAngleSlider.IsEnabled = true;
            distanceBetweenLinesSlider.IsEnabled = false;
            koefOfSegmentsSlider.IsEnabled = true;

            firstAngleText.Opacity = opacityVisible;
            secondAngleText.Opacity = opacityVisible;
            distanceBetweenLinesText.Opacity = opacityNotVisible;
            koefOfSegmentsText.Opacity = opacityVisible;

            if (recursionDeep > 10)
            {
                recursionDeep = 10;
            }

            recursionDeepSlider.Maximum = 10;

            Tree tree = new Tree(canvas1, recursionDeep, angleOfSecondSegment, angleOfFirstSegment, koefOfSegments);
            tree.ToDraw();
        }

        /// <summary>
        /// Отвечает за отключение и включение нужных для этого фрактала
        /// слайдеров, а также вызывает метод постороения фрактала "Кривая Коха".
        /// </summary>
        /// <param name="recursionDeep">Глубина рекурсии дерева.</param>
        void GetKohaLine(int recursionDeep)
        {
            double opacity = 0.3;
            firstAngleSlider.IsEnabled = false;
            secondAngleSlider.IsEnabled = false;
            distanceBetweenLinesSlider.IsEnabled = false;
            koefOfSegmentsSlider.IsEnabled = false;

            firstAngleText.Opacity = opacity;
            secondAngleText.Opacity = opacity;
            distanceBetweenLinesText.Opacity = opacity;
            koefOfSegmentsText.Opacity = opacity;

            if (recursionDeep > 5)
            {
                recursionDeep = 5;
            }

            recursionDeepSlider.Maximum = 5;

            KohaLine kh = new KohaLine(canvas1, recursionDeep);
            kh.ToDraw();
        }
    }
}
