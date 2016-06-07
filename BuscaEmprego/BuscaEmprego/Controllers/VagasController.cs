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
        private int idVaga;

        public ActionResult Buscar(BuscaViewModel vm)
        {
            var buscaBusiness = new VagasBusiness();
            var vagas = buscaBusiness.ListaVagas(vm.TipoVaga, vm.Query);

            if (vagas.Count > 0)
                foreach (var vaga in vagas)
                    vm.Vagas.Add(VagasViewModel.ParseEntityToVaga(vaga));
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
            this.idVaga = idVaga;
            Vaga vaga = VagasBusiness.BuscarVaga(idVaga);

            return View(VagasViewModel.ParseEntityToVaga(vaga));
        }

        public ActionResult ListarCandidatos()
        {
            return View();
        }

        public ActionResult SalvarVaga(VagasViewModel vm)
        {
            if (vm.Salario.Equals("R$ 0,00") || !vm.Salario.Contains("R$"))
                ModelState.AddModelError("Salario", "O Salário deve ser preenchido corretamente. Formato R$ 0,00");

            try
            {
                var vagaBusiness = new VagasBusiness();
                vagaBusiness.RegistraVaga(VagasViewModel.ParseVagaToEntity(vm));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Vagas", e.Message);
            }

            if (!ModelState.IsValid)
            {
                vm.CodigoResultado = CodigoResultadoEnum.ERRO;
                return View("Cadastrar", vm);
            }

            vm = new VagasViewModel();
            vm.CodigoResultado = CodigoResultadoEnum.OK;
            vm.Mensagem = "Vaga cadastrada com sucesso";

            return RedirectToAction("Cadastrar", vm);
        }

        public ActionResult EditarVaga(VagasViewModel vm)
        {
            string str = Request.Params["btnDetalhe"];

            if (str.Equals("Edit") &&
                (vm.Salario.Equals("R$ 0,00") || !vm.Salario.Contains("R$")))
                ModelState.AddModelError("Salario", "O Salário deve ser preenchido corretamente. Formato R$ 0,00");

            if (str.Equals("Edit"))
            {
                try
                {
                    var vagaBusiness = new VagasBusiness();
                    vagaBusiness.EditarVaga(VagasViewModel.ParseVagaToEntityEditar(vm, idVaga));

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vagas", e.Message);
                }

                if (!ModelState.IsValid)
                {
                    vm.CodigoResultado = CodigoResultadoEnum.ERRO;
                    return View("Detalhes", vm);
                }

                vm = new VagasViewModel();
                vm.CodigoResultado = CodigoResultadoEnum.OK;
                vm.Mensagem = "Vaga editada com sucesso";

                return RedirectToAction("Detalhes", vm);

            }
            else if (str.Equals("Remove"))
            {
                try
                {
                    var vagaBusiness = new VagasBusiness();
                    vagaBusiness.RemoverVaga(idVaga);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vagas", e.Message);
                }

                if (!ModelState.IsValid)
                {
                    vm.CodigoResultado = CodigoResultadoEnum.ERRO;
                    return View("Buscar", vm);
                }

                vm = new VagasViewModel();
                vm.CodigoResultado = CodigoResultadoEnum.OK;
                vm.Mensagem = "Vaga excluída com sucesso";

                return RedirectToAction("Buscar", vm);

            }
            else if (str.Equals("Apply"))
            {
                try
                {
                    var vagaBusiness = new VagasBusiness();
                    vagaBusiness.CandidatarVaga(VagasViewModel.ParseVagaToVagaUsuario(vm, idVaga));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Vagas", e.Message);
                }

                if (!ModelState.IsValid)
                {
                    vm.CodigoResultado = CodigoResultadoEnum.ERRO;
                    return View("Detalhes", vm);
                }

                vm = new VagasViewModel();
                vm.CodigoResultado = CodigoResultadoEnum.OK;
                vm.Mensagem = "Candidatura realizada com sucesso";

                return RedirectToAction("Detalhes", vm);
            }

            vm.CodigoResultado = CodigoResultadoEnum.ERRO;
            vm.Mensagem = "Ocorreu um erro";

            return RedirectToAction("Detalhes", vm);
        }
    }
}