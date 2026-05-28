using Microsoft.VisualStudio.TestTools.UnitTesting;
using TipCalculator;

namespace TipCalculatorTests
{
    /// <summary>
    /// Автоматизированные тесты для класса TipCalculatorLogic.
    /// Покрывают корректные данные, граничные значения и некорректные данные.
    /// </summary>
    [TestClass]
    public class TipCalculatorLogicTests
    {
        // =====================================================================
        // Тесты метода CalculateTip — корректные данные
        // =====================================================================

        [TestMethod]
        [Description("Чаевые 0% — всегда 0 независимо от суммы")]
        public void CalculateTip_ZeroPercent_ReturnsZero()
        {
            double result = TipCalculatorLogic.CalculateTip(1000, 0);
            Assert.AreEqual(0.0, result, 0.001);
        }

        [TestMethod]
        [Description("Чаевые 5% от 1000 руб. = 50 руб.")]
        public void CalculateTip_5Percent_CorrectResult()
        {
            double result = TipCalculatorLogic.CalculateTip(1000, 5);
            Assert.AreEqual(50.0, result, 0.001);
        }

        [TestMethod]
        [Description("Чаевые 10% от 1000 руб. = 100 руб.")]
        public void CalculateTip_10Percent_CorrectResult()
        {
            double result = TipCalculatorLogic.CalculateTip(1000, 10);
            Assert.AreEqual(100.0, result, 0.001);
        }

        [TestMethod]
        [Description("Чаевые 15% от 1000 руб. = 150 руб.")]
        public void CalculateTip_15Percent_CorrectResult()
        {
            double result = TipCalculatorLogic.CalculateTip(1000, 15);
            Assert.AreEqual(150.0, result, 0.001);
        }

        [TestMethod]
        [Description("Чаевые 10% от дробной суммы")]
        public void CalculateTip_DecimalBill_CorrectResult()
        {
            double result = TipCalculatorLogic.CalculateTip(123.50, 10);
            Assert.AreEqual(12.35, result, 0.001);
        }

        // =====================================================================
        // Тесты метода CalculateTip — граничные значения
        // =====================================================================

        [TestMethod]
        [Description("Минимально допустимая сумма счёта — чуть больше нуля")]
        public void CalculateTip_MinimalBill_DoesNotThrow()
        {
            double result = TipCalculatorLogic.CalculateTip(0.01, 10);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        [Description("Очень большая сумма счёта — не должна выбрасывать исключение")]
        public void CalculateTip_VeryLargeBill_CorrectResult()
        {
            double result = TipCalculatorLogic.CalculateTip(1_000_000, 15);
            Assert.AreEqual(150_000.0, result, 0.001);
        }

        // =====================================================================
        // Тесты метода CalculateTip — некорректные данные
        // =====================================================================

        [TestMethod]
        [Description("Сумма счёта = 0 — должно быть исключение")]
        public void CalculateTip_ZeroBill_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculateTip(0, 10));
        }

