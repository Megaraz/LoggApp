using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.DTOs
{
    public interface IPromptRenderable
    {
        /// <summary>Returnerar en kort text som kan sättas in i en prompt.</summary>
        string ToPrompt();
    }
}
