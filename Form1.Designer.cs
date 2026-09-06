namespace Calculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtDisplay;

        private Button btn0;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btn7;
        private Button btn8;
        private Button btn9;

        private Button btnPlus;
        private Button btnMinus;
        private Button btnMultiply;
        private Button btnDivide;

        private Button btnEquals;
        private Button btnClear;
        private Button btnDecimal;
        private Button btnBackspace;

        private Button btnSave;
        private Button btnLoad;
        private Button btnHistory;
        private Button btnTheme;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components =
                new System.ComponentModel.Container();

            txtDisplay = new TextBox();

            btn0 = new Button();
            btn1 = new Button();
            btn2 = new Button();
            btn3 = new Button();
            btn4 = new Button();
            btn5 = new Button();
            btn6 = new Button();
            btn7 = new Button();
            btn8 = new Button();
            btn9 = new Button();

            btnPlus = new Button();
            btnMinus = new Button();
            btnMultiply = new Button();
            btnDivide = new Button();

            btnEquals = new Button();
            btnClear = new Button();
            btnDecimal = new Button();
            btnBackspace = new Button();

            btnSave = new Button();
            btnLoad = new Button();
            btnHistory = new Button();
            btnTheme = new Button();

            SuspendLayout();

            // =========================
            // ФОРМА
            // =========================

            AutoScaleDimensions =
                new SizeF(7F, 15F);

            AutoScaleMode =
                AutoScaleMode.Font;

            ClientSize =
                new Size(430, 520);

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            StartPosition =
                FormStartPosition.CenterScreen;

            Text = "Калькулятор";

            // =========================
            // ДИСПЛЕЙ
            // =========================

            txtDisplay.Font =
                new Font(
                    "Segoe UI",
                    24F);

            txtDisplay.Location =
                new Point(20, 20);

            txtDisplay.Name =
                "txtDisplay";

            txtDisplay.ReadOnly =
                true;

            txtDisplay.Size =
                new Size(390, 50);

            txtDisplay.TabIndex = 0;

            txtDisplay.Text =
                "0";

            txtDisplay.TextAlign =
                HorizontalAlignment.Right;

            Controls.Add(txtDisplay);

            // =========================
            // ЦИФРЫ
            // =========================

            ConfigureNumberButton(
                btn7,
                "7",
                20,
                100);

            ConfigureNumberButton(
                btn8,
                "8",
                115,
                100);

            ConfigureNumberButton(
                btn9,
                "9",
                210,
                100);

            ConfigureNumberButton(
                btn4,
                "4",
                20,
                165);

            ConfigureNumberButton(
                btn5,
                "5",
                115,
                165);

            ConfigureNumberButton(
                btn6,
                "6",
                210,
                165);

            ConfigureNumberButton(
                btn1,
                "1",
                20,
                230);

            ConfigureNumberButton(
                btn2,
                "2",
                115,
                230);

            ConfigureNumberButton(
                btn3,
                "3",
                210,
                230);

            ConfigureNumberButton(
                btn0,
                "0",
                20,
                295);

            // =========================
            // ОПЕРАЦИИ
            // =========================

            ConfigureButton(
                btnDivide,
                "÷",
                305,
                100);

            ConfigureButton(
                btnMultiply,
                "×",
                305,
                165);

            ConfigureButton(
                btnMinus,
                "−",
                305,
                230);

            ConfigureButton(
                btnPlus,
                "+",
                305,
                295);

            // =========================
            // ДОПОЛНИТЕЛЬНЫЕ КНОПКИ
            // =========================

            ConfigureButton(
                btnDecimal,
                ".",
                115,
                295);

            ConfigureButton(
                btnBackspace,
                "⌫",
                210,
                295);

            ConfigureButton(
                btnClear,
                "C",
                20,
                360);

            ConfigureButton(
                btnEquals,
                "=",
                115,
                360);

            // =========================
            // СОХРАНЕНИЕ
            // =========================

            ConfigureSmallButton(
                btnSave,
                "Сохранить",
                20,
                430);

            ConfigureSmallButton(
                btnLoad,
                "Загрузить",
                125,
                430);

            ConfigureSmallButton(
                btnHistory,
                "История",
                230,
                430);

            ConfigureSmallButton(
                btnTheme,
                "Тема",
                335,
                430);

            // =========================
            // ОБРАБОТЧИКИ
            // =========================

            btnPlus.Click +=
                OperationButton_Click;

            btnMinus.Click +=
                OperationButton_Click;

            btnMultiply.Click +=
                OperationButton_Click;

            btnDivide.Click +=
                OperationButton_Click;

            btnEquals.Click +=
                btnEquals_Click;

            btnClear.Click +=
                btnClear_Click;

            btnDecimal.Click +=
                btnDecimal_Click;

            btnBackspace.Click +=
                btnBackspace_Click;

            btnSave.Click +=
                btnSave_Click;

            btnLoad.Click +=
                btnLoad_Click;

            btnHistory.Click +=
                btnHistory_Click;

            btnTheme.Click +=
                btnTheme_Click;

            ResumeLayout(false);
            PerformLayout();
        }

        // =========================
        // ЦИФРОВАЯ КНОПКА
        // =========================

        private void ConfigureNumberButton(
            Button button,
            string text,
            int x,
            int y)
        {
            ConfigureButton(
                button,
                text,
                x,
                y);

            button.Click +=
                NumberButton_Click;
        }

        // =========================
        // ОБЫЧНАЯ КНОПКА
        // =========================

        private void ConfigureButton(
            Button button,
            string text,
            int x,
            int y)
        {
            button.Font =
                new Font(
                    "Segoe UI",
                    16F);

            button.Location =
                new Point(x, y);

            button.Name =
                "button_" + text;

            button.Size =
                new Size(85, 55);

            button.TabIndex = 0;

            button.Text =
                text;

            button.UseVisualStyleBackColor =
                true;

            Controls.Add(button);
        }

        // =========================
        // МАЛЕНЬКАЯ КНОПКА
        // =========================

        private void ConfigureSmallButton(
            Button button,
            string text,
            int x,
            int y)
        {
            button.Font =
                new Font(
                    "Segoe UI",
                    9F);

            button.Location =
                new Point(x, y);

            button.Size =
                new Size(95, 50);

            button.Text =
                text;

            button.UseVisualStyleBackColor =
                true;

            Controls.Add(button);
        }
    }
}