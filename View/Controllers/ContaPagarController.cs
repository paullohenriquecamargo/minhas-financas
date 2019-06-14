using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {        
        // GET: ContaPagar
        public ActionResult Index(string pesquisa)
        {
            ContasPagarRepository repository = new ContasPagarRepository();
            List<ContaPagar> contasPagar = repository.ObterTodos(pesquisa);

            ViewBag.ContasPagar = contasPagar;

            return View();
        }

        public ActionResult Cadastro()
        {
            return View();            
        }

        public ActionResult Store(string nome, decimal valor, string tipo, string descricao, string estatus)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Nome = nome;
            contaPagar.Valor = valor;
            contaPagar.Tipo = tipo;
            contaPagar.Descricao = descricao;
            contaPagar.Estatus = estatus;

            ContasPagarRepository repository = new ContasPagarRepository();
            repository.Inserir(contaPagar);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            ContasPagarRepository repository = new ContasPagarRepository();
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContasPagarRepository repository = new ContasPagarRepository();
            ContaPagar contaPagar = repository.ObterPeloId(id);
            ViewBag.ContaPagar = contaPagar;
            return View();

        }

        public ActionResult Update(int id, string nome, decimal valor, string tipo, string descricao, string estatus)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = id;
            contaPagar.Nome = nome;
            contaPagar.Valor = valor;
            contaPagar.Tipo = tipo;
            contaPagar.Descricao = descricao;
            contaPagar.Estatus = estatus;
            ContasPagarRepository repository = new ContasPagarRepository();
            repository.Atualizar(contaPagar);
            return RedirectToAction("Index");
        }
    }
}