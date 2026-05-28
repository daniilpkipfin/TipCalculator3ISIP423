using System.Windows;

namespace TipCalculator
{
    /// <summary>
    /// Code-Behind для главного окна приложения «Калькулятор чаевых».
    /// Содержит только обработчики событий и логику отображения.
    /// Бизнес-логика вынесена в TipCalculatorLogic.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки «Рассчитать».
        /// Выполняет валидацию ввода и отображает результат.
        /// </summary>
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // --- Валидация суммы счёта ---
            if (!double.TryParse(txtBillAmount.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double billAmount) || billAmount <= 0)
            {
                MessageBox.Show("Введите корректную сумму счёта (положительное число).",
                                "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtBillAmount.Focus();
                return;
            }

            // --- Валидация количества гостей ---
            if (!int.TryParse(txtGuestCount.Text, out int guestCount) || guestCount < 1)
            {
                MessageBox.Show("Введите корректное количество гостей (целое число, не менее 1).",
                                "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtGuestCount.Focus();
                return;
            }

            // --- Определение выбранного процента чаевых ---
            int tipPercent = 0;
            if (rbTip5.IsChecked == true)  tipPercent = 5;
            else if (rbTip10.IsChecked == true) tipPercent = 10;
            else if (rbTip15.IsChecked == true) tipPercent = 15;

            // --- Вызов логики (исключения перехватываются) ---
            try
            {
                var (tip, total, perPerson) = TipCalculatorLogic.Calculate(billAmount, tipPercent, guestCount);

                // Заполняем результаты
                lblBill.Text    = $"{billAmount:F2} руб.";
                lblTip.Text     = $"{tip:F2} руб.";
                lblTotal.Text   = $"{total:F2} руб.";

                // Блок «на гостя» показываем только если гостей больше одного
                bool multipleGuests = guestCount > 1;
                pnlPerPerson.Visibility = multipleGuests ? Visibility.Visible : Visibility.Collapsed;
                sepGuests.Visibility    = multipleGuests ? Visibility.Visible : Visibility.Collapsed;

                if (multipleGuests)
                {
                    lblPerPersonLabel.Text = $"На каждого из {guestCount} гостей:";
                    lblPerPerson.Text = $"{perPerson:F2} руб.";
                }

                // Показываем блок результатов
                resultPanel.Visibility = Visibility.Visible;
            }
            catch (ArgumentException ex)
            {
                // Сообщение из логики (не должно возникать при правильной валидации выше)
                MessageBox.Show(ex.Message, "Ошибка расчёта",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
