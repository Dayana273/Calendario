using System;
using System.Collections.Generic;
using System.Text;

namespace Calendario.Models
{
    using SQLite;

    namespace Calendario.Models
    {
        public class Event
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string Title { get; set; }

            public DateTime Date { get; set; }
        }
    }

}
