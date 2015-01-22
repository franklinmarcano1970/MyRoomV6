using System;
using System.Collections.Generic;
using System.Linq;
using MyRoom.Model;

namespace MyRoom.Data.SampleData
{
    public static class TheProductTranslation
    {
        public static List<Translation> _theTranslation;
        private static Translation
            _product1;

        /// <summary>Add the translation</summary>
        public static void AddTranslation(List<Translation> persons)
        {
            _theTranslation = new List<Translation>();

            _theTranslation.Add(_product1 = new Translation
            {
                Spanish = "Desayuno en Habitación",
                English = "Breakfast Rooms",
                French = "Breakfast Rooms",
                Active = true
            });

            persons.AddRange(_theTranslation);
        }
    }
}
