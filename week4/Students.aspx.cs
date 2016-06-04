using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements are required to connect EF database
using week4.Models;
using System.Web.ModelBinding;

namespace week4
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for thhe first time populate the student grid
            if (!IsPostBack)
            {
                //get the students data
                this.GetStudents();
            }
        }
        /**
         * <summery>
         * This method gets the students data from the DB
         * </summery>
         * 
         * @method GetStudents
         * @return {void}
         * */
        protected void GetStudents()
        {
            //connect to EF
            using(DefaultConnection db = new DefaultConnection())
            {
                //query students table using EF and LINQ
                var Students = (from allStudents in db.Students
                                select allStudents);

                //bind the result in GridView
                StudentsGridView.DataSource = Students.ToList();
                StudentsGridView.DataBind();
            }

        }
    }
}