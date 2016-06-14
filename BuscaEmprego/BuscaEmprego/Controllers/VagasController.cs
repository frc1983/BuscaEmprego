using BuscaEmprego.Business;
using BuscaEmprego.Entities;
using BuscaEmprego.Enumerators;
using BuscaEmprego.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaEmprego.Controllers
{
    public class VagasController : Controller
    {
        public ActionResult Buscar(BuscaViewModel vm)
        {
            var buscaBusiness = new VagasBusiness();
            var vagas = buscaBusiness.ListaVagas(vm.TipoVaga, vm.Query);

            if (vagas.Count > 0)
                foreach (var vaga in vagas)
                    vm.Vagas.Add(VagasViewModel.ParseEntityToVaga(vaga, null));
            else
                vm.Mensagem = "Nenhuma vaga foi encontrada com esses parâmetros.";

            return View(vm);
        }

        public ActionResult Cadastrar(VagasViewModel vm)
        {
            if (vm == null)
                vm = new VagasViewModel();

            vm.EmailEmpresa = User.Identity.Name;

            return View(vm);
        }

        public ActionResult Detalhes(int idVaga)
        {
            Vaga vaga = VagasBusiness.BuscarVaga(idVaga);
            List<Perfil> perfis = new PerfilBusiness().BuscarPerfis();

            return View(VagasViewModel.ParseEntityToVaga(vaga, perfis));
        }

        public ActionResult ListarCandidatos()
        {
            return View();
        }

        public ActionResult EditarVaga(VagasViewModel vm)
        {
            string str = Request.Params["btnDetalhe"];
            var vagaBusiness = new VagasBusiness();

            if (vm.Salario.Equals("R$ 0,00") || !vm.Salario.Contains("R$"))
                ModelState.AddModelError("Salario", "O Salário deve ser preenchido corretamente. Formato R$ 0,00");

            try
            {
                if (str.Equals("Editar"))
                {
                    vagaBusiness.EditarVaga(VagasViewModel.ParseVagaToEntityEditar(vm));

                    vm = new VagasViewModel();
                }
                else if (str.Equals("Excluir"))
                    vagaBusiness.RemoverVaga(vm.Id);
                else if (str.Equals("Salvar"))
                    vagaBusiness.RegistraVaga(VagasViewModel.ParseVagaToEntity(vm));

                if (!ModelState.IsValid)
                {
                    vm.CodigoResultado = CodigoResultadoEnum.ERRO;
                    vm.Mensagem = "Ocorreu um erro";
                    return View("Cadastrar", vm);
                }

                vm = new VagasViewModel();
                vm.CodigoResultado = CodigoResultadoEnum.OK;
                vm.Mensagem = "Candidatura realizada com sucesso";

                return View("Cadastrar", vm);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Vagas", e.Message);
                vm.CodigoResultado = CodigoResultadoEnum.ERRO;
                vm.Mensagem = "Ocorreu um erro";
                return View("Cadastrar", vm);
            }
        }
    }
}