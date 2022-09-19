app.directive("facultyddSelect", function () {
    return {
        templateUrl: "UI/components/FacultyDD.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.FacultyList = [];
            $scope.getFacultyList = function () {
                $scope.FacultyList = [];
                $http({
                    method: 'POST',
                    url: 'api/ComponentDropDown/FacultyDD',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $scope.FacultyList = [];
                            alert(response.obj);
                        }
                        else {
                            $scope.FacultyList = response.obj;
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            //$scope.getFacultyList();

        }],
        link: function (e, t, a) {
        }
    }
})