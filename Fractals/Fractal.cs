using System.Windows.Controls;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Базовый класс для всех фракталов.
    /// Хранит общие свойства всех фракталов.
    /// </summary>
    class Fractal
    {
        /// <summary>
        /// Глубина рекурсии.
        /// </summary>
        protected int deepOfRecursion;
        /// <summary>
        /// Канвас, на котором будет строиться фрактал.
        /// </summary>
        protected Canvas canvas;
        /// <summary>
        /// Хранит все точки заданного фрактала.
        /// </summary>
        protected Polygon polygon;

        /// <summary>
        /// Конструктор класса. Инициализирует все поля класса.
        /// </summary>
        /// <param name="canvas">Канвас, на котором будет строиться фрактал.</param>
        /// <param name="deepOfRecursion">Глубина рекурсии фрактала.</param>
        public Fractal(Canvas canvas, int deepOfRecursion)
        {
            this.canvas = canvas;
            this.canvas.Children.Clear();
            this.deepOfRecursion = deepOfRecursion;
        }
        /// <summary>
        /// Метод для переопределения в дочерних классах.
        /// Ничего не делает.
        /// </summary>
        virtual public void ToDraw() { }
    }
}
