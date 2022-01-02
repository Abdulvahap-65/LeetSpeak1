using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeetSpeak1.Helpers;
using LeetSpeak1.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LeetSpeak1.Controllers
{
    public class L33tSp34kController : Controller
    {
        private LeetSpeakEntities db = new LeetSpeakEntities();

        // GET: L33tSp34k
        public ActionResult Index()
        {
            return View(db.L33tSp34k.OrderByDescending(m=>m.ID).ToList());
        }

        // GET: L33tSp34k/Create
        public ActionResult Create()
        {
            ViewBag.ApiResponse = new ResponseModel();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Translated,Text,Translation")] L33tSp34k l33tSp34k, string api = "leetspeak")
        {
            ResponseModel model = new ResponseModel();
           
                if (ModelState.IsValid)
                {
                    L33tSp34k newData = new L33tSp34k();
                    newData.Text = l33tSp34k.Text;
                   
                    
                        var client = new RestClient("https://api.funtranslations.com/");
                        var request = new RestRequest("translate/"+api+".json?text=" + newData.Text, DataFormat.Json);
                        var response = client.Get<FunTranslationResponse>(request);
                    if(!String.IsNullOrEmpty(response.Content))
                    {
                       
                        model = JsonConvert.DeserializeObject<ResponseModel>(response.Content);
                    }
                if (response.Data.contents!=null)
                {
                    newData.Translated = response.Data.contents.translated;
                    newData.Translation = response.Data.contents.translation;
                    db.L33tSp34k.Add(newData);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                              
                }  
            ViewBag.ApiResponse = model;
            return View(l33tSp34k);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class FunTranslationResponse
    {
        public TranslationContents contents { get; set; }
    }
    public class TranslationContents
    {
        public string translated { get; set; }
        public string text { get; set; }
        public string translation { get; set; }

    }
}
