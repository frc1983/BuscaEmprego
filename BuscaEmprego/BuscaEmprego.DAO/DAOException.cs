using System;
using System.Runtime.Serialization;

namespace BuscaEmprego.DAO
{
    [Serializable]
    public class DAOException : Exception
    {
        public DAOException()
        {
        }

        public DAOException(string message) : base(message)
        {
        }

        public DAOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DAOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
