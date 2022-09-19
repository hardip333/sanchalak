using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MSUISApi.BAL
{
    public class BALDepartmentList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Department List
        public List<MstDepartment> DepartmentListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstDepartmentGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstDepartment> ObjLstDepartment = new List<MstDepartment>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstDepartment objDepartment = new MstDepartment();

                        objDepartment.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objDepartment.DepartmentName = (Convert.ToString(Dt.Rows[i]["DepartmentName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["DepartmentName"]);
                        objDepartment.DepartmentCode = (Convert.ToString(Dt.Rows[i]["DepartmentCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["DepartmentCode"]);
                        objDepartment.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objDepartment.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objDepartment.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objDepartment.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objDepartment.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objDepartment.IndexId = count;
                        ObjLstDepartment.Add(objDepartment);
                    }
                }
                return ObjLstDepartment;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Department By Id
        public MstDepartment DepartmentGetbyId(int? DepartmentId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstDepartmentGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    MstDepartment objDepartment = new MstDepartment();

                    objDepartment.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objDepartment.DepartmentName = (Convert.ToString(Dt.Rows[0]["DepartmentName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["DepartmentName"]); 
                    objDepartment.DepartmentCode = (Convert.ToString(Dt.Rows[0]["DepartmentCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["DepartmentCode"]);
                    objDepartment.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objDepartment.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objDepartment.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    objDepartment.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objDepartment.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return objDepartment;

                }
                else
                    return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Department Get By FacultyId
        public List<MstDepartment> MstDepartmentGetbyFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstDepartmentGetByFacultyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstDepartment> ObjLstMstDepartment = new List<MstDepartment>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstDepartment ObjMstDepartment = new MstDepartment();

                        ObjMstDepartment.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstDepartment.DepartmentName = (Convert.ToString(Dt.Rows[i]["DepartmentName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["DepartmentName"]);
                        ObjMstDepartment.DepartmentCode = (Convert.ToString(Dt.Rows[i]["DepartmentCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["DepartmentCode"]);
                        ObjMstDepartment.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstDepartment.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstDepartment.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstDepartment.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstDepartment.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstDepartment.Add(ObjMstDepartment);
                    }

                }

                return ObjLstMstDepartment;


            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion
    }
}