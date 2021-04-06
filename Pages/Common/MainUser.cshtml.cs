using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using webapp.Models;
using webapp.Models.Reco;

namespace webapp.Pages.Common
{
    public class MainUserModel : PageModel
    {
        private readonly webapp.Data.ShopContext _context;
        public IList<Product> Product { get; set; }

        public MainUserModel(webapp.Data.ShopContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Product = new List<Product>();
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            var opinieUser = _context.Account_Review.Where(b => b.AccountId == accountt.Id).ToList().Count();
            if (opinieUser >= 2)
            {
                var produktyy = _context.Product.ToList();
                var pommm = 0;
                foreach (Product p in produktyy)
                {

                    if (_context.Account_Review.Any(a => a.ProductId == p.Id))
                    {
                        pommm++;
                    }
                }
                if (pommm >= 5)
                { recommendation(); }
                else
                {
                    Account account = new Account();
                    var produkty = _context.Product.ToList();
                    List<KeyValuePair<float, Product>> list = new List<KeyValuePair<float, Product>>();
                    List<KeyValuePair<float, Product>> list3 = new List<KeyValuePair<float, Product>>();
                    foreach (Product p in produkty)
                    {
                        float pom = 0;
                        int i = 0;
                        var lista = _context.ReviewProduct.Where(a => a.ProductID == p.Id).ToList();
                        foreach (ReviewProduct r in lista)
                        {
                            pom += r.Number;
                            i++;
                        }
                        pom /= i;
                        list.Add(new KeyValuePair<float, Product>(pom, p));
                    }
                    foreach (KeyValuePair<float, Product> tem in list)
                    {
                        if (tem.Key.ToString() != "NaN") list3.Add(tem);
                    }
                    list3 = list3.OrderByDescending(o => o.Key).ToList();
                    int pom1 = list3.Count();
                    Product = new List<Product>();
                    for (int i = 0; i < Math.Min(10, pom1); i++)
                    {
                        Product.Add(list3.ElementAt(i).Value);
                    }
                }
            }
            else
            {
                Account account = new Account();
                var produkty = _context.Product.ToList();
                List<KeyValuePair<float, Product>> list = new List<KeyValuePair<float, Product>>();
                List<KeyValuePair<float, Product>> list3 = new List<KeyValuePair<float, Product>>();
                foreach (Product p in produkty)
                {
                    float pom = 0;
                    int i = 0;
                    var lista = _context.ReviewProduct.Where(a => a.ProductID == p.Id).ToList();
                    foreach (ReviewProduct r in lista)
                    {
                        pom += r.Number;
                        i++;
                    }
                    pom /= i;
                    list.Add(new KeyValuePair<float, Product>(pom, p));
                }
                foreach (KeyValuePair<float, Product> tem in list)
                {
                    if (tem.Key.ToString() != "NaN") list3.Add(tem);
                }
                list3 = list3.OrderByDescending(o => o.Key).ToList();
                int pom1 = list3.Count();
                Product = new List<Product>();
                for (int i = 0; i < Math.Min(10, pom1); i++)
                {
                    Product.Add(list3.ElementAt(i).Value);
                }
            }
            if (Product.Count < 5)
            {
                var produktyy = _context.Product.ToList();
                Product.Clear();
                for (int i = 0; i < Math.Min(10, produktyy.Count); i++)
                {
                    Product.Add(produktyy[i]);
                }

            }
            var xx = 7;
        }
        /*public void recomentadion()
        {
var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            MLContext mlContext = new MLContext();
            IDataView trainingDataView = LoadData(mlContext);
            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);
            List<Product> proList = _context.Product.ToList();
            float wyn = 0;
            Product produc;
            float tempf;
            foreach(Product p in proList)
            {
                tempf=UseModelForSinglePrediction(mlContext, model, accountt.Id, p.Id);
                if (tempf > wyn) { wyn = tempf; produc = p; }
                var x = 7;
            }
        }*/
        public void recommendation()
        {
            var username = HttpContext.Session.GetString("username");
            var accountt = _context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            MLContext mlContext = new MLContext();
            IDataView trainingDataView = LoadData(mlContext);
            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);
            List<Product> proList = _context.Product.ToList();
            float wyn = 0;
            var produkty = _context.Product.ToList();
            int pom = produkty.Count();
            List<KeyValuePair<float, Product>> list = new List<KeyValuePair<float, Product>>();
          

            foreach (Product p in produkty)
            {

                if (_context.Account_Review.Any(a => a.ProductId == p.Id))
                {
                    float te = UseModelForSinglePrediction(mlContext, model, accountt.Id, p.Id);
                    list.Add(new KeyValuePair<float, Product>(te, p));
                    model = BuildAndTrainModel(mlContext, trainingDataView);
                    mlContext = new MLContext();
                }
            }
            list = list.OrderByDescending(o => o.Key).ToList();
            List<KeyValuePair<float, Product>> list2 = new List<KeyValuePair<float, Product>>();
            foreach (KeyValuePair<float, Product> a in list)
            {
                if (a.Key.ToString() != "NaN") list2.Add(a);
            }
            for (int i = 0; i < Math.Min(10, list2.Count()); i++)
            {
                Product.Add(list2.ElementAt(i).Value);
            }
            var x = 12;
            /*  Product produc = null;
              int pom = proList.Count();
              for (int i = 0; i < Math.Min(30, pom); i++)
              {
                  foreach (Product p in proList)
                  {
                      float tempf = UseModelForSinglePrediction(mlContext, model, accountt.Id, p.Id);
                      if (tempf > wyn)
                      {
                          wyn = tempf; produc = p;
                      }
                      var yy = 7;
                  }

                  if (produc != null)
                  {
                      Product.Add(produc);
                      proList.Remove(produc);

                  }
                  produc = null;
                  wyn = 0;
                  model = BuildAndTrainModel(mlContext, trainingDataView);
                  mlContext = new MLContext();
              }*/

        }

        public static IDataView LoadData(MLContext mlContext)
        {
            /* var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-train.csv");
             var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-test.csv");

             IDataView trainingDataView = mlContext.Data.LoadFromTextFile<MovieRating>(trainingDataPath, hasHeader: true, separatorChar: ',');
             IDataView testDataView = mlContext.Data.LoadFromTextFile<MovieRating>(testDataPath, hasHeader: true, separatorChar: ',');
            */

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string sqlCommand = "SELECT a.AccountId, a.ProductId, a.number FROM Account_Review a";


            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<MovieRating>();
            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance, connectionString, sqlCommand);


            IDataView trainingDataView = loader.Load(dbSource);
            return trainingDataView;
        }
        public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: "movieId"));
            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "movieIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));
            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }
        public static float UseModelForSinglePrediction(MLContext mlContext, ITransformer model, int user, int movie)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
            var testInput = new MovieRating { userId = Convert.ToSingle(user), movieId = Convert.ToSingle(movie) };

            var movieRatingPrediction = predictionEngine.Predict(testInput);
            /*if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
            {
                Console.WriteLine("Movie " + testInput.movieId + " is recommended for user " + testInput.userId);
            }
            else
            {
                Console.WriteLine("Movie " + testInput.movieId + " is not recommended for user " + testInput.userId);
            }
            Console.WriteLine(movieRatingPrediction.Label + "   " + movieRatingPrediction.Score);*/
            return (movieRatingPrediction.Score);
        }
    }
}
