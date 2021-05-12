﻿/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this library is licensed with MIT.
* The library is based on the library 'MightyLittleGeodesy' (https://github.com/bjornsallarp/MightyLittleGeodesy/) 
* which is also released with MIT.
* License information about 'sweden_crs_transformations_4net' and 'MightyLittleGeodesy':
* https://github.com/TomasJohansson/sweden_crs_transformations_4net/blob/csharpe_SwedenCrsTransformations/LICENSE
* For more information see the webpage below.
* https://github.com/TomasJohansson/sweden_crs_transformations_4net
*/
using SwedenCrsTransformations.Transformation.Transformer1;
using System;

namespace SwedenCrsTransformations.Transformation.Transformer2 {
    internal class Transformer2 : TransformStrategy {

        // Implementations of transformations from WGS84:
        private static readonly TransformStrategy _transformStrategy_from_WGS84_to_SWEREF99_or_RT90 = new TransformStrategy_from_WGS84_to_SWEREF99_or_RT90();

        // Implementations of transformations to WGS84:
        private static readonly TransformStrategy _transformStrategy_from_SWEREF99_or_RT90_to_WGS84 = new TransformStrategy_from_SWEREF99_or_RT90_to_WGS84();

        // Implementation first transforming to WGS84 and then to the real target:
        private static readonly TransformStrategy _transFormStrategy_From_Sweref99OrRT90_to_WGS84_andThenToRealTarget = new TransFormStrategy_From_Sweref99OrRT90_to_WGS84_andThenToRealTarget();


        public CrsCoordinate Transform(
            CrsCoordinate sourceCoordinate,
            CrsProjection targetCrsProjection
        ) {
            if (sourceCoordinate.CrsProjection == targetCrsProjection) return sourceCoordinate;

            // Transform FROM wgs84:
            if (
                sourceCoordinate.CrsProjection.IsWgs84()
                &&
                (targetCrsProjection.IsSweref() || targetCrsProjection.IsRT90())
            )
            {
                return method_transformStrategy_from_WGS84_to_SWEREF99_or_RT90(sourceCoordinate, targetCrsProjection);
            }

            // Transform TO wgs84:
            else if (
                targetCrsProjection.IsWgs84()
                &&
                (sourceCoordinate.CrsProjection.IsSweref() || sourceCoordinate.CrsProjection.IsRT90())
            )
            {
                return method_transformStrategy_from_SWEREF99_or_RT90_to_WGS84(sourceCoordinate, targetCrsProjection);
            }

            // Transform between two non-wgs84:
            else if (
                (sourceCoordinate.CrsProjection.IsSweref() || sourceCoordinate.CrsProjection.IsRT90())
                &&
                (targetCrsProjection.IsSweref() || targetCrsProjection.IsRT90())
            )
            {
                // the only direct transform supported is to/from WGS84, so therefore first transform to wgs84
                return method_transFormStrategy_From_Sweref99OrRT90_to_WGS84_andThenToRealTarget(sourceCoordinate, targetCrsProjection);
            }

            throw new ArgumentException(string.Format("Unhandled source/target projection transformation: {0} ==> {1}", sourceCoordinate.CrsProjection, targetCrsProjection));
        }

        private CrsCoordinate method_transformStrategy_from_WGS84_to_SWEREF99_or_RT90(
            CrsCoordinate sourceCoordinate,
            CrsProjection targetCrsProjection
        )
        {
            return _transformStrategy_from_WGS84_to_SWEREF99_or_RT90.Transform(sourceCoordinate, targetCrsProjection);
        }

        private CrsCoordinate method_transformStrategy_from_SWEREF99_or_RT90_to_WGS84(
            CrsCoordinate sourceCoordinate,
            CrsProjection targetCrsProjection
        )
        {
            return _transformStrategy_from_SWEREF99_or_RT90_to_WGS84.Transform(sourceCoordinate, targetCrsProjection);
        }

        private CrsCoordinate method_transFormStrategy_From_Sweref99OrRT90_to_WGS84_andThenToRealTarget(
            CrsCoordinate sourceCoordinate,
            CrsProjection targetCrsProjection
        )
        {
            return _transFormStrategy_From_Sweref99OrRT90_to_WGS84_andThenToRealTarget.Transform(sourceCoordinate, targetCrsProjection);
        }
    }
}
