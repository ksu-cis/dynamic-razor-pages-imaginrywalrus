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
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// Gets and sets the MPAA rating filters
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// Gets and sets the Genre filters
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? RottenMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? RottenMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, int? RottenMin, int? RottenMax)
        {
            // Nullable conversion workaround
            /*
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
            */
            Movies = MovieDatabase.All;
            // Filter search terms
            if (SearchTerms != null)
            {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));
            }

            // Filter MPAA rating
            if(MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }

            // Filter Genre
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }

            // Filter IMDB rating
            if (IMDBMax != null && IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating != null && movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }

            // Filter Rotten Tomatoes rating
            if (RottenMax != null && RottenMin != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating != null && movie.RottenTomatoesRating >= RottenMin && movie.RottenTomatoesRating <= RottenMax);
            }
        }
    }
}
