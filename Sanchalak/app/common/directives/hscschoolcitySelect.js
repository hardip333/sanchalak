app.directive("hscschoolcitySelect", function () {
    return {
        templateUrl: "UI/components/hscschoolcitySelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeGenCityList = [];
            $scope.defaultHSCSchoolList = [];
            var hscObj = {
                Id: 0,
                CityName: "Select School Location"
            }
            $scope.defaultHSCSchoolList.push(hscObj);
            $scope.getActiveGenCityList = function () {
     
                $http({
                    method: 'POST',
                    url: 'api/GenCity/getGenCityList',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.activeGenCityList = $scope.defaultHSCSchoolList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveGenCityList();


        }],
        link: function (e, t, a) {
        }
    }
})