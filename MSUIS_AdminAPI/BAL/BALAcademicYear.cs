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
    public class BALAcademicYear
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Academic Year Get All
        public List<AcademicYear> AcademicYearGetAll()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncAcademicYearGetAll", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AcademicYear objAcadYear = new AcademicYear();

                        objAcadYear.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objAcadYear.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        //ObjAY.FromDateView = Convert.ToString((dt.Rows[i]["FromDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        //ObjAY.ToDateView = Convert.ToString((dt.Rows[i]["ToDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        objAcadYear.FromDate = Convert.ToDateTime(Dt.Rows[i]["FromDate"]);
                        objAcadYear.ToDate = Convert.ToDateTime(Dt.Rows[i]["ToDate"]);
                        //objAcadYear.FromDateView = Convert.ToDateTime(Dt.Rows[i]["FromDate"]).ToShortDateString();
                        //objAcadYear.ToDateView = Convert.ToDateTime(Dt.Rows[i]["ToDate"]).ToShortDateString();
                        //objAcadYear.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        //objAcadYear.IsOpen = Convert.ToBoolean(Dt.Rows[i]["IsOpen"]);
                        count = count + 1;
                        objAcadYear.IndexId = count;
                        ObjListAcadYear.Add(objAcadYear);
                    }
                }
                return ObjListAcadYear;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Academic Year Get
        public List<AcademicYear> AcademicYearGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncAcademicYearGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AcademicYear objAcadYear = new AcademicYear();

                        objAcadYear.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objAcadYear.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        //ObjAY.FromDateView = Convert.ToString((dt.Rows[i]["FromDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        //ObjAY.ToDateView = Convert.ToString((dt.Rows[i]["ToDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        objAcadYear.FromDate = Convert.ToDateTime(Dt.Rows[i]["FromDate"]);
                        objAcadYear.ToDate = Convert.ToDateTime(Dt.Rows[i]["ToDate"]);
                        objAcadYear.FromDateView = Convert.ToDateTime(Dt.Rows[i]["FromDate"]).ToShortDateString();
                        objAcadYear.ToDateView = Convert.ToDateTime(Dt.Rows[i]["ToDate"]).ToShortDateString();
                        objAcadYear.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objAcadYear.IsOpen = Convert.ToBoolean(Dt.Rows[i]["IsOpen"]);
                        count = count + 1;
                        objAcadYear.IndexId = count;
                        ObjListAcadYear.Add(objAcadYear);                        
                    }
                }
                return ObjListAcadYear;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Academic Year Get For DropDown
        public List<AcademicYear> AcademicYearGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncAcademicYearGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AcademicYear objAcadYear = new AcademicYear();

                        objAcadYear.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objAcadYear.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);                        
                        ObjListAcadYear.Add(objAcadYear);
                    }
                }
                return ObjListAcadYear;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Academic Year Get By Id
        public AcademicYear AcademicYearGetById(int? AcadYearId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("IncAcademicYearGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", AcadYearId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    AcademicYear objAcadYear = new AcademicYear();

                    objAcadYear.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objAcadYear.AcademicYearCode = Convert.ToString(Dt.Rows[0]["AcademicYearCode"]);                    
                    objAcadYear.FromDate = Convert.ToDateTime(Dt.Rows[0]["FromDate"]);
                    objAcadYear.ToDate = Convert.ToDateTime(Dt.Rows[0]["ToDate"]);
                    objAcadYear.FromDateView = Convert.ToDateTime(Dt.Rows[0]["FromDate"]).ToShortDateString();
                    objAcadYear.ToDateView = Convert.ToDateTime(Dt.Rows[0]["ToDate"]).ToShortDateString();
                    objAcadYear.IsActive = Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objAcadYear.IsOpen = Convert.ToBoolean(Dt.Rows[0]["IsOpen"]);

                    return objAcadYear;

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