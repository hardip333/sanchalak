using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;


namespace MSUISApi.Controllers
{
    public class RoleController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public HttpResponseMessage createTokenForRole(JObject UserData)
        {
            try
            {
                dynamic Jsondata = UserData;
                String RoleId = Convert.ToString(Jsondata.Id);
                TokenOperation ac = new TokenOperation();
                
                string returnRoleId = ac.CreateRoleToken(RoleId);

                return Return.returnHttp("200", returnRoleId, returnRoleId);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetRoleByUserId(JObject UserData)
        {
            try
            {

                dynamic Jsondata = UserData;
                //string UserId="";
                //String token = Convert.ToString(Jsondata.token);
                String id = Convert.ToString(Jsondata.id);
                //AdminController ac = new AdminController();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                //string res = ac.ValidateToken(UserData, Request);

                /*if (res != "200")
                {
                    id = res;
                }*/
                if (res != "0")
                {
                    id = res;
                }
                else
                {
                    return Return.returnHttp("0", null, null);
                }

                List<MstRole> ObjLstRole = new List<MstRole>();
                SqlCommand Cmd = new SqlCommand("GetRoleByUserId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@Id", id);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstRole objRole = new MstRole();
                        objRole.RoleName = Convert.ToString(Dt.Rows[i]["RoleName"]);
                        objRole.Id = Convert.ToInt64(Dt.Rows[i]["RoleId"]);
                        objRole.DesignationName = Convert.ToString(Dt.Rows[i]["DesignationName"]);
                        
                        Int64 departmentId = 0;
                        if (Dt.Rows[i]["DepartmentId"] is DBNull)
                        {
                            objRole.DepartmentId = departmentId;
                        }
                        else
                        {
                            objRole.DepartmentId = Convert.ToInt64(Dt.Rows[i]["DepartmentId"].ToString());
                        }

                        var departmentName = Convert.ToString(Dt.Rows[i]["DepartmentName"]);
                        if (departmentName is DBNull)
                        {
                            departmentName = "";
                            objRole.DepartmentName = Convert.ToString(departmentName);
                        }
                        else
                        {
                            objRole.DepartmentName = departmentName;
                        }

                        Int64 instituteId = 0;
                        if (Dt.Rows[i]["InstituteId"] is DBNull)
                        {
                            objRole.InstituteId = instituteId;
                        }
                        else
                        {
                            objRole.InstituteId = Convert.ToInt64(Dt.Rows[i]["InstituteId"].ToString());
                        }

                        var instituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        if (instituteName is DBNull)
                        {
                            instituteName = "";
                            objRole.InstituteName = Convert.ToString(instituteName);
                        }
                        else
                        {
                            objRole.InstituteName = instituteName;
                        }

                        Int64 facultyId = 0;
                        if (Dt.Rows[i]["FacultyId"] is DBNull)
                        {
                            objRole.FacultyId = facultyId;
                        }
                        else
                        {
                            objRole.FacultyId = Convert.ToInt64(Dt.Rows[i]["FacultyId"].ToString());
                        }

                        var facultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        if (facultyName is DBNull)
                        {
                            facultyName = "";
                            objRole.FacultyName = Convert.ToString(facultyName);
                        }
                        else
                        {
                            objRole.FacultyName = facultyName;
                        }

                        objRole.NameforDisplay = Convert.ToString(Dt.Rows[i]["NameforDisplay"]);
                        objRole.TypeWithId = Convert.ToString(Dt.Rows[i]["TypeWithId"].ToString() + "?" + objRole.DesignationName + "_" + objRole.NameforDisplay);

                        ObjLstRole.Add(objRole);
                    }
                }
                return Return.returnHttp("200", ObjLstRole, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetPermissionByUserRole(JObject UserData)
        {
            try
            {
                dynamic Jsondata = UserData;
                //string UserId="";
                String UserId = Convert.ToString(Jsondata.UserId);
                String RoleId = Convert.ToString(Jsondata.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                /*TokenController ac = new TokenController();
                string res = ac.ValidateToken(token);*/
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                //string res = ac.ValidateToken(UserData, Request);
                //  string res = ac.ValidateToken(UserData);

                //string returnRoleId = ac.CreateRoleToken(RoleId);

                if (res != "0")
                {
                    UserId = res;
                }
                else
                {
                    return Return.returnHttp("0", null, null);
                }



                List<MstRole> ObjLstRole = new List<MstRole>();

                SqlCommand Cmd = new SqlCommand("GetPermissionByUserRole", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstRole objRole = new MstRole();

                        objRole.Id = Convert.ToInt64(Dt.Rows[i]["RoleId"]);
                        objRole.UserId = Convert.ToString(Dt.Rows[i]["UserId"]);
                        objRole.Icon = Convert.ToString(Dt.Rows[i]["Icon"]);
                        objRole.PageUrl = Convert.ToString(Dt.Rows[i]["PageUrl"]);
                        objRole.DisplayOrder = Convert.ToInt64(Dt.Rows[i]["DisplayOrder"]);
                        objRole.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        objRole.cn = Convert.ToInt64(Dt.Rows[i]["cn"]);
                        objRole.MainId = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objRole.RefId = Convert.ToInt64(Dt.Rows[i]["RefId"]);
                        objRole.ParentName = Convert.ToString(Dt.Rows[i]["ParentName"]);
                        objRole.IsParent = Convert.ToBoolean(Dt.Rows[i]["IsParent"]);
                       // objRole.facultyOrDepId = returnRoleId;

                        ObjLstRole.Add(objRole);
                    }
                }
                return Return.returnHttp("200", ObjLstRole, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
    }
}