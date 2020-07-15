using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_players.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_players.Controllers {
    public class EquipeController : Controller {

        Equipe equipeModel = new Equipe ();
        /// <summary>
        /// aponta para a index da minha vier
        /// </summary>
        /// <returns>a propria view da index</returns>
        public IActionResult Index () {
            ViewBag.Equipes = equipeModel.ReadAll ();
            return View ();
        }

        /// <summary>
        /// cadastra o id, nome do jogador e imagem do jogador
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public IActionResult Cadastrar (IFormCollection form) {
            Equipe equipe   = new Equipe ();
            equipe.IdEquipe = Int32.Parse (form["IdEquipe"]);
            equipe.Nome     = form["Nome"];
            //Upload da imagem
            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }
                // arquivo.pdf
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                equipe.Imagem   = file.FileName;
            }
            else
            {
                equipe.Imagem   = "padrao.png";
            }
            // fim - upload imagem

            equipeModel.Create (equipe);

            return LocalRedirect("~/Equipe");
        }
        [Route("[controller]/{id}")]
        /// <summary>
        /// exclui a as informações do jogador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);
            return LocalRedirect("~/Equipe");
        }

    }
}