using Microsoft.AspNetCore.Mvc;
using MVC.Repositories.Interfaces;
using MVC.ViewModels;

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
            // List action retrieves all Lanche items from the repository and returns them to the view.
            // </summary>
            //var lanches = _lancheRepository.Lanches;

            // <summary>
            // Returns the view with the list of Lanche items.
            // </summary>
            //return View(lanches);

            // <summary>
            // Creates a LancheListViewModel with all Lanche items and a current category message.
            // </summary>
            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = _lancheRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual: Todos os Lanches";
            return View(lancheListViewModel);

        }
    }
}
