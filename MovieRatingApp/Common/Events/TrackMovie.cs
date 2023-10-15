using MovieRatingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApp.Common
{
    public class TrackMovie
    {
        public event EventHandler MovieChanged;

        public void ChangeMovie()
        {
            MovieChanged?.Invoke(this, new EventArgs());   
        }

    }
}
