using Microsoft.AspNetCore.Mvc;
using MVC.Repositories.Interfaces;

namespace MVC.Controllers
{
    public class LancheController : Controller
    {
        // <summary>
        // LancheController is responsible for handling requests related to "Lanche" (snack) items.
        // It interacts with the ILancheRepository to retrieve and manage Lanche data.
        // </summary>
        private readonly ILancheRepository _lancheRepository;

        // <summary>
        // Constructor for LancheController.
        // Initializes the controller with an ILancheRepository instance.
        // </summary>
        public LancheController(ILancheRepository lancheRepository) 
        {
            _lancheRepository = lancheRepository;
        }


        public IActionResult List()
        {
            // <summary> 
            // List action prepares the view data for displaying available Lanche items.
            // It sets the title and current date in the ViewData dictionary.
            // </summary>
            ViewData["Titulo"] = "Lanches Disponíveis";
            ViewData["Data"] = DateTime.Now;

            // <summary>
            // List action retrieves all Lanche items from the repository and returns them to the view.
            // </summary>
            var lanches = _lancheRepository.Lanches;

            // <summary>
            // Sets the total number of Lanche items in the ViewBag for display in the view.
            // </summary>
            ViewBag.Total = "Total de lanches";
            ViewBag.TotalLanches = lanches.Count();

            // <summary>
            // Returns the view with the list of Lanche items.
            // </summary>
            return View(lanches);
        }
    }
}
