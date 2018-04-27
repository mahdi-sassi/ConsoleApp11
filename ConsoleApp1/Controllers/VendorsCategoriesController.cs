using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using ConsoleApp1.Models;
using Newtonsoft.Json;
using Root.Services.ExpressionJson;

namespace ConsoleApp1.Controllers
{
    public class VendorsCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VendorsCategories
        public IQueryable<VendorsCategories> GetVendorsCategories()
        {
            return db.VendorsCategories;
        }

        [HttpPost]
        [Route("api/PostGetVendors")]
        [ResponseType(typeof(Vendors))]
        public IEnumerable<Vendors> PostGetVendors(InputExpressions inputExpressions)
        {
            Assembly assem = Assembly.GetAssembly(typeof(Vendors));
            string typeName = assem.FullName;
            //var x = JsonConvert.DeserializeObject<InputExpressions>(inputExpressions);
            var x = inputExpressions.Expression;
            var expressionJson = new ExpressionJson<Vendors>();
            var ListIncludes = new List<Expression<Func<Vendors, object>>>();
            var WhereExpression = expressionJson.Deserialize(x.ToString());
            if (inputExpressions.Includes != null)
            {
                foreach (var item in inputExpressions.Includes)
                {
                    var include = JsonConvert.DeserializeObject(item);
                    var includeExpression = expressionJson.Deserialize(include.ToString());

                    var memberName = ((MemberExpression)includeExpression.Body).Member.Name;
                    var param = Expression.Parameter(typeof(Vendors));
                    var field = Expression.Property(param, memberName);
                    var expr = Expression.Lambda<Func<Vendors, object>>(field, param);
                    ListIncludes.Add(expr);
                }
            }
            var ListOrderBy = new List<Expression<Func<Vendors, Object>>>();
            if (inputExpressions.OrderBy != null)
            {
                foreach (var item in inputExpressions.OrderBy)
                {
                    var orderBy = JsonConvert.DeserializeObject(item);
                    var orderByExpression = expressionJson.Deserialize(orderBy.ToString());

                    var memberName = ((MemberExpression)orderByExpression.Body).Member.Name;
                    var param = Expression.Parameter(typeof(Vendors));
                    var field = Expression.Property(param, memberName);
                    var expr = Expression.Lambda<Func<Vendors, Object>>(field, param);
                    ListOrderBy.Add(expr);
                }
            }
            var result = db.Vendors.Where((Expression<Func<Vendors, bool>>)WhereExpression).ToList();
            return result;



            #region
            //HttpRequestMessage re = Request as HttpRequestMessage;
            //var headers = re.Headers;

            //if (headers.Contains("Json"))
            //{
            //    string token = headers.GetValues("Custom").First();
            //}
            //if (httpClient.Headers["Json"].Count() > 0)
            //{

            //    IEnumerable<string> headerValues = Request.Headers.GetValues("Json");
            //    var json = headerValues.FirstOrDefault();
            //    //var random = new Random();
            //    //int u;
            //    //var context = new Context1
            //    //{
            //    //    A = random.Next(),
            //    //    B = random.Next(),
            //    //    C = (u = random.Next(0, 2)) == 0 ? null : (int?)u,
            //    //    Array = new[] { random.Next() },
            //    //    Func = () => u
            //    //};

            //var settings = new JsonSerializerSettings();
            //settings.Converters.Add(new ExpressionJsonConverter(
            //    Assembly.GetAssembly(typeof(ExpressionJsonSerializerTest))
            //));
            //var json = JsonConvert.SerializeObject(source, settings);
            //var IncludeExpression = expressionJson.Deserialize(include);
            //var OrderByExpression = expressionJson.Deserialize(orderBy);
            //List<Expression<Func<Vendors, object>>> ListIncludeExpression = new List<Expression<Func<Vendors, object>>>();
            //ListIncludeExpression.Add(IncludeExpression as Expression<Func<Vendors, object>>);
            //var target = JsonConvert.DeserializeObject<LambdaExpression>(json, settings);
            //return db.Categorys.Include(c => c.Listproducts); 
            //}
            #endregion

        }


        // GET: api/VendorsCategories/5
        [ResponseType(typeof(VendorsCategories))]
        public IHttpActionResult GetVendorsCategories(Guid id)
        {
            VendorsCategories vendorsCategories = db.VendorsCategories.Find(id);
            if (vendorsCategories == null)
            {
                return NotFound();
            }

            return Ok(vendorsCategories);
        }

        // PUT: api/VendorsCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorsCategories(Guid id, VendorsCategories vendorsCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorsCategories.ID)
            {
                return BadRequest();
            }

            db.Entry(vendorsCategories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorsCategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VendorsCategories
        [ResponseType(typeof(VendorsCategories))]
        public IHttpActionResult PostVendorsCategories(VendorsCategories vendorsCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendorsCategories.Add(vendorsCategories);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VendorsCategoriesExists(vendorsCategories.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vendorsCategories.ID }, vendorsCategories);
        }

        // DELETE: api/VendorsCategories/5
        [ResponseType(typeof(VendorsCategories))]
        public IHttpActionResult DeleteVendorsCategories(Guid id)
        {
            VendorsCategories vendorsCategories = db.VendorsCategories.Find(id);
            if (vendorsCategories == null)
            {
                return NotFound();
            }

            db.VendorsCategories.Remove(vendorsCategories);
            db.SaveChanges();

            return Ok(vendorsCategories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorsCategoriesExists(Guid id)
        {
            return db.VendorsCategories.Count(e => e.ID == id) > 0;
        }
    }
}