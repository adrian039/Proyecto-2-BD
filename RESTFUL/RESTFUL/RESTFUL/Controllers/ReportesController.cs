﻿using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


    namespace RESTFUL.Controllers
    {
        public class ReportesController : ApiController
        {

            JSONSerializer serial = new JSONSerializer();

            ///<sumary>
            ///Retorna el top de ventas entre un rango de fechas
            ///</sumary>
            [HttpGet]
            public HttpResponseMessage topSalesByDate(string date1, string date2)
            {
                try
                {
                    using (gspEntity entities = new gspEntity())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                        conn.Open();
                        using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM topsalesbydate(:P0, :P1); COMMIT;", conn))
                        {
                            com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Date));
                            com.Parameters.Add(new NpgsqlParameter("P1", NpgsqlDbType.Date));
                            com.Prepare();
                            com.Parameters[0].Value = date1;
                            com.Parameters[1].Value = date2;
                            using (NpgsqlDataReader dr = com.ExecuteReader())
                            {
                                DataTable table = new DataTable();

                                table.Load(dr);
                                ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes/topSalesbyDates.rpt";
                                crystalReport.Load(strRptPath); // path of report 
                                table.Columns.Add("initDate");
                                table.Columns.Add("finalDate");
                            foreach (DataRow row in table.Rows)
                                {
                                    row["initDate"] =date1;
                                    row["finalDate"] = date2;
                            }
                                crystalReport.SetDataSource(table);
                                crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                                System.Web.HttpContext.Current.Response, false, "topSales");
                            return Request.CreateResponse(HttpStatusCode.OK, System.Web.HttpContext.Current.Response);
                        }

                        };

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            ///<sumary>
            ///Retorna el top de ventas de una sucursal
            ///</sumary>
            [HttpGet]
            public HttpResponseMessage topSucursalSales(int suc)
            {
                try
                {
                    using (gspEntity entities = new gspEntity())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                        conn.Open();
                        using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM topsucursalsales(:P0); COMMIT;", conn))
                        {
                            com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Integer));
                            com.Prepare();
                            com.Parameters[0].Value = suc;
                            using (NpgsqlDataReader dr = com.ExecuteReader())
                            {
                            DataTable table = new DataTable();
                            table.Load(dr);
                            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes/topSucursalSales.rpt";
                            crystalReport.Load(strRptPath); // path of report 
                            table.Columns.Add("storeID");
                            foreach (DataRow row in table.Rows)
                            {
                                row["storeID"] = suc;
                            }
                            crystalReport.SetDataSource(table);
                            var result = serial.Serialize(dr);
                            conn.Close();
                            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                            System.Web.HttpContext.Current.Response, false, "topSales" +
                            "");
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                        };

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            ///<sumary>
            ///Retorna el top de ventas de un cajero
            ///</sumary>
            [HttpGet]
            public HttpResponseMessage topEmpleadoSales(int empl)
            {
                try
                {
                    using (gspEntity entities = new gspEntity())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                        conn.Open();
                        using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM topempleadosales(:P0); COMMIT;", conn))
                        {
                            com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Integer));
                            com.Prepare();
                            com.Parameters[0].Value = empl;
                            using (NpgsqlDataReader dr = com.ExecuteReader())
                            {
                            DataTable table = new DataTable();
                            table.Load(dr);
                            table.Columns.Add("cedula");
                            foreach (DataRow row in table.Rows) {
                                row["cedula"] = empl;
                            }
                            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes/topEmpleadoSales.rpt";
                            crystalReport.Load(strRptPath); // path of report 
                            //crystalReport.SetParameterValue("id", empl);
                            crystalReport.SetDataSource(table);
                            var result = serial.Serialize(dr);
                            conn.Close();
                            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                            System.Web.HttpContext.Current.Response, false, "response.pdf");
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                        };

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            ///<sumary>
            ///Retorna los productos con bajo inventario, tomando bajo como meno o igual a 5
            ///</sumary>
            [HttpGet]
            public HttpResponseMessage lowQuantity()
            {
                try
                {
                    using (gspEntity entities = new gspEntity())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                        conn.Open();
                        using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM lowquantity(); COMMIT;", conn))
                        {
                            using (NpgsqlDataReader dr = com.ExecuteReader())
                            {
                            DataTable table = new DataTable();
                            table.Load(dr);
                            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes/lowStock.rpt";
                            crystalReport.Load(strRptPath); // path of report 
                            crystalReport.SetDataSource(table);
                            var result = serial.Serialize(dr);
                            conn.Close();
                            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                            System.Web.HttpContext.Current.Response, false, "response.pdf");
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                        };

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            ///<sumary>
            ///Retorna el tiempo promedio que dura un cajero atendiendo un cliente durante el dia
            ///</sumary>
            [HttpGet]
            public HttpResponseMessage promTimeEmpleado(string date)
            {
                try
                {
                    using (gspEntity entities = new gspEntity())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                        conn.Open();
                        using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM promtimeempleado(:P0); COMMIT;", conn))
                        {
                            com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Date));
                            com.Prepare();
                            com.Parameters[0].Value = date;
                            using (NpgsqlDataReader dr = com.ExecuteReader())
                            {
                            DataTable table = new DataTable();
                            table.Load(dr);
                            table.Columns.Add("date");
                            foreach (DataRow row in table.Rows)
                            {
                                row["date"] = date;
                            }
                            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes/empAverage.rpt";
                            crystalReport.Load(strRptPath); // path of report 
                            crystalReport.SetDataSource(table);
                            var result = serial.Serialize(dr);
                            conn.Close();
                            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                            System.Web.HttpContext.Current.Response, false, "response.pdf");
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                        };

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            /// <summary>
            ///Retorna las ventas del dia
            /// </summary>
            [HttpGet]
            public HttpResponseMessage getDaySales(string fecha)
            {
                try
                {
                    using (gspEntity entities = new gspEntity())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(entities.Database.Connection.ConnectionString);
                        conn.Open();
                        using (NpgsqlCommand com = new NpgsqlCommand("BEGIN; SELECT * FROM getDaySales(:P0); COMMIT;", conn))
                        {
                            com.Parameters.Add(new NpgsqlParameter("P0", NpgsqlDbType.Date));
                            com.Prepare();
                            com.Parameters[0].Value = fecha;
                            using (NpgsqlDataReader dr = com.ExecuteReader())
                            {
                            DataTable table = new DataTable();
                            table.Load(dr);
                            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
                            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes/bills.rpt";
                            crystalReport.Load(strRptPath); // path of report 
                            crystalReport.SetDataSource(table);
                            var result = serial.Serialize(dr);
                            conn.Close();
                            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                            System.Web.HttpContext.Current.Response, false, "response.pdf");
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                        };

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }


