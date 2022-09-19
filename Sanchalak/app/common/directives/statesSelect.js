app.directive("statesSelect", function () {
    return {
        templateUrl: "UI/components/statesSelect.html",
        restrict: "E",
        replace: !0,
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeStateList = [];
            $scope.defaultStateList = [];
            var stateObj = {
                id: 0,
                name: "Select State"
            }
            $scope.defaultStateList.push(stateObj);

            $scope.getActiveStateList = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/' + $rootScope.loginAs + '/getActiveStateList',
                    data: { countryId: data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.activeStateList = $scope.defaultStateList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }

            //$scope.getActiveStateList();

        }],
        link: function (e, t, a) {
        }
    }
})