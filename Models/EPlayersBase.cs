using System.Collections.Generic;
using System.IO;

namespace E_players.Models {
    public class EplayersBase {
        /// <summary>
        ///  Criar a pasta e o arquivo caso nao exista
        /// </summary>
        /// <param name="_path">caminho do arquivo</param>
        public void CreateFolderAndFile (string _path) {
            {
                string folder = _path.Split ("/") [0];

                if (!Directory.Exists (folder)) {
                    Directory.CreateDirectory (folder);
                }

                if (!File.Exists (_path)) {
                    File.Create (_path).Close ();
                }
            }
        }
         public List<string> ReadAllLinesCSV(string PATH){
            
            List<string> linhas = new List<string>();
            using(StreamReader file = new StreamReader(PATH))
            {
                string linha;
                while((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }
            return linhas;
        }

        /// <summary>
        /// reescreve o csv
        /// </summary>
        /// <param name="PATH">caminho do arquivo </param>
        /// <param name="linhas">linhas para reescrever o arquivo</param>
        public void RewriteCSV(string PATH, List<string> linhas)
        {
            using(StreamWriter output = new StreamWriter(PATH))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
    }
}