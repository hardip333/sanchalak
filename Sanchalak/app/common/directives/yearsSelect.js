app.directive("yearsSelect", function () {
    return {
        templateUrl: "UI/components/yearsSelect.html",
        restrict: "E",
        replace: !0,
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.yearList = [

                //{
                //    "yearNo": 00,
                //    "name": "Select Year"
                //},
                {
                    "yearNo": 2021,
                    "name": 2021
                },
                {
                    "yearNo": 2022,
                    "name": 2022
                },
                {
                    "yearNo": 2023,
                    "name": 2023
                },
                {
                    "yearNo": 2024,
                    "name": 2024
                },
                {
                    "yearNo": 2025,
                    "name": 2025
                },
                {
                    "yearNo": 2026,
                    "name": 2026
                },
                {
                    "yearNo": 2027,
                    "name": 2027
                },
                {
                    "yearNo": 2028,
                    "name": 2028
                },
                {
                    "yearNo": 2029,
                    "name": 2029
                },
                {
                    "yearNo": 2030,
                    "name": 2030
                },
                {
                    "yearNo": 2031,
                    "name": 2031
                },
            ];

        }],
        link: function (e, t, a) {
        }
    }
})