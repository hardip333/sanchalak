app.directive("monthsSelect", function () {
    return {
        templateUrl: "UI/components/monthsSelect.html",
        restrict: "E",
        replace: !0,
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.monthList = [

                //{
                //    "monthNo": 00,
                //    "name": "Select Month"
                //},
                {
                    "monthNo": 01,
                    "name": "Jan"
                },
                {
                    "monthNo": 02,
                    "name": "Feb"
                },
                {
                    "monthNo": 03,
                    "name": "Mar"
                },
                {
                    "monthNo": 04,
                    "name": "Apr"
                },
                {
                    "monthNo": 05,
                    "name": "May"
                },
                {
                    "monthNo": 06,
                    "name": "Jun"
                },
                {
                    "monthNo": 07,
                    "name": "Jul"
                },
                {
                    "monthNo": 08,
                    "name": "Aug"
                },
                {
                    "monthNo": 09,
                    "name": "Sep"
                },
                {
                    "monthNo": 10,
                    "name": "Oct"
                },
                {
                    "monthNo": 11,
                    "name": "Nov"
                },
                {
                    "monthNo": 12,
                    "name": "Dec"
                }
            ];
          
        
            //$scope.getMonthList = function () {

            //    $http({
            //        method: 'POST',
            //        url: 'api/Common/getMonthsTillDate',
            //        data: {},
            //        headers: { "Content-Type": 'application/json' }
            //    })
            //        .success(function (response) {
            //            $rootScope.showLoading = false;

            //            if (response.response_code != "200") {
            //                alert(response.obj);
            //            }
            //            else {

            //                $scope.tempList = response.obj;
            //                $scope.monthList = $scope.monthList.concat($scope.tempList);
            //            }
            //        })
            //        .error(function (res) {
            //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            //        });
            //}

        }],
        link: function (e, t, a) {
        }
    }
})