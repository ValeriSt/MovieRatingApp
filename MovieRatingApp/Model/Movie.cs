using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApp.Model
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public string Description { get; set; }

        public string Genres { get; set; } 

        public string ImageURL { get; set; }

    }
}
