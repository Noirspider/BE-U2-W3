﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Class;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class ProdottoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdottoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prodotto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prodotti.ToListAsync());
        }

        // GET: Prodotto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context
                .Prodotti.Include(p => p.IngredientiAggiunti)
                .ThenInclude(i => i.Ingrediente)
                .FirstOrDefaultAsync(m => m.IdProdotto == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // GET: Prodotto/Create
        [HttpGet]
        public IActionResult Create()
        {
            var listaIngredienti = _context.Ingrediente.ToList();
            ViewBag.ListaIngredienti = listaIngredienti;

            return View();
        }

        // POST: Prodotto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prodotto prodotto)
        {
            ModelState.Remove("ProdottiAcquistati");
            ModelState.Remove("IngredientiAggiunti");

            if (ModelState.IsValid)
            {
                _context.Prodotti.Add(prodotto);
                await _context.SaveChangesAsync();

                if (prodotto.IngredientiAggiuntiHidden != null)
                {
                    var listaIngredienti = prodotto.IngredientiAggiuntiHidden.Split(",");
                    foreach (var ingrediente in listaIngredienti)
                    {
                        var ingredienteAggiunto = new IngredienteAggiunto
                        {
                            IdProdotto = prodotto.IdProdotto,
                            IdIngrediente = int.Parse(ingrediente)
                        };
                        _context.IngredienteAggiunto.Add(ingredienteAggiunto);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            var lista = _context.Ingrediente.ToList();
            ViewBag.ListaIngredienti = lista;
            return View(prodotto);
        }

        // GET: Prodotto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        // GET: Prodotto/
        public async Task<IActionResult> AggiungiIngrediente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }

            var listaIngredienti = _context.Ingrediente.ToList();
            ViewBag.ListaIngredienti = listaIngredienti;

            return View(prodotto);
        }

        // POST: Prodotto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProdotto,NomeProdotto,ImgProdotto,PrezzoProdotto,TempoConsegna")] Prodotto prodotto)
        {
            if (id != prodotto.IdProdotto)
            {
                return NotFound();
            }

            ModelState.Remove("Prodotto");
            ModelState.Remove("Ingrediente");
            ModelState.Remove("ProdottiAcquistati");
            ModelState.Remove("IngredientiAggiunti");
            ModelState.Remove("IngredientiAggiuntiHidden");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.IdProdotto))
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
            else
            {
                //  return BadRequest();
                // Ottieni gli errori di validazione dal ModelState
                var erroriValidazione = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                // Costruisci un messaggio di errore che mostra il contenuto del ModelState
                var messaggioErrore = string.Join(", ", erroriValidazione);

                // Restituisci un messaggio di errore con il contenuto del ModelState
                return BadRequest(messaggioErrore);

            }
        }

        // GET: Prodotto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FirstOrDefaultAsync(m => m.IdProdotto == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // POST: Prodotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto != null)
            {
                _context.Prodotti.Remove(prodotto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottoExists(int id)
        {
            return _context.Prodotti.Any(e => e.IdProdotto == id);
        }
    }
}


