using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CrudAPINet7.Models;
using System.Data.Common;


namespace CrudAPINet7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Puestos : ControllerBase
    {

        #region conf

        private readonly IConfiguration conf;

        public Puestos(IConfiguration config)
        {
            conf = config;
        }

        #endregion conf


        #region get
        [HttpGet]
        public async Task<IActionResult> GetPuestos()
        {
            using (var connection = new MySqlConnection(conf.GetConnectionString("dbconnection")))
            {
                var query = @"SELECT * FROM puestos where estado <> 0";
                var result = await connection.QueryAsync(query);
                    return Ok(result);
            }
        }
        #endregion get

        #region post
        [HttpPost]
        public async Task<IActionResult> PostPuestos(Models.Puestos model)
        {
            using (var connection = new MySqlConnection(conf.GetConnectionString("dbconnection")))
            {
                string query = "INSERT INTO puestos (nombre_puesto) VALUES (@nombre_puesto)";
                var result = await connection.QueryAsync(query, model);
                return Ok(new { message = "Puesto creado exitosamente" });
            }
        }
        #endregion post

        #region put
        [HttpPut]
        public async Task<IActionResult> PutPuestos(Models.Puestos model)
        {
            using (var connection = new MySqlConnection(conf.GetConnectionString("dbconnection")))
            {
                string query = @"UPDATE dbempleados.puestos
                SET nombre_puesto=@nombre_puesto WHERE id_puesto=@id_puesto;";
                var result = await connection.QueryAsync(query, model);
                return Ok(new { message = "Puesto modificado exitosamente" });
            }
        }
        #endregion put


        #region delete
        [HttpDelete]
        public async Task<IActionResult> DeletePuestos(Models.Puestos model)
        {
            using (var connection = new MySqlConnection(conf.GetConnectionString("dbconnection")))
            {
                string query = @"UPDATE dbempleados.puestos
                SET estado=0 WHERE id_puesto=@id_puesto;";
                var result = await connection.QueryAsync(query, model);
                return Ok(new { message = "Puesto eliminado exitosamente" });
            }
        }
        #endregion delete
    }
}
