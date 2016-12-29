using Microsoft.Reporting.WebForms;
using RDLCExample.Report;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.Mvc;

namespace RDLCExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Rdlc()
        {
            // Go report viewer page
            Session["ReportWrapper"] = GetProduct();
            return Redirect("~/Report/ReportViewer.aspx");
        }

        private ReportWrapper GetProduct()
        {
            ReportWrapper rw = new ReportWrapper();

            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["SQL"].ConnectionString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM Product;", conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            // Set report info
            rw.ReportPath = Server.MapPath("~/Report/Rdlc/Product.rdlc");
            rw.ReportDataSources.Add(new ReportDataSource("DataSet", dt));
            rw.ReportParameters.Add(new ReportParameter("RptParam_TiTle", "採購單 MAP_590004"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Date", "2999/99/99"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Company", "公司\r\n地址\r\n電話"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Text1", "黃曉明"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Text2", "王大明"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Text3", "王忠明"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Text4", "小小明"));
            rw.ReportParameters.Add(new ReportParameter("RptParam_Text5", "AAA-123"));

            return rw;
        }
    }
}