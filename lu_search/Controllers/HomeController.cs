using lu_search.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace lu_search.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchTerm, string searchField, bool? searchDefault, int? limit)
        {
            // create default Lucene search index directory
            if (!Directory.Exists(LuceneSearch._luceneDir))
            { 
                Directory.CreateDirectory(LuceneSearch._luceneDir); 
            }

            // perform Lucene search
            List<Student> _searchResults;
            if (searchDefault == true)
                _searchResults = (string.IsNullOrEmpty(searchField)
                                   ? LuceneSearch.SearchDefault(searchTerm)
                                   : LuceneSearch.SearchDefault(searchTerm, searchField)).ToList();
            else
                _searchResults = (string.IsNullOrEmpty(searchField)
                                   ? LuceneSearch.Search(searchTerm)
                                   : LuceneSearch.Search(searchTerm, searchField)).ToList();
            if (string.IsNullOrEmpty(searchTerm) && !_searchResults.Any())
                _searchResults = LuceneSearch.GetAllIndexRecords().ToList();


            // setup and return view model
            var search_field_list = new
                List<SelectedList> {
                                         new SelectedList {Text = "(All Fields)", Value = ""},
                                         new SelectedList {Text = "Id", Value = "Id"},
                                         new SelectedList {Text = "Name", Value = "Name"},
                                         new SelectedList {Text = "Address", Value = "Address"}
                                     };

            // limit display number of database records
            var limitDb = limit == null ? 3 : Convert.ToInt32(limit);
            List<Student> allStudent;
            if (limitDb > 0)
            {
                allStudent = StudentData.GetStudents().ToList().Take(limitDb).ToList();
                ViewBag.Limit = StudentData.GetStudents().Count - limitDb;
            }
            else allStudent = StudentData.GetStudents();

            return View(new IndexViewModel
            {
                AllStudentData = allStudent,
                SearchIndexData = _searchResults,
                student = new Student(),                  //{ Id = 9, Name = "KKR", Address = "City in Texas" } for default value in input box
                SearchFieldList = search_field_list,
            });
        }

        public ActionResult Search(string searchTerm, string searchField, string searchDefault)
        {
            return RedirectToAction("Index", new { searchTerm, searchField, searchDefault });
        }



        [HttpPost]
        public ActionResult AddToIndex(Student student)
        {
            LuceneSearch.AddUpdateLuceneIndex(student);
            TempData["Result"] = "Record was added to search index successfully!";
            return RedirectToAction("Index");
        }

        public ActionResult ClearIndex()
        {
            if (LuceneSearch.ClearLuceneIndex())
                TempData["Result"] = "Search index was cleared successfully!";
            else
                TempData["ResultFail"] = "Index is locked and cannot be cleared, try again later or clear manually!";
            return RedirectToAction("Index");
        }

        public ActionResult ClearIndexRecord(int id)
        {
            LuceneSearch.ClearLuceneIndexRecord(id);
            TempData["Result"] = "Search index record was deleted successfully!";
            return RedirectToAction("Index");
        }

        public ActionResult OptimizeIndex()
        {
            LuceneSearch.Optimize();
            TempData["Result"] = "Search index was optimized successfully!";
            return RedirectToAction("Index");
        }
    }
}