        [TestMethod]
        [Description("Отрицательная сумма счёта — должно быть исключение")]
        public void CalculateTip_NegativeBill_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculateTip(-500, 10));
        }

        [TestMethod]
        [Description("Некорректный процент (например, 20) — должно быть исключение")]
        public void CalculateTip_InvalidPercent_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculateTip(1000, 20));
        }

        [TestMethod]
        [Description("Отрицательный процент — должно быть исключение")]
        public void CalculateTip_NegativePercent_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculateTip(1000, -5));
        }

        // =====================================================================
        // Тесты метода CalculateTotal — корректные данные
        // =====================================================================

        [TestMethod]
        [Description("Итог без чаевых равен сумме счёта")]
        public void CalculateTotal_ZeroTip_EqualsBill()
        {
            double result = TipCalculatorLogic.CalculateTotal(800, 0);
            Assert.AreEqual(800.0, result, 0.001);
        }

        [TestMethod]
        [Description("Итог с 10% = счёт × 1.10")]
        public void CalculateTotal_10Percent_CorrectTotal()
        {
            double result = TipCalculatorLogic.CalculateTotal(500, 10);
            Assert.AreEqual(550.0, result, 0.001);
        }

        [TestMethod]
        [Description("Итог с 15% от 200 руб. = 230 руб.")]
        public void CalculateTotal_15Percent_CorrectTotal()
        {
            double result = TipCalculatorLogic.CalculateTotal(200, 15);
            Assert.AreEqual(230.0, result, 0.001);
        }

        [TestMethod]
        [Description("Итог всегда больше или равен исходному счёту")]
        public void CalculateTotal_AlwaysGreaterOrEqual()
        {
            double bill = 300;
            double total = TipCalculatorLogic.CalculateTotal(bill, 5);
            Assert.IsTrue(total >= bill);
        }

        // =====================================================================
        // Тесты метода CalculateTotal — некорректные данные
        // =====================================================================

        [TestMethod]
        [Description("Итог при нулевом счёте — исключение")]
        public void CalculateTotal_ZeroBill_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculateTotal(0, 5));
        }

        [TestMethod]
        [Description("Итог при некорректном проценте — исключение")]
        public void CalculateTotal_InvalidPercent_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculateTotal(500, 7));
        }

        // =====================================================================
        // Тесты метода CalculatePerPerson — корректные данные
        // =====================================================================

        [TestMethod]
        [Description("Один гость — сумма на гостя = итогу")]
        public void CalculatePerPerson_OneGuest_EqualTotal()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(1100, 1);
            Assert.AreEqual(1100.0, result, 0.001);
        }

        [TestMethod]
        [Description("Два гостя — счёт делится поровну")]
        public void CalculatePerPerson_TwoGuests_HalfEach()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(1100, 2);
            Assert.AreEqual(550.0, result, 0.001);
        }

        [TestMethod]
        [Description("Четыре гостя — результат корректен")]
        public void CalculatePerPerson_FourGuests_CorrectResult()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(1000, 4);
            Assert.AreEqual(250.0, result, 0.001);
        }

        [TestMethod]
        [Description("Результат на гостя всегда > 0 при корректных данных")]
        public void CalculatePerPerson_ResultIsPositive()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(300, 3);
            Assert.IsTrue(result > 0);
        }

        // =====================================================================
        // Тесты метода CalculatePerPerson — граничные значения
        // =====================================================================

        [TestMethod]
        [Description("Большое число гостей — не должно выбрасывать исключение")]
        public void CalculatePerPerson_ManyGuests_NoException()
        {
            double result = TipCalculatorLogic.CalculatePerPerson(10000, 100);
            Assert.AreEqual(100.0, result, 0.001);
        }

        // =====================================================================
        // Тесты метода CalculatePerPerson — некорректные данные
        // =====================================================================

        [TestMethod]
        [Description("Ноль гостей — исключение")]
        public void CalculatePerPerson_ZeroGuests_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculatePerPerson(1000, 0));
        }

        [TestMethod]
        [Description("Отрицательное число гостей — исключение")]
        public void CalculatePerPerson_NegativeGuests_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculatePerPerson(1000, -1));
        }

        [TestMethod]
        [Description("Нулевая сумма — исключение")]
        public void CalculatePerPerson_ZeroAmount_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.CalculatePerPerson(0, 2));
        }

        // =====================================================================
        // Тесты метода Calculate (полный расчёт) — интеграционные
        // =====================================================================

        [TestMethod]
        [Description("Полный расчёт: 1000 руб., 10%, 2 гостя")]
        public void Calculate_FullScenario_CorrectResults()
        {
            var (tip, total, perPerson) = TipCalculatorLogic.Calculate(1000, 10, 2);
            Assert.AreEqual(100.0,  tip,       0.001);
            Assert.AreEqual(1100.0, total,     0.001);
            Assert.AreEqual(550.0,  perPerson, 0.001);
        }

        [TestMethod]
        [Description("Полный расчёт без чаевых, 1 гость")]
        public void Calculate_NoTipOneGuest_TotalEqualsBill()
        {
            var (tip, total, perPerson) = TipCalculatorLogic.Calculate(500, 0, 1);
            Assert.AreEqual(0.0,   tip,       0.001);
            Assert.AreEqual(500.0, total,     0.001);
            Assert.AreEqual(500.0, perPerson, 0.001);
        }

        [TestMethod]
        [Description("Полный расчёт: 15% чаевых, 5 гостей")]
        public void Calculate_15PercentFiveGuests_CorrectPerPerson()
        {
            var (_, total, perPerson) = TipCalculatorLogic.Calculate(1000, 15, 5);
            Assert.AreEqual(1150.0, total,     0.001);
            Assert.AreEqual(230.0,  perPerson, 0.001);
        }

        [TestMethod]
        [Description("Результат на гостя не равен итогу при нескольких гостях")]
        public void Calculate_MultipleGuests_PerPersonNotEqualTotal()
        {
            var (_, total, perPerson) = TipCalculatorLogic.Calculate(1000, 10, 3);
            Assert.AreNotEqual(total, perPerson);
        }

        [TestMethod]
        [Description("Полный расчёт с некорректной суммой — исключение")]
        public void Calculate_InvalidBill_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.Calculate(-100, 10, 2));
        }

        [TestMethod]
        [Description("Полный расчёт с некорректным числом гостей — исключение")]
        public void Calculate_InvalidGuests_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => TipCalculatorLogic.Calculate(1000, 10, 0));
        }
    }
}
