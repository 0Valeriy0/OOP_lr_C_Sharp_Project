using System;
using System.Collections;          // неузагальнені колекції: Stack
using System.Text;
using ClassLibrary1.Class;

namespace lr_1_zd_3.Class
{
    // Частковий клас: третя частина SportGroup — журнал змін складу групи
    internal partial class SportGroup
    {
        // Неузагальнений стек: останній запис — на вершині (LIFO)
        public Stack History { get; } = new Stack();

        // Реалізації часткових методів, оголошених у SportGroup.cs
        partial void OnMemberAdded(Sportsman sportsman)
        {
            History.Push($"{DateTime.Now:dd.MM.yyyy HH:mm} додано: {sportsman.Surname} {sportsman.Name}");
        }

        partial void OnMemberRemoved(Sportsman sportsman)
        {
            History.Push($"{DateTime.Now:dd.MM.yyyy HH:mm} вилучено: {sportsman.Surname} {sportsman.Name}");
        }

        partial void AppendHistory(StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine($"--- Історія змін ({History.Count}), останні зверху ---");
            foreach (object record in History)
            {
                sb.AppendLine((string)record);
            }
        }
    }
}