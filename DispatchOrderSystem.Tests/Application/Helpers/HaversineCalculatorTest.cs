using DispatchOrderSystem.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Helpers
{
    public class HaversineCalculatorTests
    {
        [Fact]
        public void CalculateDistanceKm_ShouldReturnZero_WhenPointsAreSame()
        {
            // Arrange
            double lat = 4.624335;
            double lon = -74.063644;

            // Act
            var distance = HaversineCalculator.CalculateDistanceKm(lat, lon, lat, lon);

            // Assert
            Assert.Equal(0, distance, precision: 3); // allow small margin
        }

        [Fact]
        public void CalculateDistanceKm_ShouldReturnCorrectDistance_BetweenBogotaAndMedellin()
        {
            // Arrange
            double bogotaLat = 4.624335;
            double bogotaLon = -74.063644;
            double medellinLat = 6.25184;
            double medellinLon = -75.56359;

            // Act
            var distance = HaversineCalculator.CalculateDistanceKm(bogotaLat, bogotaLon, medellinLat, medellinLon);

            // Assert
            Assert.InRange(distance, 240, 260); // Aproximadamente 245 km
        }

        [Fact]
        public void CalculateDistanceKm_ShouldBeSymmetric()
        {
            // Arrange
            double lat1 = 10.96854;
            double lon1 = -74.78132;
            double lat2 = 3.43722;
            double lon2 = -76.5225;

            // Act
            var distance1 = HaversineCalculator.CalculateDistanceKm(lat1, lon1, lat2, lon2);
            var distance2 = HaversineCalculator.CalculateDistanceKm(lat2, lon2, lat1, lon1);

            // Assert
            Assert.Equal(distance1, distance2, precision: 3);
        }
    }
}
