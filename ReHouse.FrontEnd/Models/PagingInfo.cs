using System;

namespace ReHouse.FrontEnd.Models
{
    public class PagingInfo
    {
        // Кол-во объявлений
        public int TotalItems { get; set; }

        // Кол-во объявлений на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        } 
    }
}