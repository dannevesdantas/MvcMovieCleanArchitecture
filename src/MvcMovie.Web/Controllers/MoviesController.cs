using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.ApplicationCore.Entities;
using MvcMovie.ApplicationCore.Interfaces;
using MvcMovie.Infrastructure.Data;
using MvcMovie.Web.Models;

namespace MvcMovie.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            var movies = new List<Movie>();

            if (!String.IsNullOrEmpty(searchString))
            {
                movies.AddRange(_movieRepository.SearchByTitle(searchString).ToList());
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies.AddRange(_movieRepository.SearchByGenre(movieGenre));
            }

            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(movieGenre))
            {
                movies = _movieRepository.ListAll().ToList();
            }

            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.genres = new SelectList(_movieRepository.GetAllGenres());
            movieGenreVM.movies = movies;

            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieRepository.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Genre,Price,Rating,Id")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieRepository.Add(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieRepository.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ReleaseDate,Genre,Price,Rating,Id")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _movieRepository.Update(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieRepository.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = _movieRepository.GetById(id);
            _movieRepository.Delete(movie);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            var movie = _movieRepository.GetById(id);
            return (movie == null) ? false : true; 
        }
    }
}
