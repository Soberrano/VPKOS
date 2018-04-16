﻿using Dapper;
using DarkSide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2.Models
{
    public class DogManager : Manager
    {
        public DogManager(Concrete concrete) : base(concrete) { }

        public async Task<List<Dog>> GetDogs()//хранимая процедура получения всех собак 
        {
            List<Dog> dogs = null;
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                dogs = (await cnt.QueryAsync<Dog>(
                    sql: "getDogs",
                    commandType: CommandType.StoredProcedure
                    )).ToList();
            }
            return dogs;
        }
        public async Task AddDog()
        {
           
            using (var cnt = await Concrete.OpenConnectionAsync())
            {
                await cnt.ExecuteAsync(
                    sql: "addDog",
                    commandType: CommandType.StoredProcedure
                    );
            }
        }

    }
}