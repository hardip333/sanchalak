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
    public class BALFacultyList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Faculty List
        public List<MstFaculty> FacultyListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                Int32 count = 0;
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = (Convert.ToString(Dt.Rows[i]["FacultyCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyCode"]);                        
                        objFaculty.FacultyAddress = (Convert.ToString(Dt.Rows[i]["FacultyAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyAddress"]);                        
                        objFaculty.CityName = (Convert.ToString(Dt.Rows[i]["CityName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CityName"]);                        
                        objFaculty.Pincode = (Convert.ToString(Dt.Rows[i]["Pincode"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Pincode"]);                        
                        objFaculty.FacultyContactNo = (Convert.ToString(Dt.Rows[i]["FacultyContactNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);                        
                        objFaculty.FacultyFaxNo = (Convert.ToString(Dt.Rows[i]["FacultyFaxNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);                        
                        objFaculty.FacultyEmail = (Convert.ToString(Dt.Rows[i]["FacultyEmail"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyEmail"]);                       
                        objFaculty.FacultyUrl = (Convert.ToString(Dt.Rows[i]["FacultyUrl"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyUrl"]);                        
                        objFaculty.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objFaculty.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        if (objFaculty.Pincode == 0) { objFaculty.Pincode = null; }
                        count = count + 1;
                        objFaculty.IndexId = count;
                        ObjLstFaculty.Add(objFaculty);                        
                    }
                }
                return ObjLstFaculty;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Faculty List For Drop Down
        public List<MstFaculty> FacultyListGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = (Convert.ToString(Dt.Rows[i]["FacultyCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        ObjLstFaculty.Add(objFaculty);
                    }
                }
                return ObjLstFaculty;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Faculty List Get By Id
        public MstFaculty FacultyGetbyId(int? FacultyId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstFacultyGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", FacultyId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstFaculty objFaculty = new MstFaculty();

                    objFaculty.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objFaculty.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objFaculty.FacultyCode = (Convert.ToString(Dt.Rows[0]["FacultyCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyCode"]);
                    objFaculty.FacultyAddress = (Convert.ToString(Dt.Rows[0]["FacultyAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyAddress"]);
                    objFaculty.CityName = (Convert.ToString(Dt.Rows[0]["CityName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["CityName"]);
                    objFaculty.Pincode = (Convert.ToString(Dt.Rows[0]["Pincode"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["Pincode"]);
                    objFaculty.FacultyContactNo = (Convert.ToString(Dt.Rows[0]["FacultyContactNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyContactNo"]);
                    objFaculty.FacultyFaxNo = (Convert.ToString(Dt.Rows[0]["FacultyFaxNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyFaxNo"]);
                    objFaculty.FacultyEmail = (Convert.ToString(Dt.Rows[0]["FacultyEmail"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyEmail"]);
                    objFaculty.FacultyUrl = (Convert.ToString(Dt.Rows[0]["FacultyUrl"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyUrl"]);
                    objFaculty.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);                    
                    if (objFaculty.Pincode == 0) { objFaculty.Pincode = null; }

                    return objFaculty;

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
    }
}