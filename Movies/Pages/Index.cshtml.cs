using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// Gets and sets the search terms
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; }

        /// <summary>
        /// Gets and sets the MPAA rating filters
        /// </summary>
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// Gets and sets the Genre filters
        /// </summary>
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        public int? RottenMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        public int? RottenMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, int? RottenMin, int? RottenMax)
        {
            // Nullable conversion workaround
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RottenMin = RottenMin;
            this.RottenMax = RottenMax;
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenMin, RottenMax);
        }
    }
}
