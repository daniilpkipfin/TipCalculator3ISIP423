namespace TipCalculator
{
    /// <summary>
    /// Статический класс, содержащий бизнес-логику калькулятора чаевых.
    /// Все методы принимают простые типы и выбрасывают ArgumentException при некорректных данных.
    /// </summary>
    public static class TipCalculatorLogic
    {
        /// <summary>
        /// Рассчитывает сумму чаевых.
        /// </summary>
        /// <param name="billAmount">Сумма счёта (должна быть > 0)</param>
        /// <param name="tipPercent">Процент чаевых (0, 5, 10 или 15)</param>
        /// <returns>Сумма чаевых</returns>
        public static double CalculateTip(double billAmount, int tipPercent)
        {
            if (billAmount <= 0)
                throw new ArgumentException("Сумма счёта должна быть больше нуля.", nameof(billAmount));

            if (tipPercent != 0 && tipPercent != 5 && tipPercent != 10 && tipPercent != 15)
                throw new ArgumentException("Процент чаевых должен быть 0, 5, 10 или 15.", nameof(tipPercent));

            return billAmount * tipPercent / 100.0;
        }

        /// <summary>
        /// Рассчитывает итоговую сумму (счёт + чаевые).
        /// </summary>
        /// <param name="billAmount">Сумма счёта (должна быть > 0)</param>
        /// <param name="tipPercent">Процент чаевых (0, 5, 10 или 15)</param>
        /// <returns>Итоговая сумма</returns>
        public static double CalculateTotal(double billAmount, int tipPercent)
        {
            if (billAmount <= 0)
                throw new ArgumentException("Сумма счёта должна быть больше нуля.", nameof(billAmount));

            if (tipPercent != 0 && tipPercent != 5 && tipPercent != 10 && tipPercent != 15)
                throw new ArgumentException("Процент чаевых должен быть 0, 5, 10 или 15.", nameof(tipPercent));

            double tip = CalculateTip(billAmount, tipPercent);
            return billAmount + tip;
        }

        /// <summary>
        /// Рассчитывает сумму на одного гостя.
        /// </summary>
        /// <param name="totalAmount">Итоговая сумма (должна быть > 0)</param>
        /// <param name="guestCount">Количество гостей (должно быть >= 1)</param>
        /// <returns>Сумма на одного гостя</returns>
        public static double CalculatePerPerson(double totalAmount, int guestCount)
        {
            if (totalAmount <= 0)
                throw new ArgumentException("Итоговая сумма должна быть больше нуля.", nameof(totalAmount));

            if (guestCount < 1)
                throw new ArgumentException("Количество гостей должно быть не менее 1.", nameof(guestCount));

            return totalAmount / guestCount;
        }

        /// <summary>
        /// Выполняет полный расчёт чаевых: сумма чаевых, итог и сумма на гостя.
        /// </summary>
        /// <param name="billAmount">Сумма счёта</param>
        /// <param name="tipPercent">Процент чаевых</param>
        /// <param name="guestCount">Количество гостей</param>
        /// <returns>Кортеж (чаевые, итог, на одного гостя)</returns>
        public static (double Tip, double Total, double PerPerson) Calculate(
            double billAmount, int tipPercent, int guestCount)
        {
            // Валидация выполняется внутри вызываемых методов
            double tip = CalculateTip(billAmount, tipPercent);
            double total = CalculateTotal(billAmount, tipPercent);
            double perPerson = CalculatePerPerson(total, guestCount);
            return (tip, total, perPerson);
        }
    }
}
