using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System.Linq;

namespace BookingDreams.Respositories
{
    public class RecommendOrderRes
    {
        //private readonly List<Phong> _allRooms;
        //public  RecommendOrderRes(List<Phong> allRooms) {
        //    _allRooms = allRooms;
        //}
        public IEnumerable<PhongModel> GetRecommendedRooms(List<DatPhong> bookingHistory, List<PhongModel> phong)
        {
            // Filter rooms that were booked by the user in the past
            var bookedRoomIds = bookingHistory.Select(history => history.IdPhong).ToList();

            // Group booking history by room and calculate the number of times each room was booked
            var roomBookingCounts = bookingHistory
                .GroupBy(history => history.IdPhong)
                .ToDictionary(group => group.Key, group => group.Count());

            // Order rooms by the number of times booked by the user
            var orderedRooms = phong
                .Where(room => !bookedRoomIds.Contains(room.Id)) // Exclude already booked rooms
                .OrderByDescending(room => roomBookingCounts.GetValueOrDefault(room.Id, 0));

            return orderedRooms;
        }


        //private readonly MLContext _mlContext;
        //private ITransformer _model;

        //public RecommendOrderRes()
        //{
        //    _mlContext = new MLContext(seed: 0);
        //}

        //public void TrainModel(IEnumerable<HouseData> trainingData)
        //{
        //    var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);


        //    var options = new MatrixFactorizationTrainer.Options
        //    {
        //        MatrixColumnIndexColumnName = "Email",
        //        MatrixRowIndexColumnName = "IdPhong",
        //        LabelColumnName = "Label",
        //        NumberOfIterations = 20,
        //        ApproximationRank = 100
        //    };
        //    var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("IdPhong")
        //        .Append(_mlContext.Transforms.Concatenate("Features", nameof(HouseData.HoTen), nameof(HouseData.Email), nameof(HouseData.DiaChi)))
        //        .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
        //        .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));

        //    _model = pipeline.Fit(dataView);
        //}

        //public IEnumerable<int> GetRecommendedRooms(HouseData input)
        //{
        //    var predictionEngine = _mlContext.Model.CreatePredictionEngine<HouseData, HotelPrediction>(_model);
        //    var inputData = new HouseData[] { input };
        //    var prediction = predictionEngine.Predict(inputData[0]);

        //    return prediction.PredictedIdPhong;
        //}



    }
}
