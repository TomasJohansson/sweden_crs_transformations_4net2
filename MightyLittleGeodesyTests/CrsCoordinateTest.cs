﻿using NUnit.Framework;
using MightyLittleGeodesy;

namespace MightyLittleGeodesyTests {
    
    [TestFixture]
    public class CrsCoordinateTest {
        
        [Test]
        public void CreateCoordinatePointByEpsgNumber() {
            CrsCoordinate crsCoordinate = CrsCoordinate.CreateCoordinatePoint(3006, 20.0, 60.0);
            Assert.AreEqual(3006, crsCoordinate.epsgNumber);
            Assert.AreEqual(20.0, crsCoordinate.xLongitude);
            Assert.AreEqual(60.0, crsCoordinate.yLatitude);
        }

        [Test]
        public void CreateCoordinatePoint() {
            CrsCoordinate crsCoordinate = CrsCoordinate.CreateCoordinatePoint(CrsProjection.sweref_99_tm, 20.0, 60.0);
            Assert.AreEqual(3006, crsCoordinate.epsgNumber);
            Assert.AreEqual(CrsProjection.sweref_99_tm, crsCoordinate.crsProjection);
            Assert.AreEqual(20.0, crsCoordinate.xLongitude);
            Assert.AreEqual(60.0, crsCoordinate.yLatitude);
        }

    }
}
