using System;
using System.Collections.Generic;
using System.Text;


namespace Travel.Model
{
    class Post
    {
      //  [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
       // [MaxLength(250)]
        public string Experience { get; set; }

        public string VanueName { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public int Distance { get; set; }
    }
}

