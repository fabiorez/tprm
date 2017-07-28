using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using TPRM.Application.Interface;
using TPRM.Domain.Entities;
using TPRM.MVC.ViewModels;

namespace TPRM.MVC.Controllers
{
    public class TransacoesController : Controller
    {
        private readonly ITransacaoAppService _transacaoApp;
        private readonly IEmpresaAppService _empresaApp;
        private readonly IServicoAppService _servicoApp;

        public TransacoesController(IEmpresaAppService empresaApp, IServicoAppService servicoApp, ITransacaoAppService transacaoApp)
        {
            _empresaApp = empresaApp;
            _servicoApp = servicoApp;
            _transacaoApp = transacaoApp;
        }

        [Authorize(Roles = "cliente, admin, analista")]
        // GET: Transacao
        //public ActionResult Index()
        //{
        //    var transacaoViewModel = Mapper.Map<IEnumerable<Transacao>, IEnumerable<TransacaoViewModel>>(_transacaoApp.GetAll());
        //    return View(transacaoViewModel);
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var transacaoViewModel = Mapper.Map<IEnumerable<Transacao>, IEnumerable<TransacaoViewModel>>(_transacaoApp.GetAll());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.EmpresaContratadaParam = string.IsNullOrEmpty(sortOrder) ? "EmpresaContratada_desc" : "";
            ViewBag.EmpresaContratanteParam = string.IsNullOrEmpty(sortOrder) ? "EmpresaContratante_desc" : "";
            ViewBag.StatusParam = string.IsNullOrEmpty(sortOrder) ? "Status_desc" : "";

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
                transacaoViewModel = transacaoViewModel.Where(p => p.NomeEmpresaContratada.ToUpper().Contains(searchString.ToUpper()) ||
                                             p.NomeEmpresaContratante.ToUpper().Contains(searchString.ToUpper()) ||
                                             p.Status.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "EmpresaContratada_desc":
                    transacaoViewModel = transacaoViewModel.OrderByDescending(s => s.NomeEmpresaContratada);
                    break;
                case "EmpresaContratante_desc":
                    transacaoViewModel = transacaoViewModel.OrderByDescending(s => s.NomeEmpresaContratante);
                    break;
                case "Status_desc":
                    transacaoViewModel = transacaoViewModel.OrderByDescending(s => s.Status);
                    break;
                default:
                    transacaoViewModel = transacaoViewModel.OrderBy(p => p.DataCadastro);
                    break;
            }

            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(transacaoViewModel.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "cliente, admin, analista")]
        // GET: Transacao/Details/5
        public ActionResult Details(int id)
        {
            var transacao = _transacaoApp.GetById(id);
            var transacaoViewModel = Mapper.Map<Transacao, TransacaoViewModel>(transacao);

            return View(transacaoViewModel);
        }
        
