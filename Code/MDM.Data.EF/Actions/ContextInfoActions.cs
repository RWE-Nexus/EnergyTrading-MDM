namespace EnergyTrading.Mdm.Data.EF.Actions
{
    using System.Data;

    using EnergyTrading;
    using EnergyTrading.Data;

    public static class ContextInfoActions
    {
        private const string CommandText =
            "declare @length tinyint\n" +
            "declare @ctx varbinary(128)\n" +
            "select @length = len(@data)\n" +
            "select @ctx = convert(binary(1), @length) + " + "convert(binary (127), @data)\n" +
            "set context_info @ctx";

        public static void SetContextUserInfo(IDao dao)
        {
            var userName = ContextInfoProvider.GetUserName();

            if (string.IsNullOrWhiteSpace(userName))
            {
                return;
            }

            var conn = dao.Connection;
            var initialState = conn.State;
            //try
            //{
                if (initialState != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = CommandText;
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@data";
                    param.DbType = DbType.AnsiString;
                    param.Size = 127;
                    param.Value = userName;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                }
            //}
            //finally
            //{
            //    // TODO: if we close the connection, context_info is also removed -- confirm
            //    if (initialState != ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //}
        }
    }
}