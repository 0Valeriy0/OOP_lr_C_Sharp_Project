using ClassLibrary1.Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3.Interface
{
    public interface ISortableBySurname
    {
        // void SortBySurname(List<Person> people);
        void SortBySurname<T>(List<T> people) where T : Person;
    }
}
