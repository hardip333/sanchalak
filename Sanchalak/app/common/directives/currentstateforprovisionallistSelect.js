app.directive("currentstateforprovisionallistSelect", function () {
    return {
        templateUrl: "UI/components/currentstateforprovisionallistSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeStateMasterGetList = [];
            $scope.defaultStateList = [];
            var stateObj = {
                Id: 0,
                StateName: "Select Current State"
            }
            $scope.defaultStateList.push(stateObj);

            $scope.getActiveStateMasterGetList = function () {

                $http({
                    method: 'POST',
                    url: 'api/StateMaster/StateMasterGet',
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
                            $scope.activeStateMasterGetList = $scope.defaultStateList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveStateMasterGetList();


        }],
        link: function (e, t, a) {
        }
    }
})