        [Authorize(Roles = "cliente, admin")]
        // GET: Transacao/Create
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(new TransacaoViewModel.StatusTipo().ListaStatus(), "StatusId", "Nome");
            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico");
            ViewBag.EmpresaContratanteId = new SelectList(_empresaApp.GetAll().Where(p => p.EmpresaTipo == "Contratante"), "EmpresaId", "EmpresaNome");
            ViewBag.EmpresaContratadaId = new SelectList(_empresaApp.GetAll().Where(p => p.EmpresaTipo == "Contratado"), "EmpresaId", "EmpresaNome");
            return View();
        }

        [Authorize(Roles = "cliente, admin")]
        // POST: Transacao/Create
        [HttpPost]
        public ActionResult Create(TransacaoViewModel transacao)
        {
            if (ModelState.IsValid)
            {
                if (transacao.Files != null)
                {
                    HttpPostedFileBase files = transacao.Files;

                    var transacaoDomain = Mapper.Map<TransacaoViewModel, Transacao>(transacao);

                    var extension = Path.GetExtension(files.FileName);
                    if (extension != null)
                    {
                        String fileExt = extension.ToUpper();

                        if (fileExt == ".PDF")
                        {
                            Stream str = files.InputStream;
                            BinaryReader br = new BinaryReader(str);
                            Byte[] fileDet = br.ReadBytes((Int32) str.Length);

                            transacaoDomain.FileName = files.FileName;
                            transacaoDomain.File = fileDet;
                            transacaoDomain.NomeEmpresaContratada = _empresaApp.GetById(transacao.EmpresaContratadaId).EmpresaNome;
                            transacaoDomain.NomeEmpresaContratante = _empresaApp.GetById(transacao.EmpresaContratanteId).EmpresaNome;
                            _transacaoApp.Add(transacaoDomain);

                            var empresaDomain = _empresaApp.GetById(transacaoDomain.EmpresaContratanteId);
                            empresaDomain.ValorDevido = empresaDomain.ValorDevido + 5;
                            _empresaApp.Update(empresaDomain);

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.FileStatus = "Formato de arquivo inválido";
                        }
                    }
                }
                else
                {
                    ViewBag.FileStatus = "Escolha um arquivo pdf";
                }
            }

            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico");
            ViewBag.EmpresaContratanteId = new SelectList(_empresaApp.GetAll().Where(p => p.EmpresaTipo == "Contratante"), "EmpresaId", "EmpresaNome");
            ViewBag.EmpresaContratadaId = new SelectList(_empresaApp.GetAll().Where(p => p.EmpresaTipo == "Contratado"), "EmpresaId", "EmpresaNome");

            return View(transacao);
        }

        [Authorize(Roles = "analista, admin")]
        // GET: Transacao/Edit/5
        public ActionResult Edit(int id)
        {
            var transacao = _transacaoApp.GetById(id);
            var transacaoViewModel = Mapper.Map<Transacao, TransacaoViewModel>(transacao);

            ViewBag.Status = new SelectList(new TransacaoViewModel.StatusTipo().ListaStatus(), "StatusId", "Nome", transacaoViewModel.Status);
            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico", transacaoViewModel.ServicoId);
            //ViewBag.EmpresaId = new SelectList(_empresaApp.GetAll(), "EmpresaId", "EmpresaNome", transacaoViewModel.EmpresaId);

            return View(transacaoViewModel);
        }

        [Authorize(Roles = "analista, admin")]
        // POST: Transacao/Edit/5
        [HttpPost]
        public ActionResult Edit(TransacaoViewModel transacao)
        {
            if (ModelState.IsValid)
            {
                var transacaoDomain = Mapper.Map<TransacaoViewModel, Transacao>(transacao);
                _transacaoApp.Update(transacaoDomain);

                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(new TransacaoViewModel.StatusTipo().ListaStatus(), "StatusId", "Nome", transacao.Status);
            ViewBag.ServicoId = new SelectList(_servicoApp.GetAll(), "ServicoId", "DescricaoServico", transacao.ServicoId);
            //ViewBag.EmpresaId = new SelectList(_empresaApp.GetAll(), "EmpresaId", "EmpresaNome", transacao.EmpresaId);

            return View(transacao);
        }

        [Authorize(Roles = "admin")]
        // GET: Transacao/Delete/5
        public ActionResult Delete(int id)
        {
            var transacao = _transacaoApp.GetById(id);
            var transacaoViewModel = Mapper.Map<Transacao, TransacaoViewModel>(transacao);

            return View(transacaoViewModel);
        }

        [Authorize(Roles = "admin")]
        // POST: Transacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var transacao = _transacaoApp.GetById(id);
            _transacaoApp.Remove(transacao);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "analista, admin")]
        public ActionResult Download(int id)
        {
            var pdf = _transacaoApp.GetById(id); //_pdfUploadContext.FilesContainer.FirstOrDefault(p => p.Id == id);
            return File(pdf.File, "application/pdf", pdf.FileName);
        }
    }
}
