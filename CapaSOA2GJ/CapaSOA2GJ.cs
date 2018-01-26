using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CapaSOA2GJ
{
    public class CapaSOA2GJ
    {
        public string CodeAnswer;
        public String DescriptionAnswer;

        private String InXML;
        private XmlDocument OutXML;

        #region Set
        public void SetInXML(String InXML)
        {
            this.InXML = InXML;
        }

        public void SetOutXML(String InXML)
        {
            this.OutXML.LoadXml(InXML);
        }
        #endregion

        #region PerformActivity
        public string PerformActivity()
        {
            try
            {
                SOABAWF.WorkflowEngineSOA objSOA = new SOABAWF.WorkflowEngineSOA();
                this.SetOutXML(objSOA.performActivityAsString(this.InXML));
                return this.CapturaRespuestaBA();
            }
            catch (Exception e1)
            {
                return "Error CapaSOABizAgi: " + e1.Message;
            }
        }
        public string PerformActivity(string In_Domain, string In_UserName, Int32 In_IdCase, Int32 In_IdTask)
        {
            string XML = "";
            XML += "<BizAgiWSParam>";
            XML += "<domain>" + In_Domain + "</domain>";
            XML += "<userName>" + In_UserName + "</userName>";
            XML += "<ActivityData>";
            XML += "<idCase>" + In_IdCase + "</idCase>";
            XML += "<taskId>" + In_IdTask + "</taskId>";
            XML += "</ActivityData>";
            XML += "</BizAgiWSParam>";

            this.SetInXML(XML);

            return this.PerformActivity();
        }
        #endregion

        #region Answer
        public string CapturaRespuestaBA()
        {
            string sRespuesta = "";

            //xdoc.LoadXml(this.SetOutXML);
            XmlNodeList NodoRC01 = this.OutXML.SelectNodes("/process/processError");

            if (NodoRC01.Count > 0)
            {
                foreach (XmlNode XN in NodoRC01)
                {
                    if (XN["errorCode"] != null)
                    {
                        sRespuesta += XN["errorCode"].InnerText;
                    }
                    else
                    {
                        sRespuesta += "OK";
                    }
                    if (XN["errorMessage"] != null)
                    {
                        sRespuesta += XN["errorMessage"].InnerText;
                    }
                    else
                    {
                        sRespuesta += "EJECUCION CORRECTA...";
                    }
                }
            }
            else
            {
                XmlNodeList NodoRC02 = this.OutXML.SelectNodes("/processes/process/processError");

                if (NodoRC02.Count > 0)
                {
                    foreach (XmlNode XN in NodoRC02)
                    {
                        if (XN["errorCode"] != null)
                        {
                            sRespuesta += XN["errorCode"].InnerText;
                        }
                        else
                        {
                            sRespuesta += "OK";
                        }
                        if (XN["errorMessage"] != null)
                        {
                            sRespuesta += XN["errorMessage"].InnerText;
                        }
                        else
                        {
                            sRespuesta += "EJECUCION CORRECTA...";
                        }
                    }
                }
                else
                {
                    sRespuesta += "EJECUCION CORRECTA...";
                }
            }

            sRespuesta = sRespuesta.Replace("\n", "");
            if (sRespuesta == "")
                sRespuesta = "EJECUCION CORRECTA...";

            return sRespuesta;
        }
        #endregion
    }
}
