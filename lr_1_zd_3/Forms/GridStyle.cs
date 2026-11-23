namespace lr_1_zd_3
{
    // Єдиний вигляд і розкладка таблиць у всіх формах
    public static class GridStyle
    {
        // Збільшений шрифт і висота рядків під нього
        public static void ApplyLargeFont(this DataGridView grid)
        {
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 12F);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        // Ділить висоту порівну між верхнім і нижнім елементом;
        // підпис між ними ставиться по центру проміжку
        public static void SplitEvenly(Control top, Control? caption, Control bottom)
        {
            int gap = bottom.Top - top.Bottom;
            int bottomEdge = bottom.Bottom;
            int half = (bottomEdge - top.Top - gap) / 2;
            if (half <= 0) return;

            top.Height = half;
            bottom.Top = top.Bottom + gap;
            bottom.Height = bottomEdge - bottom.Top;
            if (caption != null)
                caption.Top = top.Bottom + (gap - caption.Height) / 2;
        }
    }
}
