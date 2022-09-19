app.directive("progInstSelectWithFidAyid", function () {
    return {
        templateUrl: "UI/components/ProgrammeInstanceByFacultyAYIdDD.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ProgInstListGet = [];
            $scope.getProgInstList = function (data) {
                $scope.ProgInstListGet = [];
                $http({
                    method: 'POST',
                    url: 'api/ComponentDropDown/ProgrammeInstanceDD',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $scope.ProgInstListGet = [];
                            alert(response.obj);
                        }
                        else {
                            $scope.ProgInstListGet = response.obj;

                        }
                    })
                    .error(function (res) {

                        alert(res.obj);
                    });
            }
            /*      $scope.getProgInstListGet();*/


        }],
        link: function (e, t, a) {
        }
    }
})