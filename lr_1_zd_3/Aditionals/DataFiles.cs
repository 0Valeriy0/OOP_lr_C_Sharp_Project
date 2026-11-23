namespace lr_1_zd_3.Aditionals
{
    // Шляхи до файлів бази даних програми (папка Data у корені рішення).
    // Форми автоматично завантажують ці файли під час відкриття.
    public static class DataFiles
    {
        // Запасний шлях, якщо папку Data не вдалося знайти поруч із програмою
        private const string DefaultFolder = @"E:\VisualStudio\3_kurs\OOP\lr_1_zd_3\Data";

        public static readonly string Folder = FindDataFolder();

        public static string Sportsmen => Path.Combine(Folder, "База_Спортсмени.json");
        public static string Coaches => Path.Combine(Folder, "База_Тренери.xml");
        public static string Groups => Path.Combine(Folder, "База_Групи.txt");
        public static string Trainings => Path.Combine(Folder, "База_Тренування.txt");

        // Програма запускається з ...\lr_1_zd_3\bin\Debug\net10.0-windows,
        // тому піднімаємося вгору по папках, доки не знайдемо папку Data
        private static string FindDataFolder()
        {
            DirectoryInfo? dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                string candidate = Path.Combine(dir.FullName, "Data");
                if (Directory.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }
            return DefaultFolder;
        }
    }
}
