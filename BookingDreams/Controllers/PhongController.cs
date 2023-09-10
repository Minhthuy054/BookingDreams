using BookingDreams.Data;
using BookingDreams.Models;
using BookingDreams.Helpers;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Crypto;
using Microsoft.ML;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using static Microsoft.ML.DataOperationsCatalog;
using System.Composition.Convention;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly PhongRes _repo;
        private readonly BookingDreamsContext _context;
        private readonly RecommendOrderRes _recommend;
        private readonly FuncSupport _funcSupport;
        private readonly DatPhongRes _repoDatPhong;
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public PhongController(PhongRes repo,FuncSupport funcSupport, DatPhongRes repoDatPhong, MLContext mLContext, BookingDreamsContext context, RecommendOrderRes recommend) {
            _repo = repo;
            _funcSupport = funcSupport;
            _repoDatPhong = repoDatPhong;
            _mlContext = mLContext;
            _context = context;
            _recommend = recommend;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongModel>> GetByID(int id)
        {
            var phong = await _repo.GetByID(id);
            return phong;
        }
        //[Authorize(Roles = "NhanVien,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create (/*[FromForm]*/ PhongModel phong)
        {
            
            if(phong == null)
            {
                return BadRequest();
            }

            //string lstLink = "";
            //var newPhong = new PhongModel
            //{
            //    IdKhachSan = phong.IdKhachSan,
            //    TenPhong = phong.TenPhong,
            //    SoPhong = phong.SoPhong,
            //    GiaPhong = phong.GiaPhong,
            //    MoTa = phong.MoTa,
            //    Loai = phong.Loai,
            //    SoLuongNguoiLon = phong.SoLuongNguoiLon,
            //    SoLuongTreEm = phong.SoLuongTreEm
            //};

            //if (phong.HinhAnhFile.Count() > 0)
            //{
                
            //    foreach (var file in phong.HinhAnhFile)
            //    {
            //        if (!_funcSupport.IsImageFile(file))
            //        {
            //            return BadRequest("Tệp đầu vào không phải là ảnh");
            //        }
            //        DateTime date = DateTime.Now;
            //        string publishPath = Path.Combine(@"images", "Phong_"+newPhong.TenPhong, date.ToString("yyyy-MM-dd"));
            //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", publishPath);//Đường dẫn để lưu file
            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }
            //        string fileExtension = new FileInfo(file.FileName).Extension; //Định dạng của file (png,jpg,...)
            //        string filePath = "Phong" + "_"   + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
            //        //Lưu file vào hệ thống
            //        using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
            //        {
            //            await file.CopyToAsync(fileStream);
            //        }
            //        lstLink += Path.Combine(path, filePath) + ";";
            //    }
            //}
            
            //newPhong.HinhAnh = lstLink;
            var result = await _repo.Add(phong);
            return Ok(result);
        }
        [Authorize(Roles = "NhanVien,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update( PhongModel phong, int id) { 
            if(phong.Id != id)
            {
                return BadRequest();
            }
            //string lstLink = "";
            //var newPhong = new PhongModel
            //{
            //    IdKhachSan = phong.IdKhachSan,
            //    TenPhong = phong.TenPhong,
            //    SoPhong = phong.SoPhong,
            //    GiaPhong = phong.GiaPhong,
            //    MoTa = phong.MoTa,
            //    Loai = phong.Loai,
            //    SoLuongNguoiLon = phong.SoLuongNguoiLon,
            //    SoLuongTreEm = phong.SoLuongTreEm
            //};
            //if (phong.HinhAnhFile.Count() > 0)
            //{
            //    foreach (var file in phong.HinhAnhFile)
            //    {
            //        DateTime date = DateTime.Now;
            //        string publishPath = Path.Combine(@"images", "Phong"+newPhong.TenPhong, date.ToString("yyyy-MM-dd"));
            //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", publishPath);//Đường dẫn để lưu file
            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }
            //        string fileExtension = new FileInfo(file.FileName).Extension; //Định dạng của file (png,jpg,...)
            //        string filePath = "Phong" + "_" + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
            //        //Lưu file vào hệ thống
            //        using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
            //        {
            //            await file.CopyToAsync(fileStream);
            //        }
            //        lstLink += Path.Combine(path, filePath) + ";";
            //    }
            //}
            //newPhong.HinhAnh = lstLink;
            var result =  await _repo.Update(phong, id);
            return Ok(result);
        }
       // [Authorize(Roles = "NhanVien,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(_repo.GetAll() == null)
            {
                return BadRequest();
            }
            var result = await _repo.Delete(id);
            return Ok(result);
        }

        [HttpGet("GetByKhachSan")]
        public async Task<IActionResult> GetByKhachSan(int id)
        {
            var lstPhong = await _repo.GetByKhachSan(id);
            return Ok(lstPhong);
        }
        [HttpGet("SearchRoom")]
        public async Task<IActionResult> Seacrch(string diaDiem, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var result = await _repo.Search(diaDiem, ngayBatDau, ngayKetThuc);
            return Ok(result);
        }
        //[HttpGet("GetPhongRecommad")]
        //public async Task<IActionResult> GetRecommend(string email)
        //{
        //    MLContext mlContext = new MLContext();
        //    var listDatPhong = await _repoDatPhong.GetByEmail(email);
        //    var data = listDatPhong.Select(datPhong => new { datPhong.Id, datPhong.IdPhong }).ToList();
        //    //Chia Tập dữ liệu

        //    //Chuẩn bị dữ liệu để huấn luyện
        //    IDataView alldata = mlContext.Data.LoadFromEnumerable(data);
        //    TrainTestData splitDataView = mlContext.Data.TrainTestSplit(alldata, testFraction: 0.2);
        //    //Định nghĩa pipeline
        //    var pipeline = mlContext.Transforms.Conversion.MapValueToKey("UserId", nameof(DatPhong.Id))
        //        .Append(mlContext.Transforms.Conversion.MapValueToKey("ItemId", nameof(DatPhong.IdPhong)))
        //        .Append(mlContext.Recommendation().Trainers.MatrixFactorization("UserId", "ItemId", "Label", 5));

        //    //var pipeline = mlContext.Transforms.Conversion.MapValueToKey("UserId", "UserId")
        //    //    .Append(mlContext.Transforms.Conversion.MapValueToKey("ItemId", "ItemId"))
        //    //    .Append(mlContext.Recommendation().Trainers.MatrixFactorization(
        //    //        labelColumnName: "Rating",
        //    //        matrixColumnIndexColumnName: "UserIdEncoded",
        //    //        matrixRowIndexColumnName: "ItemIdEncoded",
        //    //        numberOfIterations: 20,
        //    //        approximationRank: 100));


        //    var model = pipeline.Fit(alldata);
        //    var userId = 1;
        //    var predictionEngine = mlContext.Model.CreatePredictionEngine<DatPhong, ItemRating>(model);
        //    var recommendations = predictionEngine.Predict(new DatPhong { IdPhong = userId });
        //    var topRecommendations = recommendations.RecommendedIds;

        //}
        //



        //public void TrainModel()
        //{
        //    var trainingData = _context.DatPhongs!.ToList();
        //    var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

        //    var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(DatPhong.IdPhong))
        //        .Append(_mlContext.Transforms.Concatenate("Features", nameof(DatPhong.ThoiGianNhanPhong), nameof(DatPhong.ThoiGianTraPhong)))
        //        .Append(_mlContext.Recommendation().Trainers.MatrixFactorization("Features", "Id", "Label", 5));
        //    //.Append(_mlContext.Regression.Trainers.FastTree());

        //    _model = pipeline.Fit(dataView);
        //}
        //public float[] Predict(List<DatPhong> input)
        //{
        //    var predictionEngine = _mlContext.Model.CreatePredictionEngine<DatPhong, PhongPrediction>(_model);
        //    List<DatPhong> inputData = input;
        //    var prediction = predictionEngine.Predict(inputData[0]);

        //    return prediction.PredictedIdPhong;
        //}

        //[HttpGet("Recommendtion")]
        //public async Task<IActionResult> Recommedation(string email)
        //{
        //    var lstDatPhong = await _repoDatPhong.GetByEmail(email);
        //    var allRoom = await _repo.GetAll();
        //    var result = _recommend.GetRecommendedRooms(lstDatPhong, allRoom);
        //    return Ok(result);
        //}


        //ĐƯợc sử dụng
        //[HttpGet("Recommendtion")]
        //public async Task<IActionResult> Recommendation(string email)
        //{
        //    //Train model
        //    var trainingData = await _repoDatPhong.GetAll();
        //    var newtrainingData = trainingData.Select(datPhong => new { datPhong.Id, datPhong.IdPhong, datPhong.TongTien,datPhong.Label }).ToList();
        //    var dataView = _mlContext.Data.LoadFromEnumerable(newtrainingData);

        //    //Lấy dữ liệu theo email
        //    var lst = await _repoDatPhong.GetByEmail(email);
        //    //lst = lst.Select(datPhong => new { datPhong.Id, datPhong.IdPhong }).ToList();

        //    //tách dữ liệu thành 2 phần 20% cho test, 80% cho train
        //    TrainTestData splitDataView = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);


        //    var options = new MatrixFactorizationTrainer.Options
        //    {
        //        MatrixColumnIndexColumnName = "Id",
        //        MatrixRowIndexColumnName = "IdPhong",
        //        LabelColumnName = "Label",
        //        NumberOfIterations = 20,
        //        ApproximationRank = 100
        //    };

        //    var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("IdPhong", nameof(DatPhong.IdPhong))
        //                   // .Append(_mlContext.Transforms.Concatenate("Features", nameof(DatPhong.Email)))
        //                  //  .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
        //                    // .Append(_mlContext.Transforms.Conversion.MapValueToKey("PhongId", nameof(DatPhong.IdPhong)))
        //                    .Append(_mlContext.Recommendation().Trainers.);
        //    //.Append(_mlContext.Regression.Trainers.FastTree());




        //    _model = pipeline.Fit(dataView);



        //    //var lst = await _repoDatPhong.GetByEmail(email);


        //    var predictionEngine = _mlContext.Model.CreatePredictionEngine<DatPhong, PhongPrediction>(_model);
        //    List<DatPhong> inputData = lst;
        //    var prediction = predictionEngine.Predict(inputData[0]);


        //    return Ok(prediction);
        //}

        //[HttpGet("Recommendtion")]
        //public async Task<IActionResult> Recommendation(string email)
        //{
        //    //Train model
        //    var trainingData = _context.DatPhongs!.ToList();
        //    var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

        //    var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(DatPhong.IdPhong))
        //        .Append(_mlContext.Transforms.Concatenate("Features", nameof(DatPhong.ThoiGianNhanPhong), nameof(DatPhong.ThoiGianTraPhong)))
        //        .Append(_mlContext.Recommendation().Trainers.MatrixFactorization("Label", "Features", "Label", 5));
        //    //.Append(_mlContext.Regression.Trainers.FastTree());

        //    _model = pipeline.Fit(dataView);



        //    var lst = await _repoDatPhong.GetByEmail(email);


        //    var predictionEngine = _mlContext.Model.CreatePredictionEngine<DatPhong, PhongPrediction>(_model);
        //    List<DatPhong> inputData = lst;
        //    var prediction = predictionEngine.Predict(inputData[0]);


        //    return Ok(prediction);
        //}
    }
}
