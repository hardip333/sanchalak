app.directive("progPartTermInstSelectWithPpidBranchid", function () {
    return {
        templateUrl: "UI/components/PartTermInstanceByProgPartBranchIdDD.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ProgPartTermListGet = [];
            $scope.getPPTList = function (data) {
                $scope.ProgPartTermListGet = [];
                $http({
                    method: 'POST',
                    url: 'api/ComponentDropDown/PartTermInstanceDD',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $scope.ProgPartTermListGet = [];
                            alert(response.obj);
                        }
                        else {

                            $scope.ProgPartTermListGet = response.obj;

                        }
                    })
                    .error(function (res) {

                        alert(res.obj);
                    });
            }
            /*      $scope.getPPTList();*/


        }],
        link: function (e, t, a) {
        }
    }
})