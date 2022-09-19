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
    public class BALInstituteList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Institute List Get
        public List<MstInstitute> InstituteListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstInstituteGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute objInstitute = new MstInstitute();

                        objInstitute.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objInstitute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objInstitute.InstituteType = (Convert.ToString(Dt.Rows[i]["InstituteType"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteType"]);
                        objInstitute.InstituteCode = (Convert.ToString(Dt.Rows[i]["InstituteCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        objInstitute.InstituteAddress = (Convert.ToString(Dt.Rows[i]["InstituteAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteAddress"]);
                        objInstitute.CityName = (Convert.ToString(Dt.Rows[i]["CityName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CityName"]);
                        objInstitute.Pincode = (Convert.ToString(Dt.Rows[i]["Pincode"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        objInstitute.InstituteContactNo = (Convert.ToString(Dt.Rows[i]["InstituteContactNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteContactNo"]);
                        objInstitute.InstituteFaxNo = (Convert.ToString(Dt.Rows[i]["InstituteFaxNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteFaxNo"]);
                        objInstitute.InstituteEmail = (Convert.ToString(Dt.Rows[i]["InstituteEmail"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteEmail"]);
                        objInstitute.InstituteUrl = (Convert.ToString(Dt.Rows[i]["InstituteUrl"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteUrl"]);
                        objInstitute.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objInstitute.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        objInstitute.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        if (objInstitute.Pincode == 0) { objInstitute.Pincode = null; }
                        count = count + 1;
                        objInstitute.IndexId = count;
                        ObjLstInstitute.Add(objInstitute);                        
                    }
                }
                return ObjLstInstitute;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Institute List Get For Drop Down
        public List<MstInstitute> InstituteListGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstInstituteGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute objInstitute = new MstInstitute();

                        objInstitute.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objInstitute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objInstitute.InstituteCode = (Convert.ToString(Dt.Rows[i]["InstituteCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteCode"]);                        
                        ObjLstInstitute.Add(objInstitute);
                    }
                }
                return ObjLstInstitute;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Institute List Get By Institute Type
        public List<MstInstitute> InstituteListGetByInstituteType()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstInstituteGetByInstituteType", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute objInstitute = new MstInstitute();

                        objInstitute.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objInstitute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objInstitute.InstituteCode = (Convert.ToString(Dt.Rows[i]["InstituteCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        objInstitute.InstituteType = (Convert.ToString(Dt.Rows[i]["InstituteType"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteType"]);
                        ObjLstInstitute.Add(objInstitute);
                    }
                }
                return ObjLstInstitute;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Institute List Get By Id
        public MstInstitute InstituteGetbyId(int? InstituteId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstInstituteGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstInstitute objInstitute = new MstInstitute();

                    objInstitute.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objInstitute.InstituteName = (Convert.ToString(Dt.Rows[0]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteName"]);
                    objInstitute.InstituteType = (Convert.ToString(Dt.Rows[0]["InstituteType"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteType"]);
                    objInstitute.InstituteCode = (Convert.ToString(Dt.Rows[0]["InstituteCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteCode"]);
                    objInstitute.InstituteAddress = (Convert.ToString(Dt.Rows[0]["InstituteAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteAddress"]);
                    objInstitute.CityName = (Convert.ToString(Dt.Rows[0]["CityName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["CityName"]);
                    objInstitute.Pincode = (Convert.ToString(Dt.Rows[0]["Pincode"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["Pincode"]);
                    objInstitute.InstituteContactNo = (Convert.ToString(Dt.Rows[0]["InstituteContactNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteContactNo"]);
                    objInstitute.InstituteFaxNo = (Convert.ToString(Dt.Rows[0]["InstituteFaxNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteFaxNo"]);
                    objInstitute.InstituteEmail = (Convert.ToString(Dt.Rows[0]["InstituteEmail"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteEmail"]);
                    objInstitute.InstituteUrl = (Convert.ToString(Dt.Rows[0]["InstituteUrl"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteUrl"]);
                    objInstitute.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objInstitute.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    objInstitute.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);

                    return objInstitute;

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

        #region  Institute List Get By Faculty Id and InstituteType
        public List<MstInstitute> InstituteListGetByFacIdAndInsType(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstInstituteGetByFacIdAndInsType", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute objInstitute = new MstInstitute();

                        objInstitute.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objInstitute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        ObjLstInstitute.Add(objInstitute);
                    }
                }
                return ObjLstInstitute;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Institute Get By ExamEvent
        public List<MstInstitute> InstituteGetByExamEvent(int ExamMasterId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("InstituteGetByExamEvent", Con);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute objInstitute = new MstInstitute();

                        objInstitute.FacultyExamId = Convert.ToInt32(Dt.Rows[i]["FacultyExamId"]);
                        objInstitute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);

                        ObjLstInstitute.Add(objInstitute);
                    }
                }
                return ObjLstInstitute;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Institute Get By Branch And PartTerm 
        public List<MstInstitute> InstituteGetForDropDownByPartTermId(int BranchId,int PartTermId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("InstituteGetForDropDownByPartTermId", Con);
                Cmd.Parameters.AddWithValue("@BranchId", BranchId);
                Cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                Dt.Rows.Add("0", "All Institute");
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute objInstitute = new MstInstitute();

                        objInstitute.Id = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objInstitute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);

                        ObjLstInstitute.Add(objInstitute);
                    }
                }
                return ObjLstInstitute;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
        
    }
}