
app.directive("branchSelectWithPiid", function () {
    return {
        templateUrl: "UI/components/BranchByPIidDD.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.BranchListGet = [];
            $scope.getBranchList = function (data) {
                $scope.BranchListGet = [];
                $http({
                    method: 'POST',
                    url: 'api/ComponentDropDown/BranchDD',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $scope.BranchListGet = [];
                            alert(response.obj);
                        }
                        else {

                            $scope.BranchListGet = response.obj;

                        }
                    })
                    .error(function (res) {

                        alert(res.obj);
                    });
            }
            /*      $scope.getBranchList();*/


        }],
        link: function (e, t, a) {
        }
    }
})