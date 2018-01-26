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
        public void PerformActivity()
        {
            try
            {
                SOABAWF.WorkflowEngineSOA objSOA = new SOABAWF.WorkflowEngineSOA();
                this.SetOutXML(objSOA.performActivityAsString(this.InXML));
            }
            catch (Exception e1)
            {
                this.CodeAnswer = "999";
                this.DescriptionAnswer = "Error CAPASOBA: " + e1.Message;
            }
        }
        public void PerformActivity(string In_Domain, string In_UserName, Int32 In_IdCase, Int32 In_IdTask)
        {
            try
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
                this.PerformActivity();
            }
            catch (Exception e1)
            {
                this.CodeAnswer = "999";
                this.DescriptionAnswer = "Error CAPASOBA: " + e1.Message;
            }
        }
        #endregion

        #region Answer
        public void CapturaRespuestaBA()
        {
            XmlNodeList NodoRC01 = this.OutXML.SelectNodes("/process/processError");
            if (NodoRC01.Count > 0)
            {
                foreach (XmlNode XN in NodoRC01)
                {
                    if (XN["errorCode"] != null)
                        this.CodeAnswer = XN["errorCode"].InnerText;
                    else
                        this.CodeAnswer = "0";
                    if (XN["errorMessage"] != null)
                        this.DescriptionAnswer = XN["errorMessage"].InnerText;
                    else
                        this.DescriptionAnswer = "---EJECUCION CORRECTA---";
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
                            this.CodeAnswer = XN["errorCode"].InnerText;
                        else
                            this.CodeAnswer = "OK";
                        if (XN["errorMessage"] != null)
                            this.DescriptionAnswer += XN["errorMessage"].InnerText;
                        else
                            sRespuesta = "---EJECUCION CORRECTA---";
                    }
                }
                else
                {
                    this.CodeAnswer = "-1";
                    sRespuesta = "---Formato Respuesta No Reconocido---";
                }
            }
        }
        #endregion
    }
}
