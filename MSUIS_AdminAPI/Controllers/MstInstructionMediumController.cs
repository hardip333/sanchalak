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
    public class MstInstructionMediumController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);

        [HttpPost]
        public HttpResponseMessage MstInstructionMediumListGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstInstructionMediumGet", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //cmdda.SelectCommand.Parameters.AddWithValue("@Flag", "MstInstructionMediumListGet");

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstInstructionMedium> ObjLstMstInstrMedium = new List<MstInstructionMedium>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstructionMedium ObjMstMstInstrMedium = new MstInstructionMedium();

                        ObjMstMstInstrMedium.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ObjMstMstInstrMedium.InstructionMediumName = Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        ObjMstMstInstrMedium.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstMstInstrMedium.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstMstInstrMedium.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstInstrMedium.Add(ObjMstMstInstrMedium);
                    }

                }

                return Return.returnHttp("200", ObjLstMstInstrMedium, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
    }
}
