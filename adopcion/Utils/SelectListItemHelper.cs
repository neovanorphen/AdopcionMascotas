using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adopcion.Utils
{
    public class SelectListItemHelper
    {
        public static IEnumerable<SelectListItem> RacesList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "San Bernardo", Value = "1"},
                new SelectListItem{Text = "Policial", Value = "2"},
                new SelectListItem{Text = "Persa", Value = "3"},
                new SelectListItem{Text = "Siames", Value = "4"},
                new SelectListItem{Text = "Angora Turco", Value = "5"}

            };
            return items;
        }


        public static IEnumerable<SelectListItem> TypesList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Perro", Value = "1"},
                new SelectListItem{Text = "Gato", Value = "2"},
                new SelectListItem{Text = "Tortuga", Value = "3"},
                new SelectListItem{Text = "Elefante", Value = "4"},
            };
            return items;
        }

        public static IEnumerable<SelectListItem> CitiesList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Curico", Value = "1"},
                new SelectListItem{Text = "Talca", Value = "2"},
                new SelectListItem{Text = "Linares", Value = "3"},
                new SelectListItem{Text = "Constitucion", Value = "4"},
            };
            return items;
        }

    }
}