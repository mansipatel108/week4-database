using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//require for EF db
using COMP2007_Lesson4B.Models;
using System.Web.ModelBinding;
namespace COMP2007_Lesson4B
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for first time, populae the grid fro EF Db
            if(IsPostBack)
            {
                //get data
                this.GetStudents();
            }

        }
        protected void GetStudents()
        {
            //connect to EF DB
            using (DefaultConnection db = new DefaultConnection())
            {
                //query the students table using EF and LINQ
                var Students = (from allStudents in db.Students
                                select allStudents);
                //bind thhe result to the gridview
                StudentsGridView.DataSource = Students.ToList();
                StudentsGridView.DataBind();
            }
        }
    }
}