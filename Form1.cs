using System.Globalization;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double firstNumber = 0;
        private string currentOperation = "";
        private bool isNewInput = true;

        private readonly List<string> calculationHistory = new();

        public Form1()
        {
            InitializeComponent();
            ApplyLightTheme();
        }

        // =========================
        // ВВОД ЦИФР
        // =========================

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (isNewInput)
            {
                // Если начинаем второе число после операции
                if (txtDisplay.Text.EndsWith(" "))
                {
                    txtDisplay.Text += button.Text;
                }
                // Если вводим отрицательное число
                else if (txtDisplay.Text.EndsWith("-"))
                {
                    txtDisplay.Text += button.Text;
                }
                else
                {
                    txtDisplay.Text = button.Text;
                }

                isNewInput = false;
                return;
            }

            if (txtDisplay.Text == "0")
            {
                txtDisplay.Text = button.Text;
            }
            else
            {
                txtDisplay.Text += button.Text;
            }
        }

        // =========================
        // ДЕСЯТИЧНАЯ ТОЧКА
        // =========================

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (isNewInput)
            {
                if (txtDisplay.Text.EndsWith(" "))
                {
                    txtDisplay.Text += "0.";
                }
                else if (txtDisplay.Text.EndsWith("-"))
                {
                    txtDisplay.Text += "0.";
                }
                else
                {
                    txtDisplay.Text = "0.";
                }

                isNewInput = false;
                return;
            }

            // Проверяем только последнее число
            string[] parts = txtDisplay.Text.Split(' ');

            string lastPart = parts[^1];

            if (!lastPart.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }

        // =========================
        // ОПЕРАЦИИ
        // =========================

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Text;

            // Минус в самом начале — отрицательное число
            if (operation == "−" &&
                txtDisplay.Text == "0" &&
                isNewInput)
            {
                txtDisplay.Text = "-";
                isNewInput = true;
                return;
            }

            // Минус после другой операции —
            // начинаем отрицательное второе число
            if (operation == "−" &&
                isNewInput &&
                !string.IsNullOrEmpty(currentOperation))
            {
                if (!txtDisplay.Text.EndsWith("-"))
                {
                    txtDisplay.Text += "-";
                }

                isNewInput = true;
                return;
            }

            // Нельзя выполнить операцию после одного "-"
            if (txtDisplay.Text == "-" ||
                txtDisplay.Text.EndsWith(" -"))
            {
                return;
            }

            // Первая операция
            if (string.IsNullOrEmpty(currentOperation))
            {
                string numberText = txtDisplay.Text.Trim();

                if (!double.TryParse(
                    numberText,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out firstNumber))
                {
                    ShowError("Некорректное число.");
                    return;
                }

                currentOperation = operation;

                txtDisplay.Text =
                    $"{FormatNumber(firstNumber)} {operation} ";

                isNewInput = true;

                return;
            }

            // Если ввод второго числа уже начался,
            // выполняем предыдущую операцию
            if (!isNewInput)
            {
                if (!TryGetSecondNumber(out double secondNumber))
                {
                    ShowError("Некорректное число.");
                    return;
                }

                if (!PerformCalculation(secondNumber))
                {
                    return;
                }
            }

            currentOperation = operation;

            txtDisplay.Text =
                $"{FormatNumber(firstNumber)} {operation} ";

            isNewInput = true;
        }

        // =========================
        // РАВНО
        // =========================

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentOperation))
            {
                return;
            }

            if (!TryGetSecondNumber(out double secondNumber))
            {
                ShowError("Введите второе число.");
                return;
            }

            if (!PerformCalculation(secondNumber))
            {
                return;
            }

            currentOperation = "";
            isNewInput = true;
        }

        // =========================
        // ПОЛУЧЕНИЕ ВТОРОГО ЧИСЛА
        // =========================

        private bool TryGetSecondNumber(out double secondNumber)
        {
            secondNumber = 0;

            string expression = txtDisplay.Text;

            string prefix =
                $"{FormatNumber(firstNumber)} {currentOperation} ";

            if (!expression.StartsWith(prefix))
            {
                return false;
            }

            string secondNumberText =
                expression.Substring(prefix.Length).Trim();

            if (string.IsNullOrEmpty(secondNumberText) ||
                secondNumberText == "-")
            {
                return false;
            }

            return double.TryParse(
                secondNumberText,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out secondNumber);
        }

        // =========================
        // ВЫЧИСЛЕНИЕ
        // =========================

        private bool PerformCalculation(double secondNumber)
        {
            double result;

            switch (currentOperation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;

                case "−":
                    result = firstNumber - secondNumber;
                    break;

                case "×":
                    result = firstNumber * secondNumber;
                    break;

                case "÷":
                    if (secondNumber == 0)
                    {
                        ShowError("Деление на ноль невозможно!");
                        return false;
                    }

                    result = firstNumber / secondNumber;
                    break;

                default:
                    return false;
            }

            string calculation =
                $"{FormatNumber(firstNumber)} " +
                $"{currentOperation} " +
                $"{FormatNumber(secondNumber)} = " +
                $"{FormatNumber(result)}";

            calculationHistory.Add(calculation);

            firstNumber = result;

            txtDisplay.Text =
                FormatNumber(result);

            return true;
        }

        // =========================
        // ОЧИСТКА
        // =========================

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";

            firstNumber = 0;
            currentOperation = "";

            isNewInput = true;
        }

        // =========================
        // BACKSPACE
        // =========================

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length <= 1)
            {
                txtDisplay.Text = "0";
                isNewInput = true;
                return;
            }

            txtDisplay.Text =
                txtDisplay.Text.Substring(
                    0,
                    txtDisplay.Text.Length - 1);

            if (txtDisplay.Text == "-" ||
                txtDisplay.Text.EndsWith(" "))
            {
                isNewInput = true;
            }
        }

        // =========================
        // СОХРАНЕНИЕ
        // =========================

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (calculationHistory.Count == 0)
            {
                MessageBox.Show(
                    "История вычислений пуста.",
                    "Сохранение");

                return;
            }

            using SaveFileDialog dialog =
                new SaveFileDialog();

            dialog.Filter =
                "Текстовые файлы (*.txt)|*.txt|" +
                "Все файлы (*.*)|*.*";

            dialog.FileName =
                "calculator_history.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllLines(
                        dialog.FileName,
                        calculationHistory);

                    MessageBox.Show(
                        "История успешно сохранена!",
                        "Сохранение");
                }
                catch (Exception ex)
                {
                    ShowError(
                        "Ошибка сохранения: " +
                        ex.Message);
                }
            }
        }

        // =========================
        // ЗАГРУЗКА
        // =========================

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "Текстовые файлы (*.txt)|*.txt|" +
                "Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines =
                        File.ReadAllLines(
                            dialog.FileName);

                    calculationHistory.Clear();

                    calculationHistory.AddRange(lines);

                    MessageBox.Show(
                        "История успешно загружена!",
                        "Загрузка");
                }
                catch (Exception ex)
                {
                    ShowError(
                        "Ошибка загрузки: " +
                        ex.Message);
                }
            }
        }

        // =========================
        // ИСТОРИЯ
        // =========================

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (calculationHistory.Count == 0)
            {
                MessageBox.Show(
                    "История вычислений пуста.",
                    "История");

                return;
            }

            string history =
                string.Join(
                    Environment.NewLine,
                    calculationHistory);

            MessageBox.Show(
                history,
                "История вычислений");
        }

        // =========================
        // ТЕМА
        // =========================

        private void btnTheme_Click(object sender, EventArgs e)
        {
            if (BackColor ==
                Color.FromArgb(245, 245, 245))
            {
                ApplyDarkTheme();
            }
            else
            {
                ApplyLightTheme();
            }
        }

        // =========================
        // СВЕТЛАЯ ТЕМА
        // =========================

        private void ApplyLightTheme()
        {
            BackColor =
                Color.FromArgb(245, 245, 245);

            ForeColor =
                Color.Black;

            txtDisplay.BackColor =
                Color.White;

            txtDisplay.ForeColor =
                Color.Black;

            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.BackColor =
                        Color.White;

                    button.ForeColor =
                        Color.Black;
                }
            }

            btnEquals.BackColor =
                Color.FromArgb(76, 175, 80);

            btnEquals.ForeColor =
                Color.White;

            btnClear.BackColor =
                Color.FromArgb(244, 67, 54);

            btnClear.ForeColor =
                Color.White;

            btnTheme.BackColor =
                Color.FromArgb(33, 150, 243);

            btnTheme.ForeColor =
                Color.White;
        }

        // =========================
        // ТЁМНАЯ ТЕМА
        // =========================

        private void ApplyDarkTheme()
        {
            BackColor =
                Color.FromArgb(35, 35, 35);

            ForeColor =
                Color.White;

            txtDisplay.BackColor =
                Color.FromArgb(55, 55, 55);

            txtDisplay.ForeColor =
                Color.White;

            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.BackColor =
                        Color.FromArgb(65, 65, 65);

                    button.ForeColor =
                        Color.White;
                }
            }

            btnEquals.BackColor =
                Color.FromArgb(76, 175, 80);

            btnEquals.ForeColor =
                Color.White;

            btnClear.BackColor =
                Color.FromArgb(244, 67, 54);

            btnClear.ForeColor =
                Color.White;

            btnTheme.BackColor =
                Color.FromArgb(33, 150, 243);

            btnTheme.ForeColor =
                Color.White;
        }

        // =========================
        // ОШИБКА
        // =========================

        private void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            txtDisplay.Text = "0";

            firstNumber = 0;
            currentOperation = "";

            isNewInput = true;
        }

        // =========================
        // ФОРМАТ ЧИСЛА
        // =========================

        private string FormatNumber(double number)
        {
            return number.ToString(
                "G15",
                CultureInfo.InvariantCulture);
        }
    }
}