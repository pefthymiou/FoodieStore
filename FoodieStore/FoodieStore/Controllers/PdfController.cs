using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoodieStore.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;

namespace FoodieStore.Controllers
{
    public class PdfController : Controller
    {
        private App_Context db = new App_Context();


        //public ActionResult ExportToPdf_Click(object sender, EventArgs e, int id)
        //{
        //    //var UserId = (string)Session["UserName"];
        //    string File = "PDFUserDetails";
        //    List<OrdersJoin> result = (from ord in db.Orders
        //                               join ordDet in db.OrderDetails on ord.OrderId equals ordDet.OrderId
        //                               join prod in db.Products on ordDet.ProductId equals prod.ProductId
        //                               where ord.Username == User.Identity.Name
        //                               select new OrdersJoin
        //                               {
        //                                   State = ord.OrderState,
        //                                   OrderDate = ord.OrderDate,
        //                                   Quantity = ordDet.Quantity,
        //                                   ProductPrice = ordDet.ProductPrice,
        //                                   ProductName = prod.ProductName
        //                               }).ToList();
        //    ExportListToPDF(result, File);

        //    return View("CustOrders", "Home");
        //}

        public ActionResult ExportToExcel()
        {
            var data = (from ord in db.Orders
                        join ordDet in db.OrderDetails on ord.OrderId equals ordDet.OrderId
                        join prod in db.Products on ordDet.ProductId equals prod.ProductId
                        where ord.Username == User.Identity.Name
                        select new OrdersJoin
                        {
                            State = ord.OrderState,
                            OrderDate = ord.OrderDate,
                            Quantity = ordDet.Quantity,
                            ProductPrice = ordDet.ProductPrice,
                            ProductName = prod.ProductName
                        }).ToList();

            GridView view = new GridView();
            view.DataSource = data;
            view.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=myorders.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Charset = "";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    view.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            return View();
        }

        //private void ExportListToPDF(List<OrdersJoin> result, string File)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=UserDetails.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw = new StringWriter();//Edw exei thn HTML tou table
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    GridView gridView = new GridView();
        //    gridView.DataSource = result;
        //    gridView.DataBind();
        //    gridView.RenderControl(hw);
        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    //FontFactory.Register(Path.Combine(_webHelper.))
        //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    XMLWorkerHelper worker = XMLWorkerHelper.GetInstance();
        //    //worker.ParseXHtml(writer,pdfDoc,Cha,)
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();
        //    gridView.AutoGenerateColumns = true;
        //}
    }
}