using DarkSide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;

namespace WebApplication2.Models
{
    public class ReservationManager : Manager
    {
        public ReservationManager(Concrete concrete) : base(concrete) { }

        public async Task SetEventPricing(int someParam)
        {
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                await cnt.QueryAsync<UserProfile>(
                    sql: "dbo.Get_Some_Procedure",
                    param: new StructuredDynamicParameters(new
                    {
                        someParam
                    }),
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}