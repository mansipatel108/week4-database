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

        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected StudentID using the Grid's DataKey Collection
            int StudentID = Convert.ToInt32(StudentsGridView.DataKeys[selectedRow].Values["StudentID"]);

            // use EF to find the selected student from DB and remove it
            using (DefaultConnection db = new DefaultConnection())
            {
                Student deletedStudent = (from studentRecords in db.Students
                                          where studentRecords.StudentID == StudentID
                                          select studentRecords).FirstOrDefault();

                // perform the removal in the DB
                db.Students.Remove(deletedStudent);
                db.SaveChanges();

                // refresh the grid
                this.GetStudents();

            }
        }

        protected void StudentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            StudentsGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetStudents();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set new page size
            StudentsGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetStudents();
        }
    }
}