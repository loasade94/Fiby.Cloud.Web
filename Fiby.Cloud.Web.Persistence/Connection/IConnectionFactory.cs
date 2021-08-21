using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Fiby.Cloud.Web.Persistence.Connection
{
    public interface IConnectionFactory
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}
