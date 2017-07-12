using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using TPRM.Application.Interface;
using TPRM.Domain.Entities;
using TPRM.MVC.ViewModels;

namespace TPRM.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class EmpresasController : Controller
    {

        private readonly IEmpresaAppService _empresaApp;
        private readonly IServicoAppService _servicoApp;

        public EmpresasController(IEmpresaAppService empresaApp, IServicoAppService servicoApp)
        {
            _empresaApp = empresaApp;
            _servicoApp = servicoApp;
        }

        // GET: Empresas
        //public ActionResult Index()
        //{
        //    var empresaViewModel = Mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaViewModel>>(_empresaApp.GetAll());
        //    return View(empresaViewModel);
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var empresaViewModel = Mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaViewModel>>(_empresaApp.GetAll());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = string.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.TipoParam = string.IsNullOrEmpty(sortOrder) ? "Tipo_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                empresaViewModel = empresaViewModel.Where(p => p.EmpresaNome.ToUpper().Contains(searchString.ToUpper()) ||
                                                                   p.EmpresaTipo.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Nome_desc":
                    empresaViewModel = empresaViewModel.OrderByDescending(s => s.EmpresaNome);
                    break;
                case "Tipo_desc":
                    empresaViewModel = empresaViewModel.OrderByDescending(s => s.EmpresaTipo);
                    break;
                default:
                    empresaViewModel = empresaViewModel.OrderBy(p => p.ValorDevido);
                    break;
            }

            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(empresaViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int id)
        {
            var empresa = _empresaApp.GetById(id);
            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);

            return View(empresaViewModel);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaTipo = new SelectList(new EmpresaViewModel.Tipo().ListaTipos(), "TipoId", "Nome");
            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico");
            return View();
        }

        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpresaViewModel empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaDomain = Mapper.Map<EmpresaViewModel, Empresa>(empresa);
                _empresaApp.Add(empresaDomain);
                return RedirectToAction("Index");
            }

            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int id)
        {
            var empresa = _empresaApp.GetById(id);
            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);

            ViewBag.EmpresaTipo = new SelectList(_empresaApp.GetAll(), "EmpresaTipo", "EmpresaTipo", empresaViewModel.EmpresaTipo);
            ViewBag.EmpresaTipo = new SelectList(new EmpresaViewModel.Tipo().ListaTipos(), "TipoId", "Nome", empresaViewModel.EmpresaTipo);

            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico", empresaViewModel.ServicoId);

            return View(empresaViewModel);
        }

        // POST: Empresas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpresaViewModel empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaDomain = Mapper.Map<EmpresaViewModel, Empresa>(empresa);
                _empresaApp.Update(empresaDomain);

                return RedirectToAction("Index");
            }

            ViewBag.EmpresaTipo = new SelectList(new EmpresaViewModel.Tipo().ListaTipos(), "TipoId", "Nome");
            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico");
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int id)
        {
            var empresa = _empresaApp.GetById(id);
            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);

            return View(empresaViewModel);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var empresa = _empresaApp.GetById(id);
            _empresaApp.Remove(empresa);

            return RedirectToAction("Index");
        }
    }
}
