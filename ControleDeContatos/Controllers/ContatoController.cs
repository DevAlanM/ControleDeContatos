using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    //ContatoController irá herdar metodos de Controller
    public class ContatoController : Controller
    {
        //irá gerar uma classe de interface privada somente para leitura herdando o IContatoRepositorio da Interface
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            //Nessa ação irá listar todos os contatos informados na lista utilizando o metodo BuscarTodos(), retornando a View Contatos
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            // é gerado uma variavel ContatoModel com o nome de contato que recebe o valor de ListarPor Id, pegando as informações do contato
            ContatoModel contato = _contatoRepositorio.ListarPorId(id); 
            return View(contato);

        }
        public IActionResult Apagar(int id) // essa ação irá execultar a exclusão através do id, adquirido ao novo contato
        {
            _contatoRepositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            //Condição que irá validar ao clicar no adicionar será criado um novo contato
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index"); //irá ser retornado a pagina inicial dos contatos
                }
                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemSucesso"] = $"Ops, não conseguimos cadastrar o seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index"); 
                throw;
            }
        }


        [HttpPost] //definido o metodo post para coletar os dados e alterar-los
        public IActionResult Alterar(ContatoModel contato)
        {
            // essa condição irá validar se o botao de atualizar foi clicado e caso for, irá realizar a alteração, que está informada nas abas de interface da classe IContatoRepositorio
            if (ModelState.IsValid)
            { 
            _contatoRepositorio.Atualizar(contato);
            return RedirectToAction("Index"); 
            }
            //irá retornar a pagina de editar.cshtml, informando os dados do contato, caso não seja clicado em alterar
        return View("Editar", contato);
        }
    }

}
