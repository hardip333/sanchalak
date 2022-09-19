using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class AddOnInformationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

      

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeInstancePartTermGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyFacId(AddOnInformation ObjAddOnInfo)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByInstituteIdAcaId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjAddOnInfo.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjAddOnInfo.AcademicYearId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AddOnInformation> ObjLstAddOnInfo = new List<AddOnInformation>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddOnInformation objAddOnInfo = new AddOnInformation();
                        objAddOnInfo.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objAddOnInfo.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjLstAddOnInfo.Add(objAddOnInfo);

                    }
                    return Return.returnHttp("200", ObjLstAddOnInfo, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region AddOnInformationReport
        [HttpPost]
        public HttpResponseMessage AddOnInformationReport(AddOnInformation ObjAddOnInfo)
        {
            try
            {
               
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);



                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                
                List<AddOnInformation> ObjLstAddOnInfo = new List<AddOnInformation>();
                SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjAddOnInfo.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjAddOnInfo.IncProgInstId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddOnInformation objAI = new AddOnInformation();

                        objAI.ProgrammeName = (dt.Rows[i]["ProgrammeName"].ToString());
                        objAI.BranchName = (dt.Rows[i]["BranchName"].ToString());
                        objAI.InstancePartTermName = (dt.Rows[i]["InstancePartTermName"].ToString());
      
                        List<AddOn> ObjLstAddOn = new List<AddOn>();
                        SqlCommand Cmd = new SqlCommand("AddOnCriteriaTiltleGetByProgrameInstancePartTermId", con);
                        Cmd.CommandType = CommandType.StoredProcedure;

                        Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjAddOnInfo.IncProgInstId);
                        SqlDataAdapter Da1 = new SqlDataAdapter();
                        DataTable Dt1 = new DataTable();
                        Da1.SelectCommand = Cmd;
                        Da1.Fill(Dt1);

                        if (Dt1.Rows.Count > 0)
                        {

                            for (int k = 0; k < Dt1.Rows.Count; k++)
                            {
                                AddOn objAOI = new AddOn();
                                objAOI.TitleName = Convert.ToString(Dt1.Rows[k]["TitleName"]);
                                ObjLstAddOn.Add(objAOI);
                            }

                        }
                        //else { }
                        Dt1.Clear();
                        Da1.Dispose();
                        objAI.DocList = ObjLstAddOn;
                        ObjLstAddOnInfo.Add(objAI);
                    }
                }
                return Return.returnHttp("200", ObjLstAddOnInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region AdmStudentAddOnReport
        [HttpPost]
         public HttpResponseMessage AdmStudentAddOnReport(AdmStudentAddOnInfo ObjAddOnStudent)
      {
          AdmStudentAddOnInfo modelobj = new AdmStudentAddOnInfo();
          try
          {
              String token = Request.Headers.GetValues("token").FirstOrDefault();
              //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
              TokenOperation ac = new TokenOperation();
              string res = ac.ValidateToken(token);
              //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

              if (res == "0")
              {
                  return Return.returnHttp("0", null, null);
              }

                Int32 Count = 0;
              modelobj.IncProgInstId = ObjAddOnStudent.IncProgInstId;
              SqlCommand cmd = new SqlCommand("AdmStudentAddOnInfoGetByProgrameInstancePartTermId", con);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.IncProgInstId);
              sda.SelectCommand = cmd;
              sda.Fill(dt);
              List<AdmStudentAddOnInfo> Stulist = new List<AdmStudentAddOnInfo>();
                if (dt.Rows.Count > 0) { 
              foreach (DataRow DR in dt.Rows)
                 {
                    AdmStudentAddOnInfo StuObj = new AdmStudentAddOnInfo();
                    StuObj.ApplicationFormNo = Convert.ToInt64(DR["ApplicationFormNo"].ToString());
                    StuObj.MobileNo = DR["MobileNo"].ToString();
                    StuObj.NameAsPerMarkSheet = DR["NameAsPerMarkSheet"].ToString();
                    StuObj.PRN = Convert.ToInt64(DR["PRN"]);
                    StuObj.EmailId = DR["EmailId"].ToString();
                    StuObj.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    Count = Count + 1;
                    StuObj.IndexId = Count;



                    Stulist.Add(StuObj);
             
                    }
              return Return.returnHttp("200", Stulist, null);
              }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
               
          }
          catch (Exception e)
          {
              return Return.returnHttp("201", e.Message.ToString(), null);
          }

      }
        #endregion

       
        #region AdmStudentTitleAddOnvalue
       [HttpPost]
       public HttpResponseMessage AdmStudentTitleAddOnvalueReport(AdmStudentAddOnTitle ObjAddOnStudent)
       {
           AdmStudentAddOnTitle modelobj = new AdmStudentAddOnTitle();
           try
           {
               String token = Request.Headers.GetValues("token").FirstOrDefault();
               //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
               TokenOperation ac = new TokenOperation();
               string res = ac.ValidateToken(token);

               if (res == "0")
               {
                   return Return.returnHttp("0", null, null);
               }

                Int32 Count = 0;
               modelobj.ApplicationFormNo = ObjAddOnStudent.ApplicationFormNo;
               SqlCommand cmd = new SqlCommand("AddOnInfoTitleGetByApplicationId", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationFormNo);
               sda.SelectCommand = cmd;
               sda.Fill(dt);
               List<AdmStudentAddOnTitle> Stulist = new List<AdmStudentAddOnTitle>();
                if (dt.Rows.Count > 0)
                {               
                   for (int i=0;i<dt.Rows.Count;i++)
                    {
                     AdmStudentAddOnTitle StuObj = new AdmStudentAddOnTitle();

                     StuObj.AddOnValue = Convert.ToString(dt.Rows[i]["AddOnValue"]);
                     StuObj.TitleName = Convert.ToString(dt.Rows[i]["TitleName"]);
                     Count = Count + 1;
                     StuObj.IndexId = Count;
                     Stulist.Add(StuObj);



                    }
                }

                SqlCommand cmd1 = new SqlCommand("AddOnNotAnsweredQuestionsGetByApplicationId", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationFormNo);
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                sda1.SelectCommand = cmd1;
                sda1.Fill(dt1);
                List<AdmStudentAddOnNotAnswered> StuNotAnslist = new List<AdmStudentAddOnNotAnswered>();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        AdmStudentAddOnNotAnswered StuNotAnsObj = new AdmStudentAddOnNotAnswered();
                        StuNotAnsObj.NotAnsweredQuestion = Convert.ToString(dt1.Rows[i]["NotAnsweredQuestion"]);
                        Count = Count + 1;
                        StuNotAnsObj.IndexId = Count;
                        StuNotAnslist.Add(StuNotAnsObj);

                    }
                }
               
                QuestionList questionList = new QuestionList();
                questionList.ansquestList = Stulist;
                questionList.NoansquestList = StuNotAnslist;
                if (questionList.ansquestList.Any())
                {
                    questionList.AnsweredListflag = true;
                }
                if (questionList.NoansquestList.Any())
                {
                    questionList.NotAnsweredListflag = true;
                }

                return Return.returnHttp("200", questionList, null);
           }
           catch (Exception e)
           {
               return Return.returnHttp("201", e.Message.ToString(), null);
           }

       }
       #endregion

        
        #region AddOn Info Report Excel
        [HttpPost]
        public HttpResponseMessage AddonInfoReportExcel(AddOnInformation ObjAddOnInfo)
        {
            AdmStudentAddOnTitle modelobj = new AdmStudentAddOnTitle();
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int32 Count = 0;
                ObjAddOnInfo.IncProgInstId = ObjAddOnInfo.IncProgInstId;
                SqlCommand cmd = new SqlCommand("AddOnInfoTitleReportExcel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjAddOnInfo.IncProgInstId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmStudentAddOnInfo> Stulist = new List<AdmStudentAddOnInfo>();
                if (dt.Rows.Count > 0) { 
                foreach (DataRow DR in dt.Rows)
                {
                    AdmStudentAddOnInfo StuObjExcel = new AdmStudentAddOnInfo();
                    StuObjExcel.ApplicationFormNumber = DR["ApplicationFormNo"].ToString();
                    StuObjExcel.PRN = Convert.ToInt64(DR["PRN"].ToString());
                    StuObjExcel.MobileNo = DR["MobileNo"].ToString();
                    StuObjExcel.EmailId = DR["EmailId"].ToString();
                    StuObjExcel.NameAsPerMarkSheet = DR["NameAsPerMarkSheet"].ToString();
                    StuObjExcel.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    StuObjExcel.AddOnValue = DR["AddOnValue"].ToString();
                    StuObjExcel.TitleName = DR["TitleName"].ToString();
                    Count = Count + 1;
                    StuObjExcel.IndexId = Count;
                    Stulist.Add(StuObjExcel);
                   
                }
                return Return.returnHttp("200", Stulist, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

    }
}
