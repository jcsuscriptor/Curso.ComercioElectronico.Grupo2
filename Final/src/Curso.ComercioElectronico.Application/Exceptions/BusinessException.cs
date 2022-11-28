using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Curso.ComercioElectronico.Application;
 
    /// <summary>
    /// Exception para lanzar error por el incumplimiento de reglas negocios
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public string FriendlyMessage { get; }
        public BusinessException(string friendlyMessage)
        {
            FriendlyMessage = friendlyMessage;
        }

        public BusinessException(string friendlyMessage, string mensajeTecnico) : base(mensajeTecnico)
        {
            FriendlyMessage = friendlyMessage;
        }

        public BusinessException(string friendlyMessage, string mensajeTecnico, Exception inner) : base(mensajeTecnico, inner)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }


    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }


 