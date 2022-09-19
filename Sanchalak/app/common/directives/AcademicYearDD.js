app.directive("academicYearSelect", function () {
    return {
        templateUrl: "UI/components/AcademicYearDD.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.AcademicYearList = [];
            $scope.getAcademicYearList = function () {
                $scope.AcademicYearList = [];
                $http({
                    method: 'POST',
                    url: 'api/ComponentDropDown/AcademicYearDD',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $scope.AcademicYearList = [];
                            alert(response.obj);
                        }
                        else {                            
                            $scope.AcademicYearList = response.obj;
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getAcademicYearList();

        }],
        link: function (e, t, a) {
        }
    }
})