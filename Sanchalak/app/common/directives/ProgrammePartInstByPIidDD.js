app.directive("progPartInstSelectWithPiid", function () {
    return {
        templateUrl: "UI/components/ProgrammePartInstByPIidDD.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ProgPartListGet = [];
            $scope.getPPList = function (data) {
                $scope.ProgPartListGet = [];
                $http({
                    method: 'POST',
                    url: 'api/ComponentDropDown/PartInstanceDD',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $scope.ProgPartListGet = [];
                            alert(response.obj);
                        }
                        else {

                            $scope.ProgPartListGet = response.obj;

                        }
                    })
                    .error(function (res) {

                        alert(res.obj);
                    });
            }
            /*      $scope.getPPList();*/


        }],
        link: function (e, t, a) {
        }
    }
})