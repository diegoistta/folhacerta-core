using System.Collections.Generic;

namespace FolhaCerta.Model.ModelData
{
    public class Response
    {
        public bool Error { get; private set; }

        public IList<string> Messages { get; private set; }

        public dynamic Data { get; set; }

        public string Title => this.Error ? "Erro" : "Sucesso";

        public void AddMessage(bool error, string message)
        {
            this.Error = error;
            if (this.Messages == null)
            {
                this.Messages = new List<string>();
            }

            this.Messages.Add(message);
        }
    }
}